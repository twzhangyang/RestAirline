using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestAirline.Api.Resources;

namespace RestAirline.Api.Controllers
{
    [Route("api/home")]
    public class HomeController : Controller
    {
        [HttpGet]
        public Task<RestAirlineHomeResource> Index()
        {
            return Task.FromResult(new RestAirlineHomeResource(Url));
        }
    }
}