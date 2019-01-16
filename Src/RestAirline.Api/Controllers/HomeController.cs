using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RestAirline.Api.Resources;

namespace RestAirline.Api.Controllers
{
    [Route("api/home")]
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        [Route("settings")]
        public object Settings()
        {
            var settings = new
            {
                Server = _configuration["SERVER"]
            };

            return settings;
        }
        
        [HttpGet]
        public Task<RestAirlineHomeResource> Index()
        {
            return Task.FromResult(new RestAirlineHomeResource(Url));
        }
    }
}