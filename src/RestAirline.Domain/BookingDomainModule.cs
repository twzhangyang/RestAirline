using System.Reflection;
using EventFlow;
using EventFlow.Configuration;
using EventFlow.EntityFramework;
using EventFlow.EntityFramework.Extensions;
using EventFlow.Extensions;
using RestAirline.Domain.EventSourcing;

namespace RestAirline.Domain
{
    public class BookingDomainModule : IModule
    {
        public void Register(IEventFlowOptions eventFlowOptions)
        {
            eventFlowOptions
                .AddDefaults(typeof(BookingDomainModule).Assembly)
                .ConfigureEntityFramework(EntityFrameworkConfiguration.New)
                .AddDbContextProvider<EventStoreContext, EventStoreContextProvider>()
                .UseEntityFrameworkEventStore<EventStoreContext>()
                .UseEntityFrameworkSnapshotStore<EventStoreContext>();
        }
    }
}