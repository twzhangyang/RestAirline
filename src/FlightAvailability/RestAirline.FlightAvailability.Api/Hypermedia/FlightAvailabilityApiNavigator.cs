using Microsoft.AspNetCore.Mvc;
using RestAirline.FlightAvailability.Api.Controllers;
using RestAirline.FlightAvailability.Api.Resources;
using RestAirline.Web.Hypermedia;
using RestAirline.Web.Hypermedia.Navigator;

namespace RestAirline.FlightAvailability.Api.Hypermedia
{
    public class FlightAvailabilityApiNavigator : ApiNavigator<FlightAvailabilityHomeResource>
    {
        public FlightAvailabilityApiNavigator(IUrlHelper urlHelper) : base(urlHelper.Link((HomeController c) => c.Index())) { }
    }
}