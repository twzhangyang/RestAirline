using System.Threading;
using System.Threading.Tasks;
using EventFlow.Queries;
using Microsoft.AspNetCore.Mvc;
using RestAirline.FlightAvailability.Api.Resources;
using RestAirline.FlightAvailability.Queries.Elasticsearch;

namespace RestAirline.FlightAvailability.Api.Controllers
{
    public class AvailabilityController : Controller
    {
        private readonly IQueryProcessor _queryProcessor;

        public AvailabilityController(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }
        
        [Route("availability")]
        [HttpGet]
        public async Task<FlightAvailabilityResource> GetFlightAvailability(string departure)
        {
            var query = new DepartureStationQuery(departure);

            var flightAvailability = await _queryProcessor.ProcessAsync(query, CancellationToken.None);
            
            return new FlightAvailabilityResource(Url, flightAvailability);
        }
    }
}