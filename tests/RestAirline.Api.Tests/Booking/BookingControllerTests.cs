using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using RestAirline.Api.Resources.Booking;
using RestAirline.Api.Resources.Booking.Journey;
using RestAirline.Api.Resources.Booking.Passenger;
using RestAirline.Domain.Booking;
using Xunit;
using AddPassengerCommand = RestAirline.CommandHandlers.Passenger.AddPassengerCommand;

namespace RestAirline.Api.Tests.Booking
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
            //Act
            var journeyResource = await SelectJourney();
            
            //Assert
            journeyResource.ResourceLinks.Should().NotBeNull();
            journeyResource.ResourceCommands.Should().NotBeNull();
        }
        
        [Fact]
        public async Task ShouldGetBooking()
        {
            //Arrange
            var journeyResource = await SelectJourney();
            var bookingId = journeyResource.BookingId;
            
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
            var journeyResource = await SelectJourney();
            var addPassengerCommand = new AddPassengerCommand(new BookingId(journeyResource.BookingId), null);

            //Act
            var resource = await _apiTestClient.Post<AddPassengerCommand, PassengerAddedResource>( $"api/booking/{journeyResource.BookingId}/passenger", addPassengerCommand);
            
            //Assert
            resource.ResourceLinks.Should().NotBeNull();
            resource.ResourceCommands.Should().NotBeNull();
        }
        
        private Task<JourneysSelectedResource> SelectJourney()
        {
            var journeys = new List<string>();
            
            var resource = _apiTestClient.Post<List<string>, JourneysSelectedResource>("api/booking/journeys", journeys);

            return resource;
        }
    }
}