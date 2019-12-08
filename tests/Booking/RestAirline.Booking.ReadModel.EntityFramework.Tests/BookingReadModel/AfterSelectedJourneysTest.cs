using System.Threading;
using System.Threading.Tasks;
using EventFlow.EntityFramework.ReadStores;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using RestAirline.Booking.Queries.EntityFramework.Booking;
using RestAirline.Booking.ReadModel.EntityFramework.DBContext;
using RestAirline.TestsHelper.TestScenario;
using Xunit;

namespace RestAirline.Booking.ReadModel.EntityFramework.Tests.BookingReadModel
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
            var booking =
                await QueryProcessor.ProcessAsync(new BookingIdQuery(bookingId.Value), CancellationToken.None);
            booking.Journeys.Should().NotBeEmpty();
        }
    }
}