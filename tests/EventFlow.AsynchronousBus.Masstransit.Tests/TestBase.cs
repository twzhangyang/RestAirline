using System;
using GreenPipes;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EventFlow.AsynchronousBus.MassTransit.Tests
{
    public class TestBase 
    {
        protected IServiceProvider ServiceProvider { get; }
        
        protected IHostedService HostedService { get; }
        
        public TestBase()
        {
            var services = new ServiceCollection();
            
            ConfigBus(services);

            ServiceProvider = services.BuildServiceProvider();
            HostedService = ServiceProvider.GetService<IHostedService>();
        }
        
        public void ConfigBus(IServiceCollection services)
        {
            services.AddScoped<OrderCommittedConsumer>();
            services.AddMassTransit(x =>
            {
                x.AddConsumer<OrderCommittedConsumer>();

                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    var host = cfg.Host(new Uri("rabbitmq://localhost"), hostConfigurator => 
                    { 
                        hostConfigurator.Username("rabbitmq");
                        hostConfigurator.Password("rabbitmq");
                    });

                    cfg.ReceiveEndpoint(host, "submit-order", ep =>
                    {
                        ep.PrefetchCount = 16;
                        ep.UseMessageRetry(r => r.Interval(2, 100));
                        EndpointConvention.Map<OrderCommittedConsumer>(ep.InputAddress);
                        ep.ConfigureConsumer<OrderCommittedConsumer>(provider);
                    });
                }));
            });
            
            services.AddSingleton<IHostedService, BusService>();
        }
    }
}