using EventFlow;
using EventFlow.Configuration;
using EventFlow.Extensions;
using EventFlow.MongoDB.Extensions;
using RestAirline.Shared.Extensions;

namespace RestAirline.ReadModel.MongoDb
{
    public class MongoDbReadModelModule : IModule
    {
        public void Register(IEventFlowOptions eventFlowOptions)
        {
            eventFlowOptions
                .AddDefaults(typeof(MongoDbReadModelModule).Assembly)
                .RegisterMongoDb("restairline-booking-read")
                .UseMongoDbReadModel<BookingReadModel>();
        }
    }
}