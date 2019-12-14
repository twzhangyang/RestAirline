using System;
using EventFlow;
using EventFlow.Configuration;
using EventFlow.DependencyInjection.Extensions;
using EventFlow.Elasticsearch.Extensions;
using EventFlow.Queries;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using RestAirline.FlightAvailability.CommandHandlers;
using RestAirline.FlightAvailability.Commands;
using RestAirline.FlightAvailability.Domain;
using RestAirline.FlightAvailability.QueryHandlers.Elasticsearch;
using RestAirline.Shared;

namespace RestAirline.FlightAvailability.ReadModel.Elasticsearch.Tests
{
    public class TestBase : IDisposable
    {
        protected readonly IRootResolver Resolver;
        protected readonly ICommandBus CommandBus;
        protected readonly IQueryProcessor QueryProcessor;

        public TestBase()
        {
            var services = new ServiceCollection();
            var configuration = ConfigurationRootCreatorForTesting.Create(services);
            var elasticsearchUrl = configuration["ElasticsearchUrl"];
            var connection = new ConnectionSettings(new Uri(elasticsearchUrl));
            connection.DisableDirectStreaming(true);
            
            Resolver = EventFlowOptions.New
                .UseServiceCollection(services)
                .RegisterModule<FlightAvailabilityDomainModule>()
                .RegisterModule<CommandModule>()
                .RegisterModule<CommandHandlersModule>()
                .RegisterModule<ElasticsearchReadModelModule>()
                .RegisterModule<ElasticsearchQueryHandlersModule>()
                .ConfigureElasticsearch(connection)
                .CreateResolver();

            CommandBus = Resolver.Resolve<ICommandBus>();
            QueryProcessor = Resolver.Resolve<IQueryProcessor>();

            var indexer = Resolver.Resolve<FlightAvailabilityReadModelIndexer>();
            indexer.PrepareIndex();
        }

        public void Dispose()
        {
            Resolver?.Dispose();
        }
    }
}