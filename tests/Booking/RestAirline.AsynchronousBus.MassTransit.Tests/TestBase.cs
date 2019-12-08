using System;
using System.Threading;
using EventFlow;
using EventFlow.Configuration;
using EventFlow.DependencyInjection.Extensions;
using EventFlow.EntityFramework;
using EventFlow.Subscribers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestAirline.CommandHandlers;
using RestAirline.Booking.Commands;
using RestAirline.Booking.Domain;
using RestAirline.Booking.Domain.EventSourcing;
using RestAirline.TestsHelper;

namespace RestAirline.AsynchronousBus.MassTransit.Tests
{
    public class TestBase : IDisposable
    {
        protected readonly IRootResolver Resolver;
        protected readonly ICommandBus CommandBus;
        protected IHostedService HostedService { get; }

        public TestBase()
        {
            var services = new ServiceCollection();
            var configuration = ConfigurationRootCreator.Create(services);
            services.Configure<RabbitMqOption>(configuration);
            services.ConfigureBus();
            services.AddSingleton<ISubscribeSynchronousToAll, RabbitMqEventPublisher>();

            Resolver = EventFlowOptions.New
                .UseServiceCollection(services)
                .RegisterModule<BookingDomainModule>()
                .RegisterModule<CommandModule>()
                .RegisterModule<CommandHandlersModule>()
                .RegisterServices(r => r.Register<IDbContextProvider<EventStoreContext>, FakedEventStoreContextProvider>())
                .CreateResolver(false);

            CommandBus = Resolver.Resolve<ICommandBus>();
            HostedService = Resolver.Resolve<IHostedService>();    
        }

        public void Dispose()
        {
            Resolver?.Dispose();
        }
    }
}