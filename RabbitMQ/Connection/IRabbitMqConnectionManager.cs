namespace RabbitMQ.Connection
{
    public interface IRabbitMqConnectionManager
    {
        Task<IChannel> CreateChannel();
        Task DeclareExchangeAndQueue(
            IChannel channel,
            string queueName,
            string routingKey);
    }
}
