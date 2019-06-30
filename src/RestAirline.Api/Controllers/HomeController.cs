using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RestAirline.Api.Resources;

namespace RestAirline.Api.Controllers
{
    [Route("api/home")]
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;

        public HomeController(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        [Route("hello")]
        public object Hello()
        {
            var settings = new
            {
                Message = "Hello, Welcome to RestAirline Api",
                EnvironmentName = _hostingEnvironment.EnvironmentName
            };

            return settings;
        }

        [Route("vars")]
        public object PrintVariables()
        {
            var settings = new
            {
                EventStoreConnectString = _configuration["EventStoreConnectionString"],
                ReadModelConnectString = _configuration["ReadModelConnectionString"]
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