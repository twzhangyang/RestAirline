using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Queries;
using EventFlow.ReadStores.InMemory;
using RestAirline.Queries.Journeys;
using RestAirline.ReadModel;

namespace RestAirline.QueryHandlers.Journeys
{
    public class GetDepartureStationQueryHandler : IQueryHandler<GetDepartureStationsQuery, List<string>>
    {
        private readonly IInMemoryReadStore<StationsReadModel> _readStore;

        public GetDepartureStationQueryHandler(IInMemoryReadStore<StationsReadModel> readStore)
        {
            _readStore = readStore;
        }
        
        public async Task<List<string>> ExecuteQueryAsync(GetDepartureStationsQuery query, CancellationToken cancellationToken)
        {
            var readModels = await _readStore.FindAsync(rm => true, cancellationToken);

            var stations = readModels.SelectMany(x => x.Items)
                .Where(x => x.DepartureDate > query.DepartureTime1)
                .Where(x => x.DepartureDate < query.DepartureTime2)
                .Select(x => x.DepartureStation)
                .ToList();

            return stations;
        }
    }
}