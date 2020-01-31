using EventFlow;
using EventFlow.Configuration;
using EventFlow.MongoDB.Extensions;
using MongoDB.Driver;

namespace RestAirline.Booking.Domain
{
    public class MongoDBEventStoreModule : IModule
    {
        public void Register(IEventFlowOptions eventFlowOptions)
        {
            // TODO: read form config
            var client = new MongoClient("mongodb://localhost:27017");

            eventFlowOptions
                .ConfigureMongoDb(client, "restairline-booking-events")
                .UseMongoDbEventStore()
                .UseMongoDbSnapshotStore();
        }
    }
}