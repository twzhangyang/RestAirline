using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Elasticsearch.ReadStores;
using EventFlow.Queries;
using Nest;
using RestAirline.Queries.Elasticsearch.Booking;
using RestAirline.ReadModel.Elasticsearch;

namespace RestAirline.QueryHandlers.Elasticsearch.Booking
{
    public class BookingQueryHandler : IQueryHandler<BookingIdQuery, BookingReadModel>
    {
        private readonly IElasticClient _elasticClient;
        private readonly IReadModelDescriptionProvider _modelDescriptionProvider;

        public BookingQueryHandler(IElasticClient elasticClient, IReadModelDescriptionProvider modelDescriptionProvider)
        {
            _elasticClient = elasticClient;
            _modelDescriptionProvider = modelDescriptionProvider;
        }
        
        public async Task<BookingReadModel> ExecuteQueryAsync(BookingIdQuery query, CancellationToken cancellationToken)
        {
            var index = _modelDescriptionProvider.GetReadModelDescription<BookingReadModel>().IndexName;
            
            var searchResponse = await _elasticClient.SearchAsync<BookingReadModel>(d => d
                        .RequestConfiguration(c => c
                            .AllowedStatusCodes((int)HttpStatusCode.NotFound))
                        .Index(index.Value)
                        .Query(q => q.Term(m => m.Id, query.BookingId)),
                    cancellationToken)
                .ConfigureAwait(false);

            return searchResponse.Documents
                .First();
        }
    }
}