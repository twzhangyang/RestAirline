using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using EventFlow;
using EventFlow.Configuration;
using EventFlow.EntityFramework;
using EventFlow.MongoDB.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mongo2Go;
using MongoDB.Driver;
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
            var runner = MongoDbRunner.Start();

            ApplicationBootstrap.AddTestingServicesRegistrar(r =>
            {
                r.RegisterServices(register =>
                {
                    register.Register<IDbContextProvider<EventStoreContext>, FakedEventStoreContextProvider>();
                    register
                        .Register<IDbContextProvider<RestAirlineReadModelContext>,
                            FakedEntityFramewokReadModelDbContextProvider>();
                    register.Register(f =>
                    {
                        MongoUrl mongoUrl = new MongoUrl(runner.ConnectionString);
                        IMongoDatabase mongoDatabase = new MongoClient(mongoUrl).GetDatabase("restairline-api-tests");
                        return mongoDatabase;
                    }, Lifetime.Singleton);
                });
            });

            var hostBuilder = new WebHostBuilder()
                .UseEnvironment("UnitTest")
                .ConfigureAppConfiguration((context, builder) => { builder.AddJsonFile("appsettings.UnitTest.json"); })
                .UseStartup<Startup>();

            _server = new TestServer(hostBuilder);
            HttpClient = _server.CreateClient();
            CommandBus = ServiceProvider.Resolve<ICommandBus>();
        }

        protected readonly HttpClient HttpClient;

        protected ILifetimeScope ServiceProvider => ApplicationBootstrap.AutofacContainer;

        protected ICommandBus CommandBus { get; private set; }

        public void Dispose()
        {
            _server.Dispose();
            HttpClient.Dispose();
        }
    }
}