using Microsoft.AspNetCore.Mvc;
using RestAirline.Api.Controllers;
using RestAirline.Api.Resources;

namespace RestAirline.Api.HyperMedia.Navigator
{
    public class RestAirlineApiNavigator : ApiNavigator<RestAirlineHomeResource>
    {
        public RestAirlineApiNavigator(IUrlHelper urlHelper) : base(urlHelper.Link((HomeController c) => c.Index())) { }
    }
}