using Microsoft.Extensions.Options;

namespace RabbitMQ.Connection
{
    public class RabbitMqConnectionManager : IRabbitMqConnectionManager, IDisposable
    {
        private readonly IConnection _connection;
        private readonly RabbitMqConfig _config;

        public RabbitMqConnectionManager(IOptions<RabbitMqConfig> config)
        {
            _config = config.Value;

            var factory = new ConnectionFactory
            {
                HostName = _config.HostName,
                Port = _config.Port,
                UserName = _config.UserName,
                Password = _config.Password,
                AutomaticRecoveryEnabled = true,
                NetworkRecoveryInterval = TimeSpan.FromSeconds(10)
            };

            _connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
        }

        public async Task<IChannel> CreateChannel()
        {
            var channel = await _connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(
                exchange: _config.ExchangeName,
                type: _config.ExchangeType,
                durable: true,
                autoDelete: false);

            return channel;
        }

        public async Task DeclareExchangeAndQueue(IChannel channel, string queueName, string routingKey)
        {
            await channel.QueueDeclareAsync(
                queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            await channel.QueueBindAsync(
                queue: queueName,
                exchange: _config.ExchangeName,
                routingKey: routingKey
            );
        }

        public void Dispose()
        {
            _connection?.CloseAsync();
            _connection?.Dispose();
        }
    }
}
