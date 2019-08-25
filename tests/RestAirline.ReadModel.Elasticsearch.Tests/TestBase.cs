using System;
using EventFlow;
using EventFlow.Configuration;
using EventFlow.DependencyInjection.Extensions;
using EventFlow.Elasticsearch.Extensions;
using EventFlow.Queries;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using RestAirline.CommandHandlers;
using RestAirline.Commands;
using RestAirline.Domain;
using RestAirline.QueryHandlers.Elasticsearch;
using RestAirline.TestsHelper;

namespace RestAirline.ReadModel.Elasticsearch.Tests
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
            
            Resolver = EventFlowOptions.New
                .UseServiceCollection(services)
                .RegisterModule<BookingDomainModule>()
                .RegisterModule<CommandModule>()
                .RegisterModule<CommandHandlersModule>()
                .RegisterModule<ElasticsearchReadModelModule>()
                .RegisterModule<ElasticsearchQueryHandlersModule>()
                .ConfigureElasticsearch(new ConnectionSettings(new Uri(elasticsearchUrl)))
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