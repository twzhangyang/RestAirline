using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using RestAirline.FlightAvailability.Queries.Elasticsearch;
using RestAirline.FlightAvailability.TestsHelper;
using Xunit;

namespace RestAirline.FlightAvailability.ReadModel.Elasticsearch.Tests
{
    [Collection("elasticsearch read model tests")]
    public class AfterAddFlightTest : TestBase
    {
        [Fact]
        public async Task AfterAddFlightShouldAddToReadModel()
        {
            //Arrange
            var scenario = new AddFlightScenario(CommandBus);

            //Act
            await scenario.Execute();

            //Assert
            var query = new DepartureStationQuery("MEL");

            var flights = await QueryProcessor.ProcessAsync(query, CancellationToken.None);

            flights.Should().NotBeEmpty();
        }
    }
}