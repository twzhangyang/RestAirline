using EventFlow;
using EventFlow.Configuration;
using EventFlow.EntityFramework;
using EventFlow.EntityFramework.Extensions;
using RestAirline.Domain.EventSourcing;

namespace RestAirline.Api.ServiceModules
{
    public class EventStoreModule: IModule
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