using System.Threading.Tasks;
using FluentAssertions;
using MongoDB.Driver;
using RestAirline.TestsHelper.TestScenario;
using Xunit;

namespace RestAirline.ReadModel.MongoDb.Tests.BookingReadModel
{
    public class AfterSelectedJourneysTest : TestBase
    {
        [Fact]
        public async Task AfterSelectedJourneysShouldAddJourneysToReadModel()
        {
            //Arrange
            var selectJourneysScenario = new SelectJourneysScenario(CommandBus);

            //Act
            await selectJourneysScenario.Execute();
            var bookingId = selectJourneysScenario.BookingId;

            //Assert
            var bookingCursor = await ReadModel.FindAsync(f => f.Id == bookingId.Value);
            var booking = await bookingCursor.FirstOrDefaultAsync();

            booking.Journeys.Should().NotBeEmpty();
        }
    }
}