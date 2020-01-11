using System.Linq;
using EventFlow.Elasticsearch.ReadStores;
using Nest;

namespace RestAirline.FlightAvailability.ReadModel.Elasticsearch
{
    public class FlightAvailabilityReadModelIndexer
    {
        private readonly IElasticClient _elasticClient;
        private readonly IReadModelDescriptionProvider _descriptionProvider;

        public FlightAvailabilityReadModelIndexer(IElasticClient elasticClient,
            IReadModelDescriptionProvider descriptionProvider)
        {
            _elasticClient = elasticClient;
            _descriptionProvider = descriptionProvider;
        }

        public void PrepareIndex()
        {
            var modelDescription = _descriptionProvider.GetReadModelDescription<FlightAvailabilityReadModel>();
            var indexName = GetIndexName(modelDescription.IndexName.Value);
            var isExist = _elasticClient.IndexExists(indexName).Exists;
            if (isExist)
            {
                return;
            }

            _elasticClient.CreateIndex(indexName, c => c
                .Settings(s => s
                    .NumberOfShards(1)
                    .NumberOfReplicas(0))
                .Aliases(a => a.Alias(modelDescription.IndexName.Value))
                .Mappings(m => m
                    .Map<FlightAvailabilityReadModel>(map => map
                        .AutoMap())));
        }

        private string GetIndexName(string name)
        {
            return $"restailine-{name}-{001}".ToLowerInvariant();
        }
    }
}