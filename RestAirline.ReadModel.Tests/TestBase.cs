using System;
using EventFlow;
using EventFlow.Configuration;
using RestAirline.CommandHandlers;
using RestAirline.Domain;

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
                .ConfigureBookingCommandHandlers()
                .ConfigureReadModel()
                .CreateResolver();

            CommandBus = Resolver.Resolve<ICommandBus>();
        }

        public void Dispose()
        {
            Resolver?.Dispose();
        }
    }
}