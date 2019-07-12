using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RestAirline.Shared.Extensions;

namespace RestAirline.Api
{
    public class HealthCheckerResponseProvider
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public HealthCheckerResponseProvider(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public Task Writer(HttpContext httpContext, HealthReport result)
        {
            httpContext.Response.ContentType = "application/json";
            var dt = DateTime.Now;
            var settings = new
            {
                Message = "Hello, Welcome to RestAirline Api",
                EnvironmentName = _hostingEnvironment.EnvironmentName,
                LocalDate = dt,
                UtcDate = dt.ToUniversalTime(),
                Status = result.Status.ToString()
            };

            return httpContext.Response.WriteAsync(settings.SerializeToCamelCase());
        }
    }
}