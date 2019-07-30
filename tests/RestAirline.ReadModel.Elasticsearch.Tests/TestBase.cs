using System;
using EventFlow;
using EventFlow.Configuration;
using EventFlow.DependencyInjection.Extensions;
using EventFlow.Elasticsearch.Extensions;
using EventFlow.Queries;
using Microsoft.Extensions.DependencyInjection;
using RestAirline.CommandHandlers;
using RestAirline.Domain;
using RestAirline.QueryHandlers;
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
            var elasticseachUrl = configuration["Elasticsearch_url"];

            Resolver = EventFlowOptions.New
                .UseServiceCollection(services)
                .RegisterModule<BookingDomainModule>()
                .RegisterModule<CommandModule>()
                .RegisterModule<CommandHandlersModule>()
                .RegisterModule<QueryHandlersModule>()
                .ConfigureElasticsearch(elasticseachUrl)
                .RegisterModule<ElasticsearchReadModelModule>()
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