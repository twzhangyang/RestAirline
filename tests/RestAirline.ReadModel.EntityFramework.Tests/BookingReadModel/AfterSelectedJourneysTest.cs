using System.Threading;
using System.Threading.Tasks;
using EventFlow.EntityFramework.ReadStores;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using RestAirline.ReadModel.EntityFramework.DBContext;
using RestAirline.TestsHelper.TestScenario;
using Xunit;

namespace RestAirline.ReadModel.EntityFramework.Tests.BookingReadModel
{
    public class AfterSelectedJourneysTest : TestBase
    {
        private readonly FakedEntityFramewokReadModelDbContextProvider _contextProvider;

        public AfterSelectedJourneysTest()
        {
            _contextProvider = Resolver.Resolve<FakedEntityFramewokReadModelDbContextProvider>();
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
            using (var context = _contextProvider.CreateContext())
            {
                var booking = await context.Bookings
                    .Include(x => x.Journeys)
                    .Include(x => x.Passengers)
                    .SingleAsync(x => x.Id == bookingId.Value, CancellationToken.None);
                booking.Journeys.Should().NotBeEmpty();
            }
        }
    }
}