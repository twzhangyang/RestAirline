using System;
using System.Threading.Tasks;

namespace RestAirline.FlightAvailability.Api.Hypermedia
{
    public class FlightAvailabilityApiClient
    {
        private readonly FlightAvailabilityApiNavigator _flightAvailabilityApiNavigator;

        public FlightAvailabilityApiClient(FlightAvailabilityApiNavigator flightAvailabilityApiNavigator)
        {
            _flightAvailabilityApiNavigator = flightAvailabilityApiNavigator;
        }
    }
}