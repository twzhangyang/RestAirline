using System;
using EventFlow;
using EventFlow.Configuration;
using EventFlow.Elasticsearch.Extensions;
using EventFlow.Extensions;
using Nest;
using RestAirline.Shared.Extensions;

namespace RestAirline.FlightAvailability.ReadModel.Elasticsearch
{
    public class ElasticsearchReadModelModule : IModule
    {
        public void Register(IEventFlowOptions eventFlowOptions)
        {
            eventFlowOptions
                .RegisterElasticsearch()
                .AddDefaults(typeof(ElasticsearchReadModelModule).Assembly)
                .RegisterServices(r =>
                    r.Register<FlightAvailabilityReadModelIndexer, FlightAvailabilityReadModelIndexer>())
                .UseElasticsearchReadModel<FlightAvailabilityReadModel>();
        }
    }
}