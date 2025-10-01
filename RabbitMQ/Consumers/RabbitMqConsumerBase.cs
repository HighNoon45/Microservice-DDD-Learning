namespace RabbitMQ.Consumers
{
    public abstract class RabbitMqConsumerBase(IServiceProvider serviceProvider, IRabbitMqConnectionManager connectionManager, ConsumerConfig config) : BackgroundService
    {
        protected readonly IServiceProvider _serviceProvider = serviceProvider;
        protected readonly IRabbitMqConnectionManager _connectionManager = connectionManager;
        protected IChannel? _channel;
        protected readonly ConsumerConfig _config = config;

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            await Task.Yield();

            _channel = await _connectionManager.CreateChannel();

            await _connectionManager.DeclareExchangeAndQueue(_channel, _config.QueueName, _config.RoutingKey);

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    await ProcessMessageAsync(message, ea.RoutingKey);

                    await _channel.BasicAckAsync(ea.DeliveryTag, false);
                }
                catch
                {
                    await _channel.BasicNackAsync(ea.DeliveryTag, false, true);
                }
            };

            await _channel.BasicConsumeAsync(queue: _config.QueueName, autoAck: false, consumer: consumer);

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }

        protected abstract Task ProcessMessageAsync(string message, string routingKey);

        public override void Dispose()
        {
            _channel?.CloseAsync();
            _channel?.Dispose();
            base.Dispose();
        }
    }
}
