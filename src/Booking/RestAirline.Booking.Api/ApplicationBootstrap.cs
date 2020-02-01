using System;
using EventFlow;
using EventFlow.AspNetCore.Extensions;
using EventFlow.DependencyInjection.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RestAirline.Booking.Api.HealthCheck;
using RestAirline.Booking.Api.Swagger;
using RestAirline.Booking.CommandHandlers;
using RestAirline.Booking.Commands;
using RestAirline.Booking.Domain;
using RestAirline.Booking.QueryHandlers.MongoDB;
using RestAirline.Booking.ReadModel.EntityFramework;
using RestAirline.ReadModel.MongoDb;

namespace RestAirline.Booking.Api
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
            RegisterHealthCheck(services);
            SwaggerServicesConfiguration.Confirure(services);

            var eventFlowOptions = EventFlowOptions.New
                .UseServiceCollection(services)
                .AddAspNetCore(options => { options.AddUserClaimsMetadata(); })
                .RegisterModule<BookingDomainModule>()
                .RegisterModule<CommandModule>()
                .RegisterModule<CommandHandlersModule>()
                // EntityFramework was broken after upgrade to .NET3.1
                // https://github.com/eventflow/EventFlow/issues/718
//                .RegisterModule<EntityFrameworkQueryHandlersModule>()
//                .RegisterModule<EntityFrameworkEventStoreModule>()
//                .RegisterModule<EntityFrameworkReadModelModule>();
                
                // MongoDB event store and read model
                .RegisterModule<MongoDBEventStoreModule>()
                .RegisterModule<MongoDbReadModelModule>()
                .RegisterModule<MongoDbQueryHandlersModule>()
                ;

            return eventFlowOptions;
        }

        private static void RegisterHealthCheck(IServiceCollection services)
        {
//            services.AddHostedService<StartupHostedService>();
            services.AddSingleton<StartupHostedServiceHealthCheck>();

            services.AddHealthChecks()
                .AddCheck<StartupHostedServiceHealthCheck>(
                    "hosted_service_startup",
                    failureStatus: HealthStatus.Degraded,
                    tags: new[] {"ready"});
        }
    }
}