using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using FluentAssertions;
using RestAirline.Booking.Api.Resources.Booking;
using RestAirline.Booking.Api.Resources.Booking.Journey;
using RestAirline.Booking.Api.Resources.Booking.Passenger;
using RestAirline.Booking.Api.Resources.Booking.Passenger.Add;
using RestAirline.Booking.Domain.Booking;
using RestAirline.Shared.ModelBuilders;
using Xunit;
using PassengerType = RestAirline.Booking.Domain.Booking.Passenger.PassengerType;

namespace RestAirline.Booking.Api.Tests.Booking
{
    [Collection("api tests")]
    public class BookingControllerTests : TestBase
    {
        private readonly ApiTestClient _apiTestClient;

        public BookingControllerTests()
        {
           _apiTestClient = new ApiTestClient(HttpClient); 
        }
        
        [Fact]
        public async Task ShouldSelectJourney()
        {
            //Arrange
            var command = new SelectJourneysCommand();

            //Act
            var resource = await _apiTestClient.Post<SelectJourneysCommand, JourneysSelectedResource>("api/booking/journeys", command);
            
            //Assert
            resource.ResourceLinks.Should().NotBeNull();
            resource.ResourceCommands.Should().NotBeNull();
        }
        
        [Fact]
        public async Task ShouldGetBooking()
        {
            //Arrange
            var bookingId = await SelectJourney();
            
            //Act
            var booking = await _apiTestClient.Get<BookingResource>($"api/booking/{bookingId}");
            
            //Assert
            booking.Id.Should().NotBeNull();
            booking.Journeys.Should().NotBeEmpty();
            booking.Passengers.Should().BeEmpty();
            booking.ResourceLinks.Should().NotBeNull();
        }
        
        [Fact]
        public async Task ShouldAddPassengerInBooking()
        {
            //Arrange
            var bookingId = await SelectJourney();
            var addPassengerCommand = new AddPassengerCommand
            {
                BookingId = bookingId.Value,
                Age = 40,
                Email = "test@test.com",
                Name = "test",
                PassengerType = PassengerType.Male
            };

            //Act
            var resource = await _apiTestClient.Post<AddPassengerCommand, PassengerAddedResource>( $"api/booking/{bookingId.Value}/passenger", addPassengerCommand);
            
            //Assert
            resource.ResourceLinks.Should().NotBeNull();
            resource.ResourceCommands.Should().NotBeNull();
        }
        
        private async Task<BookingId> SelectJourney()
        {
            var journeys = new JourneysBuilder().BuildJourneys();
            var bookingId = BookingId.New;
            var selectJourneysCommand = new RestAirline.Booking.Commands.Journey.SelectJourneysCommand(bookingId, journeys);

            //Act
            await CommandBus.PublishAsync(selectJourneysCommand, CancellationToken.None);
            return bookingId;
        }
    }
}