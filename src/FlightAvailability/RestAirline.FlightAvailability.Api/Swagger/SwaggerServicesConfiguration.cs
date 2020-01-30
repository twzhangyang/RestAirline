using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace RestAirline.FlightAvailability.Api.Swagger
{
    public static class SwaggerServicesConfiguration
    {
        public static void Confirure(IServiceCollection services)
        {
            services.AddSwaggerGen(
                options =>
                {
                    options.DescribeAllEnumsAsStrings();

                    options.EnableAnnotations();

                    options.CustomSchemaIds(t => t.FullName.Replace("+", "."));

                    options.SwaggerDoc("v1", new OpenApiInfo() { Title = "RestAirline API", Version = "v1" });

                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    options.IncludeXmlComments(xmlPath);
                });
        }
    }
}