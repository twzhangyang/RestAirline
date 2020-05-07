using EventFlow;
using EventFlow.Configuration;
using EventFlow.MongoDB.EventStore;
using EventFlow.MongoDB.ReadStores;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace RestAirline.Shared.Extensions
{
    public static class MongoDBRegister
    {
        public static IEventFlowOptions RegisterMongoDb(
            this IEventFlowOptions eventFlowOptions,
            string database)
        {
            return eventFlowOptions.RegisterServices(sr =>
            {
                sr.Register(f =>
                {
                    var connectionString = f.Resolver.Resolve<IConfiguration>()["mongodb"];
                    var mongoDatabase = new MongoClient(connectionString).GetDatabase(database);
                    return mongoDatabase;
                }, Lifetime.Singleton);
                sr.Register<IReadModelDescriptionProvider, ReadModelDescriptionProvider>(Lifetime.Singleton, true);
                sr.Register<IMongoDbEventSequenceStore, MongoDbEventSequenceStore>(Lifetime.Singleton);
            });
        }
    }
}