using System;
using System.Threading.Tasks;
using EventFlow;
using EventFlow.Aggregates;
using EventFlow.DependencyInjection.Extensions;
using EventFlow.Elasticsearch.Extensions;
using EventFlow.EntityFramework;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using RestAirline.FlightAvailability.CommandHandlers;
using RestAirline.FlightAvailability.Commands;
using RestAirline.FlightAvailability.Domain;
using RestAirline.FlightAvailability.QueryHandlers.Elasticsearch;
using RestAirline.FlightAvailability.ReadModel.Elasticsearch;

namespace RestAirline.FlightAvailability.Management
{
    class Program
    {
        private static ConnectionSettings _connectionSettings;

        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            Config(services);


            var resolver = EventFlowOptions.New
                .UseServiceCollection(services)
                .RegisterModule<FlightAvailabilityDomainModule>()
                .RegisterModule<CommandModule>()
                .RegisterModule<CommandHandlersModule>()
                .RegisterModule<ElasticsearchReadModelModule>()
                .RegisterModule<ElasticsearchQueryHandlersModule>()
                .ConfigureElasticsearch(_connectionSettings)
                .RegisterModule<ManagementModule>()
                .CreateResolver();

            Console.WriteLine("Start to add flights");

            var flightsScheduler = resolver.Resolve<FlightsScheduler>();

            Task.WaitAll(flightsScheduler.AddFlights);

            Console.WriteLine("End of adding flights");
            Console.ReadKey();
        }

        private static void Config(ServiceCollection services)
        {
            services.AddOptions();

            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json",
                optional: false);

            var ConfigurationRoot = configurationBuilder.Build();
            services.AddSingleton((IConfiguration) ConfigurationRoot);

            var elasticsearchUrl = ConfigurationRoot["ElasticsearchUrl"];
            _connectionSettings = new ConnectionSettings(new Uri(elasticsearchUrl));
            _connectionSettings.DisableDirectStreaming(true);
        }
    }
}