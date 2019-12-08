using System;
using EventFlow;
using EventFlow.Aggregates;
using EventFlow.Configuration;
using EventFlow.DependencyInjection.Extensions;
using EventFlow.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using RestAirline.Booking.Commands;
using RestAirline.Booking.Domain;
using RestAirline.Booking.Domain.EventSourcing;
using RestAirline.TestsHelper;

namespace RestAirline.CommandHandlers.Tests
{
    public class TestBase : IDisposable
    {
        protected readonly IRootResolver Resolver;
        protected readonly ICommandBus CommandBus;
        protected readonly IAggregateStore AggregateStore;

        public TestBase()
        {
            var services = new ServiceCollection();
            ConfigurationRootCreator.Create(services);

            Resolver = EventFlowOptions.New
                .UseServiceCollection(services)
                .RegisterModule<BookingDomainModule>()
                .RegisterModule<CommandModule>()
                .RegisterModule<CommandHandlersModule>()
                .RegisterServices(r => r.Register<IDbContextProvider<EventStoreContext>, FakedEventStoreContextProvider>())
                .CreateResolver();

            CommandBus = Resolver.Resolve<ICommandBus>();
            AggregateStore = Resolver.Resolve<IAggregateStore>();
        }

        public void Dispose()
        {
            Resolver?.Dispose();
        }
    }
}