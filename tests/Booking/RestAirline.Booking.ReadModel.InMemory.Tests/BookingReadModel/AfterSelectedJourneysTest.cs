using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.ReadStores.InMemory;
using FluentAssertions;
using RestAirline.Booking.Commands.Journey;
using RestAirline.Booking.Domain.Booking;
using RestAirline.TestsHelper;
using RestAirline.TestsHelper.TestScenario;
using Xunit;

namespace RestAirline.ReadModel.Tests.BookingReadModel
{
    public class AfterSelectedJourneysTest : TestBase
    {
        private readonly IInMemoryReadStore<Booking.ReadModel.InMemory.BookingReadModel> _bookingReadModel;

        public AfterSelectedJourneysTest()
        {
            _bookingReadModel = Resolver.Resolve<IInMemoryReadStore<Booking.ReadModel.InMemory.BookingReadModel>>();
        }

        [Fact]
        public async Task AfterSelectedJourneysShouldAddJourneysToReadModel()
        {
            //Arrange
            var selectJourneysScenario = new SelectJourneysScenario(CommandBus);

            //Act
            await selectJourneysScenario.Execute();
            var bookingId = selectJourneysScenario.BookingId;
            
            //Assert
            var bookings = await _bookingReadModel.FindAsync(rm => rm.Id == bookingId, CancellationToken.None);
            var booking = bookings.First();
            booking.Journeys.Should().NotBeEmpty();
        }
    }
}