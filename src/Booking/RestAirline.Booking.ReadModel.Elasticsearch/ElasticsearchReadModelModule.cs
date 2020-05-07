using System;
using EventFlow;
using EventFlow.Configuration;
using EventFlow.Elasticsearch.Extensions;
using EventFlow.Extensions;
using RestAirline.Booking.Domain.Booking;
using RestAirline.Shared.Extensions;

namespace RestAirline.Booking.ReadModel.Elasticsearch
{
    public class ElasticsearchReadModelModule : IModule
    {
        public void Register(IEventFlowOptions eventFlowOptions)
        {
            eventFlowOptions
                .RegisterElasticsearch()
                .AddDefaults(typeof(ElasticsearchReadModelModule).Assembly)
                .RegisterServices(r=>r.Register<BookingReadModelIndexer, BookingReadModelIndexer>())
                .UseElasticsearchReadModel<BookingReadModel>();
        }
    }
}