using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using RestAirline.Booking.Domain.Tests;
using RestAirline.FlightAvailability.Domain.ModelBuilders;
using Xunit;

namespace RestAirline.FlightAvailability.Domain.Tests
{
    public class AddFLightTests : TestBase
    {
        [Fact]
        public async Task AfterSelectedJourneysShouldHaveJourneys()
        {
            //Arrange
            var flight = new FlightBuilder().Build();

            //Act
            await UpdateAsync(FlightAvailabilityId, (FlightAvailability x) => x.AddFlight(flight));
            
            //Assert
            var flightAvailability = await AggregateStore.LoadAsync<FlightAvailability, FlightAvailabilityId>(FlightAvailabilityId, CancellationToken.None);
            flightAvailability.Flights.Count.Should().Be(1);
        }
    }
}