using System;
using EventFlow;
using EventFlow.Configuration;
using EventFlow.DependencyInjection.Extensions;
using EventFlow.MongoDB.Extensions;
using EventFlow.MongoDB.ReadStores;
using Microsoft.Extensions.DependencyInjection;
using Mongo2Go;
using RestAirline.CommandHandlers;
using RestAirline.Domain;

namespace RestAirline.ReadModel.MongoDb.Tests
{
    public class TestBase : IDisposable
    {
        protected readonly IRootResolver Resolver;
        protected readonly ICommandBus CommandBus;
        protected readonly IMongoDbReadModelStore<MongoDbBookingReadModel> ReadModel;

        public TestBase()
        {
            var services = new ServiceCollection();

            var runner = MongoDbRunner.Start();
            
            Resolver = EventFlowOptions.New
                .UseServiceCollection(services)
                .RegisterModule<BookingDomainModule>()
                .RegisterModule<CommandModule>()
                .RegisterModule<CommandHandlersModule>()
                .RegisterModule<MongoDbReadModelModule>()
                .ConfigureMongoDb(runner.ConnectionString, "restairline")
                .CreateResolver();

            CommandBus = Resolver.Resolve<ICommandBus>();
            ReadModel = Resolver.Resolve<IMongoDbReadModelStore<MongoDbBookingReadModel>>();
        }

        public void Dispose()
        {
            Resolver?.Dispose();
        }
    }
}