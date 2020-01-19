using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RestAirline.FlightAvailability.Api.Resources;
using Swashbuckle.AspNetCore.Annotations;

namespace RestAirline.FlightAvailability.Api.Controllers
{
    [Route("availability/home")]
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// The summary of the home api
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(FlightAvailabilityHomeResource), 200)]
        [SwaggerOperation(Description = "The home api for restairline")]
        public Task<FlightAvailabilityHomeResource> Index()
        {
            return Task.FromResult(new FlightAvailabilityHomeResource(Url));
        }
    }
}