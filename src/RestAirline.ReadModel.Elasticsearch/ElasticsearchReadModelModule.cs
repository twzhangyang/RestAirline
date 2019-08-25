using System;
using EventFlow;
using EventFlow.Configuration;
using EventFlow.Elasticsearch.Extensions;
using EventFlow.Extensions;
using RestAirline.Domain.Booking;

namespace RestAirline.ReadModel.Elasticsearch
{
    public class ElasticsearchReadModelModule : IModule
    {
        public void Register(IEventFlowOptions eventFlowOptions)
        {
            var elasticsearchUrl = Environment.GetEnvironmentVariable("ELASTICSEARCH_URL");

            eventFlowOptions
                .ConfigureElasticsearch(elasticsearchUrl)
                .AddDefaults(typeof(ElasticsearchReadModelModule).Assembly)
                .UseElasticsearchReadModelFor<Domain.Booking.Booking, BookingId, BookingReadModel>();
        }
    }
}