using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestAirline.Api.Filters;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace RestAirline.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry();

            services.AddMvc(options => { options.Filters.Add<UnhandledExceptionFilter>(); });

            services.AddCors();
            
            if (Environment.IsEnvironment("UnitTest"))
            {
                return ApplicationBootstrap.RegisterServicesForTesting(services);
            }

            return ApplicationBootstrap.RegisterServices(services, Configuration);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });

            app.UseMvc();

            if (!Environment.IsEnvironment("UnitTest"))
            {
                var healthCheckResponseWriterProvider = new HealthCheckerResponseProvider(env);
                app.UseHealthChecks("/health/ready", new HealthCheckOptions
                {
                    Predicate = (check) => check.Tags.Contains("ready"),
                    ResponseWriter = healthCheckResponseWriterProvider.Writer
                });

                app.UseHealthChecks("/health/live", new HealthCheckOptions
                {
                    Predicate = (_) => false,
                    ResponseWriter = healthCheckResponseWriterProvider.Writer
                });
                app.UseSwagger();
                app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });
            }
        }
    }
}