using Microsoft.AspNetCore.Mvc;
using RestAirline.Api.Resources;

namespace RestAirline.Api.Controllers
{
    [Route("api/home")]
    public class HomeController : Controller
    {
        [HttpGet]
        public RestAirlineHomeResource Index()
        {
            return new RestAirlineHomeResource(Url);
        }
    }
}