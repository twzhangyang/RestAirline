using System.Threading;
using System.Threading.Tasks;
using EventFlow.EntityFramework.ReadStores;
using FluentAssertions;
using RestAirline.TestsHelper.TestScenario;
using Xunit;

namespace RestAirline.ReadModel.EntityFramework.Tests.BookingReadModel
{
    public class AfterSelectedJourneysTest : TestBase
    {
        private readonly IEntityFrameworkReadModelStore<EntityFramework.BookingReadModel> _bookingReadModel;

        public AfterSelectedJourneysTest()
        {
            _bookingReadModel = Resolver.Resolve<IEntityFrameworkReadModelStore<EntityFramework.BookingReadModel>>();
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
            var bookings = await _bookingReadModel.GetAsync(bookingId.Value, CancellationToken.None);
            var booking = bookings.ReadModel;
            booking.Journeys.Should().NotBeEmpty();
        }
    }
}