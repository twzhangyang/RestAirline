using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.ReadStores.InMemory;
using FluentAssertions;
using RestAirline.Commands.Journey;
using RestAirline.Domain.Booking;
using RestAirline.TestsHelper;
using Xunit;

namespace RestAirline.ReadModel.Tests
{
    public class BookingReadModelTest : TestBase
    {
        private readonly BookingId _bookingId;
        private readonly IInMemoryReadStore<BookingReadModel> _bookingReadModel;

        public BookingReadModelTest()
        {
            _bookingId = BookingId.New;
            _bookingReadModel = Resolver.Resolve<IInMemoryReadStore<BookingReadModel>>();
        }

        [Fact]
        public async Task WhenSendSelectJourneysCommandShouldAddJourneysInReadModel()
        {
            //Arrange
            var journeys = new JourneysBuilder().BuildJourneys();
            var selectJourneysCommand = new SelectJourneysCommand(_bookingId, journeys);

            //Act
            await CommandBus.PublishAsync(selectJourneysCommand, CancellationToken.None);

            //Assert
            var bookings = await _bookingReadModel.FindAsync(rm => rm.Id == _bookingId, CancellationToken.None);
            var booking = bookings.First();
            booking.Journeys.Should().NotBeEmpty();
        }
    }
}