using System;
using EventFlow;
using EventFlow.Configuration;
using EventFlow.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using RestAirline.CommandHandlers;
using RestAirline.Domain;
using RestAirline.TestsHelper;

namespace RestAirline.ReadModel.EntityFramework.Tests
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
                .RegisterModule<CommandHandlersModule>()
                .RegisterModule<EntityFrameworkReadModelModule>()
                .CreateResolver();

            CommandBus = Resolver.Resolve<ICommandBus>();
        }

        public void Dispose()
        {
            Resolver?.Dispose();
        }
    }
}