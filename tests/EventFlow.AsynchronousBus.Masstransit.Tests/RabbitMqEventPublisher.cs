using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates;
using EventFlow.Subscribers;
using MassTransit;

namespace EventFlow.AsynchronousBus.MassTransit.Tests
{
    public class RabbitMqEventPublisher : ISubscribeSynchronousToAll
    {
        private readonly IBusControl _busControl;
        private readonly IBus _bus;

        public RabbitMqEventPublisher(IBusControl busControl, IBus bus)
        {
            _busControl = busControl;
            _bus = bus;
        }
        
        public Task HandleAsync(IReadOnlyCollection<IDomainEvent> domainEvents, CancellationToken cancellationToken)
        {
            foreach (var domainEvent in domainEvents)
            {
                var a = 1;
            }
            
            return Task.CompletedTask;
        }
    }
}