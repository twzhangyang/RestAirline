using System;
using Autofac;
using EventFlow;
using EventFlow.AspNetCore.Extensions;
using EventFlow.Autofac.Extensions;
using EventFlow.DependencyInjection.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RestAirline.Booking.Api.HealthCheck;
using RestAirline.Booking.Api.Swagger;
using RestAirline.Booking.CommandHandlers;
using RestAirline.Booking.Commands;
using RestAirline.Booking.Domain;
using RestAirline.Booking.QueryHandlers.EntityFramework;
using RestAirline.Booking.QueryHandlers.MongoDB;
using RestAirline.Booking.ReadModel.EntityFramework;
using RestAirline.ReadModel.MongoDb;

namespace RestAirline.Booking.Api
{
    public class ApplicationBootstrap
    {
        private static Action<IEventFlowOptions> _testingServicesRegistrar;

        public static ILifetimeScope AutofacContainer { get; private set; }

        public static void RegisterServices(ContainerBuilder containerBuilder)
        {
            RegisterCommonServices(containerBuilder);
        }

        public static void AddTestingServicesRegistrar(Action<IEventFlowOptions> registrar)
        {
            _testingServicesRegistrar = registrar;
        }

        public static void  RegisterServicesForTesting(ContainerBuilder containerBuilder)
        {
            var eventFlowOptions = RegisterCommonServices(containerBuilder);

            _testingServicesRegistrar?.Invoke(eventFlowOptions);
        }

        public static void SetAutofacContainer(ILifetimeScope container)
        {
            AutofacContainer = container;
        }

        public static IEventFlowOptions RegisterCommonServices(ContainerBuilder container)
        {
            var eventFlowOptions = EventFlowOptions.New
                    .UseAutofacContainerBuilder(container)
                .AddAspNetCore(options => { options.AddUserClaimsMetadata(); })
                .RegisterModule<BookingDomainModule>()
                .RegisterModule<CommandModule>()
                .RegisterModule<CommandHandlersModule>()
                // EntityFramework was broken after upgrade to .NET3.1
                // https://github.com/eventflow/EventFlow/issues/718
                .RegisterModule<EntityFrameworkQueryHandlersModule>()
                .RegisterModule<EntityFrameworkEventStoreModule>()
                .RegisterModule<EntityFrameworkReadModelModule>()

                // MongoDB event store and read model
                .RegisterModule<MongoDBEventStoreModule>()
                .RegisterModule<MongoDbReadModelModule>()
                .RegisterModule<MongoDbQueryHandlersModule>()
                ;

            return eventFlowOptions;
        }
    }
}
