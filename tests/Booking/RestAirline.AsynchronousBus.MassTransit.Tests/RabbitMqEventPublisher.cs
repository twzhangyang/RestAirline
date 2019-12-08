using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates;
using EventFlow.Subscribers;
using MassTransit;

namespace RestAirline.AsynchronousBus.MassTransit.Tests
{
    public class RabbitMqEventPublisher : ISubscribeSynchronousToAll
    {
        private readonly IBusControl _busControl;

        public RabbitMqEventPublisher(IBusControl busControl, IBus bus)
        {
            _busControl = busControl;
        }
        
        public async Task HandleAsync(IReadOnlyCollection<IDomainEvent> domainEvents, CancellationToken cancellationToken)
        {
            foreach (var domainEvent in domainEvents)
            {
                var e = domainEvent.GetAggregateEvent();

               await _busControl.Publish(e, domainEvent.EventType, cancellationToken);
            }
        }
    }
}