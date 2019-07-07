using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RestAirline.Api.Swagger
{
    public class ContentTypeFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            operation.Produces.Clear();
            operation.Produces.Add("application/json");

            operation.Consumes.Clear();
            operation.Consumes.Add("application/json");
        }
    }
}