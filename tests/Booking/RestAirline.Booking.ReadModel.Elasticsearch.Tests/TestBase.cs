using System;
using Elasticsearch.Net;
using EventFlow;
using EventFlow.Configuration;
using EventFlow.DependencyInjection.Extensions;
using EventFlow.Elasticsearch.Extensions;
using EventFlow.Queries;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using RestAirline.Booking.CommandHandlers;
using RestAirline.Booking.Commands;
using RestAirline.Booking.Domain;
using RestAirline.Booking.QueryHandlers.Elasticsearch;
using RestAirline.TestsHelper;

namespace RestAirline.Booking.ReadModel.Elasticsearch.Tests
{
    public class TestBase : IDisposable
    {
        protected readonly IRootResolver Resolver;
        protected readonly ICommandBus CommandBus;
        protected readonly IQueryProcessor QueryProcessor;

        public TestBase()
        {
            var services = new ServiceCollection();
            var configuration = ConfigurationRootCreator.Create(services);
            var elasticsearchUrl = configuration["ElasticsearchUrl"];
            var connection = new ConnectionSettings(new Uri(elasticsearchUrl));
            connection.DisableDirectStreaming(true);
            
            Resolver = EventFlowOptions.New
                .UseServiceCollection(services)
                .RegisterModule<BookingDomainModule>()
                .RegisterModule<CommandModule>()
                .RegisterModule<CommandHandlersModule>()
                .RegisterModule<ElasticsearchReadModelModule>()
                .RegisterModule<ElasticsearchQueryHandlersModule>()
                .ConfigureElasticsearch(connection)
                .CreateResolver();

            CommandBus = Resolver.Resolve<ICommandBus>();
            QueryProcessor = Resolver.Resolve<IQueryProcessor>();

            var indexer = Resolver.Resolve<BookingReadModelIndexer>();
            indexer.PrepareIndex();
        }

        public void Dispose()
        {
            Resolver?.Dispose();
        }
    }
}