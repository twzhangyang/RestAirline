using System.Reflection;
using EventFlow;
using EventFlow.Configuration;
using EventFlow.EntityFramework;
using EventFlow.EntityFramework.Extensions;
using EventFlow.Extensions;
using RestAirline.Booking.Domain.EventSourcing;

namespace RestAirline.Booking.Domain
{
    public class EntityFrameworkEventStoreModule : IModule
    {
        public void Register(IEventFlowOptions eventFlowOptions)
        {
            eventFlowOptions
                .ConfigureEntityFramework(EntityFrameworkConfiguration.New)
                .AddDbContextProvider<EventStoreContext, EventStoreContextProvider>()
                .UseEntityFrameworkEventStore<EventStoreContext>()
                .UseEntityFrameworkSnapshotStore<EventStoreContext>();
        }
    }
}