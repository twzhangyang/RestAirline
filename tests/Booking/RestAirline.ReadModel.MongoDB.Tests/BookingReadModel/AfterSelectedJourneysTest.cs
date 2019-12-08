using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using RestAirline.Queries.MongoDB.Booking;
using RestAirline.TestsHelper.TestScenario;
using Xunit;

namespace RestAirline.ReadModel.MongoDb.Tests.BookingReadModel
{
    [Collection("mongodb read model tests")]
    public class AfterSelectedJourneysTest : TestBase
    {
        public async Task AfterSelectedJourneysShouldAddJourneysToReadModel()
        {
            //Arrange
            var selectJourneysScenario = new SelectJourneysScenario(CommandBus);

            //Act
            await selectJourneysScenario.Execute();
            var bookingId = selectJourneysScenario.BookingId;

            //Assert
            var query = new BookingIdQuery(bookingId.Value);
            var booking = await QueryProcessor.ProcessAsync(query, CancellationToken.None); 

            booking.Journeys.Should().NotBeEmpty();
        }
    }
}