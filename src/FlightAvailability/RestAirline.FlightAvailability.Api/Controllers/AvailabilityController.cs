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
        private readonly FlightsScheduler _flightsScheduler;

        public AvailabilityController(IQueryProcessor queryProcessor, FlightsScheduler flightsScheduler)
        {
            _queryProcessor = queryProcessor;
            _flightsScheduler = flightsScheduler;
        }
        
        [Route("availability")]
        [HttpGet]
        public async Task<FlightAvailabilityResource> GetFlightAvailability(string departure)
        {
            var query = new DepartureStationQuery(departure);

            var flightAvailability = await _queryProcessor.ProcessAsync(query, CancellationToken.None);
            
            return new FlightAvailabilityResource(Url, flightAvailability);
        }
        
        [Route("schedule")]
        [HttpPost]
        public async Task<FlightAvailabilityResource> ScheduleFlights()
        {
            var results = _flightsScheduler.AddFlights();
            Task.WaitAll(results);

            return await GetFlightAvailability("MEL");
        }
    }
}