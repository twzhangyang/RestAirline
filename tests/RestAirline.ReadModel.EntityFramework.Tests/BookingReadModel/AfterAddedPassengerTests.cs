using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using RestAirline.Queries.Booking;
using RestAirline.TestsHelper.TestScenario;
using Xunit;

namespace RestAirline.ReadModel.EntityFramework.Tests.BookingReadModel
{
    public class AfterAddedPassengerTests : TestBase
    {
        [Fact]
        public async Task AfterSelectedJourneysShouldAddJourneysToReadModel()
        {
            //Arrange
            var addPassengerScenario = new AddPassengerScenario(CommandBus);

            //Act
            await addPassengerScenario.Execute();
            var bookingId = addPassengerScenario.BookingId;

            //Assert
            var booking =
                await QueryProcessor.ProcessAsync(new BookingIdQuery(bookingId.Value), CancellationToken.None);
            booking.Journeys.Should().NotBeEmpty();
            booking.Passengers.Should().NotBeEmpty();
        }
    }
}