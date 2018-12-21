using System;
using EventFlow;
using EventFlow.Configuration;
using RestAirline.CommandHandlers;
using RestAirline.Domain;
using RestAirline.QueryHandlers;

namespace RestAirline.ReadModel.Tests
{
    public class TestBase : IDisposable
    {
        protected IRootResolver Resolver;
        protected ICommandBus CommandBus;

        public TestBase()
        {
            Resolver = EventFlowOptions.New.ConfigureBookingDomain()
                .ConfigureBookingCommands()
                .ConfigureReadModel()
                .ConfigureBookingCommandHandlers()
                .ConfigureBookingQueryHandlers()
                .CreateResolver();

            CommandBus = Resolver.Resolve<ICommandBus>();
        }

        public void Dispose()
        {
            Resolver?.Dispose();
        }
    }
}