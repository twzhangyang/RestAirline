using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace RestAirline.Api.Swagger
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

                    options.SwaggerDoc("v1", new Info { Title = "RestAirline API", Version = "v1" });

                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    options.IncludeXmlComments(xmlPath);
                    
                    
                    //options.AddSecurityDefinition("Bearer", new ApiKeyScheme
//                    {
//                        Description =
//                            "JWT Authorization header using the Bearer scheme. Example: \"Authorization: {token}\"",
//                        Name = "Authorization",
//                        In = "header",
//                        Type = "apiKey"
//                    });
//                    options.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
//                    {
//                        { "Bearer", new string[] { } }
//                    });

                    options.OperationFilter<ContentTypeFilter>();
                });
        }
    }
}