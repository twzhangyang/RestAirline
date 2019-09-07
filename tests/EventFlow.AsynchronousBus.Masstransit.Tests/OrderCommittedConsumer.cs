using System.Threading.Tasks;
using MassTransit;

namespace EventFlow.AsynchronousBus.MassTransit.Tests
{
    public class OrderCommittedEvent
    {
        public string Id { get; set; }
        
        public string Name { get; set; }
        
        public decimal Price { get; set; }
    }

    public class OrderState
    {
        public static string Name { get; set; }
        
        public static decimal Price { get; set; }
    }
    
    public class OrderCommittedConsumer : IConsumer<OrderCommittedEvent>
    {
        public Task Consume(ConsumeContext<OrderCommittedEvent> context)
        {
            OrderState.Name = context.Message.Name;
            OrderState.Price = context.Message.Price;
            
            return Task.CompletedTask;
        }
    }
}