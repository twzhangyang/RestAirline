namespace RestAirline.AsynchronousBus.MassTransit.Tests
{
    public class RabbitMqOption
    {
        public string Host { get; set; }
        
        public string UserName { get; set; }
        
        public string Password { get; set; }
        
        public string QueueName { get; set; }
    }
}