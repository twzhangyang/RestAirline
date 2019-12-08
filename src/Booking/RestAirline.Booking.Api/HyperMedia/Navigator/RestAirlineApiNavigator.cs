using Microsoft.AspNetCore.Mvc;
using RestAirline.Booking.Api.Controllers;
using RestAirline.Booking.Api.Resources;

namespace RestAirline.Booking.Api.HyperMedia.Navigator
{
    public class RestAirlineApiNavigator : ApiNavigator<RestAirlineHomeResource>
    {
        public RestAirlineApiNavigator(IUrlHelper urlHelper) : base(urlHelper.Link((HomeController c) => c.Index())) { }

    }
}