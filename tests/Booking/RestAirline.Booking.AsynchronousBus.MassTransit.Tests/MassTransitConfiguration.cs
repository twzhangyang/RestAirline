using System;
using GreenPipes;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RestAirline.Booking.AsynchronousBus.MassTransit.Tests.Journey;
using RestAirline.Booking.Domain.Booking.Trip.Events;

namespace RestAirline.Booking.AsynchronousBus.MassTransit.Tests
{
    public static class MassTransitConfiguration
    {
        private static IOptions<RabbitMqOption> _rabbitMqOption;
        
        public static void ConfigureBus(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumersFromNamespaceContaining<OrderCommittedConsumer>();
                x.AddConsumersFromNamespaceContaining<JourneySelectedConsumer>();

                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    _rabbitMqOption = provider.GetService<IOptions<RabbitMqOption>>();

                    var host = cfg.Host(new Uri(_rabbitMqOption.Value.Host), hostConfigurator => 
                    { 
                        hostConfigurator.Username(_rabbitMqOption.Value.UserName);
                        hostConfigurator.Password(_rabbitMqOption.Value.Password);
                    });

                    cfg.ReceiveEndpoint(host, _rabbitMqOption.Value.QueueName, ep =>
                    {
                        ep.PrefetchCount = 16;
                        ep.UseMessageRetry(r => r.Interval(2, 100));
                        ep.ConfigureConsumer<OrderCommittedConsumer>(provider);
                        ep.ConfigureConsumer<JourneySelectedConsumer>(provider);
                    });
                }));
            });
            
            services.AddSingleton<IHostedService, BusService>();

        }
    }
}