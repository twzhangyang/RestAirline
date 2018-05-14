using Microsoft.AspNetCore.Mvc;
using RestAirline.Api.Resources;

namespace RestAirline.Api.Controllers
{
    public class TripAvailabilityController : Controller
    {
        [Route("tripAvailability")]
        public TripAvailabilityResource SearchTrips(SearchTripsCommand command)
        {
            return new TripAvailabilityResource(Url);
        }
    }
}