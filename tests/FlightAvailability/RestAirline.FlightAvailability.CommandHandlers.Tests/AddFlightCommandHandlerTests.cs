using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using RestAirline.FlightAvailability.Domain;
using RestAirline.FlightAvailability.TestsHelper;
using Xunit;

namespace RestAirline.FlightAvailability.CommandHandlers.Tests
{
    public class AddFlightCommandHandlerTests : TestBase
    {
        [Fact]
        public async Task WhenSendAddFlightCommandShouldAddFlight()
        {
            //Arrange
            var scenario = new AddFlightScenario(CommandBus);
            
            //Act
            await scenario.Execute();
            
            //Assert
            var id = scenario.Id;
    
            var flightAvailability = await AggregateStore.LoadAsync<Domain.FlightAvailability, FlightAvailabilityId>(id, CancellationToken.None);
            flightAvailability.Flights.Count.Should().Be(1);
        }
    }
}