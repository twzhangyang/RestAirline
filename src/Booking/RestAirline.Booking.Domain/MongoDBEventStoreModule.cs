using System;
using EventFlow;
using EventFlow.Configuration;
using EventFlow.MongoDB.Extensions;
using RestAirline.Shared.Extensions;

namespace RestAirline.Booking.Domain
{
    public class MongoDBEventStoreModule : IModule
    {
        public void Register(IEventFlowOptions eventFlowOptions)
        {
            eventFlowOptions
                .RegisterMongoDb("restairline-booking-events")
                .UseMongoDbEventStore()
                .UseMongoDbSnapshotStore();
        }
    }
}