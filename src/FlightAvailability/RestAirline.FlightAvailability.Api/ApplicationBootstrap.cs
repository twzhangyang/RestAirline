using System;
using EventFlow;
using EventFlow.DependencyInjection.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestAirline.FlightAvailability.Api.Swagger;
using RestAirline.FlightAvailability.CommandHandlers;
using RestAirline.FlightAvailability.Commands;
using RestAirline.FlightAvailability.Domain;
using RestAirline.FlightAvailability.QueryHandlers.Elasticsearch;
using RestAirline.FlightAvailability.ReadModel.Elasticsearch;

namespace RestAirline.FlightAvailability.Api
{
    public class ApplicationBootstrap
    {
        private static IServiceProvider _serviceProvider;

        private static Action<IEventFlowOptions> _testingServicesRegistrar;

        public static IServiceProvider ServiceProvider => _serviceProvider;

        public static IServiceProvider RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            var eventFlowOptions = RegisterCommonServices(services);

            _serviceProvider = eventFlowOptions.CreateServiceProvider();
            services.AddScoped(typeof(IServiceProvider), _ => _serviceProvider);

            return _serviceProvider;
        }

        public static void AddTestingServicesRegistrar(Action<IEventFlowOptions> registrar)
        {
            _testingServicesRegistrar = registrar;
        }

        public static IServiceProvider RegisterServicesForTesting(IServiceCollection services)
        {
            var eventFlowOptions = RegisterCommonServices(services);

            _testingServicesRegistrar?.Invoke(eventFlowOptions);
            _serviceProvider = eventFlowOptions.CreateServiceProvider();
            services.AddScoped(typeof(IServiceProvider), _ => _serviceProvider);

            return _serviceProvider;
        }

        public static IEventFlowOptions RegisterCommonServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            SwaggerServicesConfiguration.Confirure(services);

            var eventFlowOptions = EventFlowOptions.New
                .UseServiceCollection(services)
                .RegisterModule<FlightAvailabilityDomainModule>()
                .RegisterModule<CommandModule>()
                .RegisterModule<CommandHandlersModule>()
                .RegisterModule<ElasticsearchReadModelModule>()
                .RegisterModule<ElasticsearchQueryHandlersModule>()
                .RegisterModule<ApiModule>();
            return eventFlowOptions;
        }
    }
}