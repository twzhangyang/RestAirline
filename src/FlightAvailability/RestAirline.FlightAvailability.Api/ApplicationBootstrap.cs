using System;
using EventFlow;
using EventFlow.Aggregates;
using EventFlow.AspNetCore.Extensions;
using EventFlow.DependencyInjection.Extensions;
using EventFlow.EntityFramework;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestAirline.FlightAvailability.Api.Swagger;
using RestAirline.FlightAvailability.CommandHandlers;
using RestAirline.FlightAvailability.Commands;
using RestAirline.FlightAvailability.Domain;
using RestAirline.FlightAvailability.Domain.EventSourcing;
using RestAirline.FlightAvailability.QueryHandlers.Elasticsearch;
using RestAirline.FlightAvailability.ReadModel.Elasticsearch;

namespace RestAirline.FlightAvailability.Api
{
    public class ApplicationBootstrap
    {
        private static IServiceProvider _serviceProvider;

        public static IServiceProvider RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            var eventFlowOptions = RegisterCommonServices(services);

            _serviceProvider = eventFlowOptions.CreateServiceProvider();
            services.AddScoped(typeof(IServiceProvider), _ => _serviceProvider);

            return _serviceProvider;
        }

        public static IServiceProvider RegisterServicesForTesting(IServiceCollection services)
        {
            var eventFlowOptions = RegisterCommonServices(services);

            _serviceProvider = eventFlowOptions.CreateServiceProvider(false);
            services.AddScoped(typeof(IServiceProvider), _ => _serviceProvider);

            return _serviceProvider;
        }

        public static IEventFlowOptions RegisterCommonServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            SwaggerServicesConfiguration.Confirure(services);

            var eventFlowOptions = EventFlowOptions.New
                .UseServiceCollection(services)
                .AddAspNetCore(options => { options.AddUserClaimsMetadata(); })
                .RegisterModule<FlightAvailabilityDomainModule>()
                // .RegisterModule<MongoDBEventStoreModule>()
                .RegisterModule<CommandModule>()
                .RegisterModule<CommandHandlersModule>()
                .RegisterModule<ElasticsearchReadModelModule>()
                .RegisterModule<ElasticsearchQueryHandlersModule>()
                .RegisterModule<ApiModule>();
            
            return eventFlowOptions;
        }
    }
}