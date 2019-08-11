using EventFlow;
using EventFlow.Configuration;
using EventFlow.Extensions;
using EventFlow.MongoDB.Extensions;
using MongoDB.Driver;

namespace RestAirline.ReadModel.MongoDb
{
    public class MongoDbReadModelModule : IModule
    {
        public void Register(IEventFlowOptions eventFlowOptions)
        {
            var client = new MongoClient("mongodb://localhost:27017");

            eventFlowOptions
                .AddDefaults(typeof(MongoDbReadModelModule).Assembly)
                .ConfigureMongoDb(client, "restairline")
                .UseMongoDbReadModel<MongoDbBookingReadModel>();
        }
    }
}