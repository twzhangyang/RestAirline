using System;
using EventFlow;
using EventFlow.Configuration;
using EventFlow.DependencyInjection.Extensions;
using EventFlow.EntityFramework;
using EventFlow.Queries;
using Microsoft.Extensions.DependencyInjection;
using RestAirline.CommandHandlers;
using RestAirline.Booking.Commands;
using RestAirline.Booking.Domain;
using RestAirline.Booking.Domain.EventSourcing;
using RestAirline.QueryHandlers;
using RestAirline.Booking.QueryHandlers.EntityFramework;
using RestAirline.Booking.ReadModel.EntityFramework.DBContext;
using RestAirline.TestsHelper;

namespace RestAirline.ReadModel.EntityFramework.Tests
{
    public class TestBase : IDisposable
    {
        protected readonly IRootResolver Resolver;
        protected readonly ICommandBus CommandBus;
        protected readonly IQueryProcessor QueryProcessor;

        public TestBase()
        {
            var services = new ServiceCollection();
            ConfigurationRootCreator.Create(services);

            Resolver = EventFlowOptions.New
                .UseServiceCollection(services)
                .RegisterModule<BookingDomainModule>()
                .RegisterModule<EntityFrameworkEventStoreModule>()
                .RegisterModule<CommandModule>()
                .RegisterModule<CommandHandlersModule>()
                .RegisterModule<EntityFrameworkReadModelModule>()
                .RegisterModule<EntityFrameworkQueryHandlersModule>()
                .RegisterServices(register =>
                {
                    register.Register<IDbContextProvider<RestAirlineReadModelContext>, FakedEntityFramewokReadModelDbContextProvider>();
                    register.Register<IDbContextProvider<EventStoreContext>, FakedEventStoreContextProvider>();
                })
                .CreateResolver();

            CommandBus = Resolver.Resolve<ICommandBus>();
            QueryProcessor = Resolver.Resolve<IQueryProcessor>();
        }

        public void Dispose()
        {
            Resolver?.Dispose();
        }
    }
}