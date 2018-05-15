using Microsoft.AspNetCore.Mvc;
using RestAirline.Api.Resources;

namespace RestAirline.Api.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        [Route("api/home")]
        [HttpGet]
        public RestAirlineHomeResource Index()
        {
            return new RestAirlineHomeResource(Url);
        }
    }
}