using Microsoft.AspNetCore.Mvc;
using RestAirline.FlightAvailability.Api.Controllers;
using RestAirline.FlightAvailability.Api.Resources;

namespace RestAirline.FlightAvailability.Api.HyperMedia.Navigator
{
    public class RestAirlineApiNavigator : ApiNavigator<FlightAvailabilityHomeResource>
    {
        public RestAirlineApiNavigator(IUrlHelper urlHelper) : base(urlHelper.Link((HomeController c) => c.Index())) { }

    }
}