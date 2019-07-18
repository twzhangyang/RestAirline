using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EventFlow.EntityFramework;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using RestAirline.Domain.EventSourcing;
using RestAirline.ReadModel.EntityFramework.DBContext;
using RestAirline.Shared.Extensions;

namespace RestAirline.Api.Tests
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
        }
        
        protected readonly HttpClient HttpClient;

        protected IServiceProvider ServiceProvider => ApplicationBootstrap.ServiceProvider;

        public void Dispose()
        {
            _server.Dispose();
            HttpClient.Dispose();
        }
    }
}