using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using RestAirline.Commands.Journey;
using RestAirline.Domain.Booking;
using RestAirline.Shared.ModelBuilders;
using Xunit;

namespace RestAirline.CommandHandlers.Tests.Journey
{
    public class SelectJourneysCommandHandlerTests : TestBase 
    {
        private readonly BookingId _bookingId;

        public SelectJourneysCommandHandlerTests()
        {
            _bookingId = BookingId.New;
        }

        [Fact]
        public async Task WhenSendSelectJourneysCommandShouldAddJourneys()
        {
            //Arrange
         
            //Assert
            var booking = await AggregateStore.LoadAsync<Booking, BookingId>(_bookingId, CancellationToken.None);
            booking.Journeys.Should().NotBeEmpty();
        }
    }
}