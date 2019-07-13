using System;
using EventFlow;
using EventFlow.Configuration;
using EventFlow.DependencyInjection.Extensions;
using EventFlow.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using RestAirline.CommandHandlers;
using RestAirline.Domain;
using RestAirline.Domain.EventSourcing;
using RestAirline.QueryHandlers;
using RestAirline.ReadModel.EntityFramework;
using RestAirline.ReadModel.EntityFramework.DBContext;
using RestAirline.ReadModel.InMemory;
using RestAirline.TestsHelper;

namespace RestAirline.ReadModel.Tests
{
    public class TestBase : IDisposable
    {
        protected readonly IRootResolver Resolver;
        protected readonly ICommandBus CommandBus;

        public TestBase()
        {
            var services = new ServiceCollection();
            ConfigurationRootCreator.Create(services);
            
            Resolver = EventFlowOptions.New
                .UseServiceCollection(services)
                .RegisterModule<BookingDomainModule>()
                .RegisterModule<CommandModule>()
                .RegisterModule<InMemoryReadModelModule>()
                .RegisterModule<EntityFrameworkReadModelModule>()
                .RegisterModule<CommandHandlersModule>()
                .RegisterModule<QueryHandlersModule>()
                .RegisterServices(register =>
                {
                    register.Register<IDbContextProvider<RestAirlineReadModelContext>, FakedEntityFramewokReadModelDbContextProvider>();
                    register.Register<FakedEntityFramewokReadModelDbContextProvider, FakedEntityFramewokReadModelDbContextProvider>();
                    register.Register<IDbContextProvider<EventStoreContext>, FakedEventStoreContextProvider>();
                })
                .CreateResolver();

            CommandBus = Resolver.Resolve<ICommandBus>();
        }

        public void Dispose()
        {
            Resolver?.Dispose();
        }
    }
}