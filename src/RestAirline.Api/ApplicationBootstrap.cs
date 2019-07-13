using System;
using EventFlow;
using EventFlow.AspNetCore.Extensions;
using EventFlow.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RestAirline.Api.HealthCheck;
using RestAirline.Api.Swagger;
using RestAirline.CommandHandlers;
using RestAirline.Domain;
using RestAirline.QueryHandlers;
using RestAirline.ReadModel.EntityFramework;
using RestAirline.ReadModel.InMemory;
using RestAirline.Shared.Extensions;

namespace RestAirline.Api
{
    public class ApplicationBootstrap
    {
        private static IServiceProvider _serviceProvider;

        private static Action<IEventFlowOptions> _testingServicesRegistrar;

        public static IServiceProvider ServiceProvider => _serviceProvider;

        public static IServiceProvider RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            RegisterHealthCheck(services);
            SwaggerServicesConfiguration.Confirure(services);

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
//            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
//            
//            services.AddScoped(typeof(IUrlHelper), s =>
//            {
//                var context = s.ResolveService<IActionContextAccessor>();
//                if (context.ActionContext.IsNull())
//                {
//                    return new UrlHelper(new ActionContext(){RouteData = new RouteData()});
//                }
//                var urlHelper = s.ResolveService<IUrlHelperFactory>().GetUrlHelper(context.ActionContext);
//                return urlHelper;
//            });

            var eventFlowOptions = EventFlowOptions.New
                .UseServiceCollection(services)
                .AddAspNetCore(options => { options.AddUserClaimsMetadata(); })
                .RegisterModule<CommandModule>()
                .RegisterModule<CommandHandlersModule>()
                .RegisterModule<QueryHandlersModule>()
                .RegisterModule<BookingDomainModule>()
                .RegisterModule<InMemoryReadModelModule>()
                .RegisterModule<EntityFrameworkReadModelModule>();

            return eventFlowOptions;
        }

        private static void RegisterHealthCheck(IServiceCollection services)
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