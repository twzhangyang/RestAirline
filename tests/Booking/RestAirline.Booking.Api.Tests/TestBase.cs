using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EventFlow;
using EventFlow.EntityFramework;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using RestAirline.Booking.Domain.EventSourcing;
using RestAirline.Booking.ReadModel.EntityFramework.DBContext;
using RestAirline.Shared.Extensions;

namespace RestAirline.Booking.Api.Tests
{
    public class TestBase : IDisposable
    {
        private readonly TestServer _server;

        protected TestBase()
        {
            ApplicationBootstrap.AddTestingServicesRegistrar(r =>
            {
                r.RegisterServices(register =>
                {
                    register.Register<IDbContextProvider<EventStoreContext>, FakedEventStoreContextProvider>();
                    register.Register<IDbContextProvider<RestAirlineReadModelContext>, FakedEntityFramewokReadModelDbContextProvider>();
                });
            });

            var hostBuilder = new WebHostBuilder()
                .UseEnvironment("UnitTest")
                .UseStartup<Startup>();

            _server = new TestServer(hostBuilder);
            HttpClient = _server.CreateClient();
            CommandBus = ServiceProvider.GetService<ICommandBus>();
        }
        
        protected readonly HttpClient HttpClient;

        protected IServiceProvider ServiceProvider => ApplicationBootstrap.ServiceProvider;

        protected ICommandBus CommandBus { get; private set; }
        
        public void Dispose()
        {
            _server.Dispose();
            HttpClient.Dispose();
        }
    }
}