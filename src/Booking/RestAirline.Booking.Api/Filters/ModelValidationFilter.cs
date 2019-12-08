using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using RestAirline.Shared.Extensions;

namespace RestAirline.Booking.Api.Filters
{
    public class ModelValidationFilter : IActionFilter
    {
        private readonly ILogger<ModelValidationFilter> _logger;

        public ModelValidationFilter(ILogger<ModelValidationFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);

                _logger.LogInformation(context.Result .SerializeToCamelCase());
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Do not need implementation in this class.
        }
    }
}