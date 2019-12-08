using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using RestAirline.TestsHelper.TestScenario;
using Xunit;

namespace RestAirline.Booking.AsynchronousBus.MassTransit.Tests.Journey
{
    public class AfterSelectedJourneysTest : TestBase
    {
        [Fact]
        public async Task AfterSelectedJourneysShouldAddJourneysToReadModel()
        {
            //Arrange
            var selectJourneysScenario = new SelectJourneysScenario(CommandBus);

            //Act
            await HostedService.StartAsync(CancellationToken.None);
            await selectJourneysScenario.Execute();
            await HostedService.StopAsync(CancellationToken.None);

            var bookingId = selectJourneysScenario.BookingId;
            JourneySelectedConsumer.Origin.Should().Be("MEL");
            JourneySelectedConsumer.Destination.Should().Be("SYD");
        }
    }
}