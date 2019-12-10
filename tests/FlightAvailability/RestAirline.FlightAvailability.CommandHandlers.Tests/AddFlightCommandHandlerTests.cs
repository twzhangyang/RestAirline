using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using RestAirline.FlightAvailability.Commands;
using RestAirline.FlightAvailability.Domain;
using Xunit;

namespace RestAirline.FlightAvailability.CommandHandlers.Tests
{
    public class AddFlightCommandHandlerTests : TestBase
    {
        [Fact]
        public async Task WhenSendAddFlightCommandShouldAddFlight()
        {
            //Arrange
            var id = FlightAvailabilityId.New;
            var command = new AddFlightCommand(id)
            {
                Aircraft = Aircraft.A320,
                Number = "FD320",
                Price = 120.00m,
                ArriveDate = DateTime.Now.AddDays(2),
                ArriveStation = "SYD",
                DepartureDate = DateTime.Now,
                DepartureStation = "MEL",
                FlightKey = Guid.NewGuid().ToString()
            };
            
            //Act
            await CommandBus.PublishAsync(command, CancellationToken.None);

            //Assert
            var flightAvailability = await AggregateStore.LoadAsync<Domain.FlightAvailability, FlightAvailabilityId>(id, CancellationToken.None);
            flightAvailability.Flights.Count.Should().Be(1);
        }
    }
}