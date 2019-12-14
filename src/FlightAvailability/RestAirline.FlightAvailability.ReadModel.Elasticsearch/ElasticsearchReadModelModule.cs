using EventFlow;
using EventFlow.Configuration;
using EventFlow.Elasticsearch.Extensions;
using EventFlow.Extensions;

namespace RestAirline.FlightAvailability.ReadModel.Elasticsearch
{
    public class ElasticsearchReadModelModule : IModule
    {
        public void Register(IEventFlowOptions eventFlowOptions)
        {
            //TODO: read from config
            var elasticsearchUrl = "http://localhost:9200";

            eventFlowOptions
                .ConfigureElasticsearch(elasticsearchUrl)
                .AddDefaults(typeof(ElasticsearchReadModelModule).Assembly)
                .RegisterServices(r =>
                    r.Register<FlightAvailabilityReadModelIndexer, FlightAvailabilityReadModelIndexer>())
                .UseElasticsearchReadModel<FlightAvailabilityReadModel>();
        }
    }
}