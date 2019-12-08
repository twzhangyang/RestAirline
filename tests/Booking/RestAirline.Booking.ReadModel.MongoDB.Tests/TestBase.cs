using System;
using EventFlow;
using EventFlow.Configuration;
using EventFlow.DependencyInjection.Extensions;
using EventFlow.MongoDB.Extensions;
using EventFlow.MongoDB.ReadStores;
using EventFlow.Queries;
using Microsoft.Extensions.DependencyInjection;
using Mongo2Go;
using RestAirline.Booking.CommandHandlers;
using RestAirline.Booking.Commands;
using RestAirline.Booking.Domain;
using RestAirline.Booking.QueryHandlers.MongoDB;

namespace RestAirline.ReadModel.MongoDb.Tests
{
    public class TestBase : IDisposable
    {
        protected readonly IRootResolver Resolver;
        protected readonly ICommandBus CommandBus;
        protected readonly IMongoDbReadModelStore<MongoDbBookingReadModel> ReadModel;
        protected readonly IQueryProcessor QueryProcessor;
        
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
                .RegisterModule<MongoDbQueryHandlersModule>()
                .ConfigureMongoDb(runner.ConnectionString, "restairline")
                .CreateResolver();

            CommandBus = Resolver.Resolve<ICommandBus>();
            QueryProcessor = Resolver.Resolve<IQueryProcessor>();
        }

        public void Dispose()
        {
            Resolver?.Dispose();
        }
    }
}