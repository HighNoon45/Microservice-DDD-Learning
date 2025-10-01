namespace RabbitMQ.Configuration
{
    public class RabbitMqConfig
    {
        public string HostName { get; set; } = "rabbitmq-dev";
        public int Port { get; set; } = 5672;
        public string UserName { get; set; } = "guest";
        public string Password { get; set; } = "guest";
        public string ExchangeName { get; set; } = "app.events.exchange";
        public string ExchangeType { get; set; } = "topic";
    }

    public class ConsumerConfig
    {
        public string QueueName { get; set; } = string.Empty;
        public string RoutingKey { get; set; } = string.Empty;
    }
}
