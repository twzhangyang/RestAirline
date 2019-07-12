using System;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EventFlow;
using EventFlow.AspNetCore.Extensions;
using EventFlow.Autofac.Extensions;
using EventFlow.DependencyInjection.Extensions;
using EventFlow.MsSql;
using EventFlow.MsSql.EventStores;
using EventFlow.MsSql.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using RestAirline.Api.Filters;
using RestAirline.Api.HealthCheck;
using RestAirline.Api.ServiceModules;
using RestAirline.Api.Swagger;
using RestAirline.CommandHandlers;
using RestAirline.Domain;
using RestAirline.Domain.EventSourcing;
using RestAirline.QueryHandlers;
using RestAirline.ReadModel.EntityFramework;
using RestAirline.ReadModel.InMemory;
using RestAirline.Shared.Extensions;
using Swashbuckle.AspNetCore.Swagger;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace RestAirline.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry();

            services.AddMvc(options => { options.Filters.Add<UnhandledExceptionFilter>(); });

            SwaggerServicesConfiguration.Confirure(services);

            RegisterHealthCheck(services);

            services.AddHttpContextAccessor();

            var serviceProvider = EventFlowOptions.New
                .UseServiceCollection(services)
                .AddAspNetCore(options => { options.AddUserClaimsMetadata(); })
                .ConfigureMsSql(MsSqlConfiguration.New.SetConnectionString(Configuration["EventStoreConnectionString"]))
                .UseMssqlEventStore()
                .RegisterModule<EventStoreModule>()
                .RegisterModule<CommandModule>()
                .RegisterModule<CommandHandlersModule>()
                .RegisterModule<QueryHandlersModule>()
                .RegisterModule<BookingDomainModule>()
                .RegisterModule<InMemoryReadModelModule>()
                .RegisterModule<EntityFrameworkReadModelModule>()
                .CreateServiceProvider();

            return serviceProvider;
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

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });
        }

        private void RegisterHealthCheck(IServiceCollection services)
        {
            services.AddHostedService<StartupHostedService>();
            services.AddSingleton<StartupHostedServiceHealthCheck>();

            services.AddHealthChecks()
                .AddCheck<StartupHostedServiceHealthCheck>(
                    "hosted_service_startup",
                    failureStatus: HealthStatus.Degraded,
                    tags: new[] {"ready"});
        }
    }
}