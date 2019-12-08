using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using RestAirline.Booking.Commands.Journey;
using RestAirline.Booking.Domain.Booking;
using RestAirline.Shared.ModelBuilders;
using Xunit;

namespace RestAirline.Booking.CommandHandlers.Tests.Journey
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
            var journeys = new JourneysBuilder().BuildJourneys();
            var selectJourneysCommand = new SelectJourneysCommand(_bookingId, journeys);
            
            //Act
            await CommandBus.PublishAsync(selectJourneysCommand, CancellationToken.None);

            //Assert
            var booking = await AggregateStore.LoadAsync<Domain.Booking.Booking, BookingId>(_bookingId, CancellationToken.None);
            booking.Journeys.Should().NotBeEmpty();
        }
    }
}