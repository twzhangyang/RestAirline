using System;
using EventFlow;
using EventFlow.Configuration;
using RestAirline.CommandHandlers;
using RestAirline.Domain;
using RestAirline.QueryHandlers;
using RestAirline.ReadModel.InMemory;

namespace RestAirline.ReadModel.Tests
{
    public class TestBase : IDisposable
    {
        protected readonly IRootResolver Resolver;
        protected readonly ICommandBus CommandBus;

        public TestBase()
        {
            Resolver = EventFlowOptions.New
                .RegisterModule<BookingDomainModule>()
                .RegisterModule<CommandModule>()
                .RegisterModule<InMemoryReadModelModule>()
                .RegisterModule<CommandHandlersModule>()
                .RegisterModule<QueryHandlersModule>()
                .CreateResolver();

            CommandBus = Resolver.Resolve<ICommandBus>();
        }

        public void Dispose()
        {
            Resolver?.Dispose();
        }
    }
}