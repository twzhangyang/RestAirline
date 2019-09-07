using System;
using EventFlow.Configuration;
using EventFlow.DependencyInjection.Extensions;
using EventFlow.EntityFramework;
using EventFlow.Queries;
using EventFlow.Subscribers;
using GreenPipes;
using MassTransit;
using MassTransit.Scoping;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestAirline.CommandHandlers;
using RestAirline.Commands;
using RestAirline.Domain;
using RestAirline.Domain.EventSourcing;
using RestAirline.ReadModel.InMemory;
using RestAirline.TestsHelper;

namespace EventFlow.AsynchronousBus.MassTransit.Tests
{
    public class TestBase 
    {
        protected readonly IRootResolver Resolver;
        protected readonly ICommandBus CommandBus;
        protected IServiceProvider ServiceProvider { get; }
        
        protected IHostedService HostedService { get; }
        
        public TestBase()
        {
            var services = new ServiceCollection();
            var configuration = ConfigurationRootCreator.Create(services);
            services.Configure<RabbitMqOption>(configuration);
            services.ConfigureBus();
            services.AddSingleton<ISubscribeSynchronousToAll, RabbitMqEventPublisher>();

            EventFlowOptions.New
                .UseServiceCollection(services)
                .RegisterModule<BookingDomainModule>()
                .RegisterModule<CommandModule>()
                .RegisterModule<CommandHandlersModule>()
                .RegisterModule<InMemoryReadModelModule>()
                .RegisterServices(register =>
                {
                    register.Register<IDbContextProvider<EventStoreContext>, FakedEventStoreContextProvider>();
                });
                
            ServiceProvider = services.BuildServiceProvider();
            HostedService = ServiceProvider.GetService<IHostedService>();
            CommandBus = ServiceProvider.GetService<ICommandBus>();
        }
    }
}