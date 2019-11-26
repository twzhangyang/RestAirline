using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using RestAirline.Api.Resources;
using Swashbuckle.AspNetCore.Annotations;

namespace RestAirline.Api.Controllers
{
    [ApiController]
    [Route("api/home")]
    public class HomeController : ControllerBase 
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("vars")]
        [HttpGet]
        public object PrintVariables()
        {
            var settings = new
            {
                EventStoreConnectString = _configuration["EventStoreConnectionString"],
                ReadModelConnectString = _configuration["ReadModelConnectionString"]
            };

            return settings;
        }

        /// <summary>
        /// The summary of the home api
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(RestAirlineHomeResource), 200)]
        [SwaggerOperation(Description = "The home api for restairline")]
        public Task<RestAirlineHomeResource> Index()
        {
            return Task.FromResult(new RestAirlineHomeResource(Url));
        }
    }
}