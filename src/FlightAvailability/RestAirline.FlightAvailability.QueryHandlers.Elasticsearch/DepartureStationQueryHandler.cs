using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Elasticsearch.ReadStores;
using EventFlow.Queries;
using Nest;
using RestAirline.FlightAvailability.Queries.Elasticsearch;
using RestAirline.FlightAvailability.ReadModel.Elasticsearch;

namespace RestAirline.FlightAvailability.QueryHandlers.Elasticsearch
{
    public class DepartureStationQueryHandler : IQueryHandler<DepartureStationQuery, List<FlightAvailabilityReadModel>>
    {
        private readonly IElasticClient _elasticClient;
        private readonly IReadModelDescriptionProvider _modelDescriptionProvider;

        public DepartureStationQueryHandler(IElasticClient elasticClient, IReadModelDescriptionProvider modelDescriptionProvider)
        {
            _elasticClient = elasticClient;
            _modelDescriptionProvider = modelDescriptionProvider;
        }

        public async Task<List<FlightAvailabilityReadModel>> ExecuteQueryAsync(DepartureStationQuery query,
            CancellationToken cancellationToken)
        {
            var index = _modelDescriptionProvider.GetReadModelDescription<FlightAvailabilityReadModel>().IndexName;

            var searchResponse = await _elasticClient.SearchAsync<FlightAvailabilityReadModel>(d => d
                .RequestConfiguration(c => c
                    .AllowedStatusCodes((int) HttpStatusCode.NotFound))
                .Index(index.Value)
                .Query(q => q.QueryString(qs => qs
                    .Query(query.Departure))), cancellationToken);

            return searchResponse.Documents.ToList();
        }
    }
}