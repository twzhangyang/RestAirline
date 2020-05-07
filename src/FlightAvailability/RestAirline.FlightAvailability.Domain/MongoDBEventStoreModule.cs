using EventFlow;
using EventFlow.Configuration;
using EventFlow.MongoDB.Extensions;
using RestAirline.Shared.Extensions;

namespace RestAirline.FlightAvailability.Domain
{
    public class MongoDBEventStoreModule : IModule
    {
        public void Register(IEventFlowOptions eventFlowOptions)
        {
            eventFlowOptions
                .RegisterMongoDb("restairline-flight-availability-events")
                .UseMongoDbEventStore()
                .UseMongoDbSnapshotStore();
        }
    }
}