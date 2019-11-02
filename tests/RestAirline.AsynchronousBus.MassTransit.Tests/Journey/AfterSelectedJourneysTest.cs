using System.Threading;
using System.Threading.Tasks;
using RestAirline.TestsHelper.TestScenario;
using Xunit;

namespace RestAirline.AsynchronousBus.MassTransit.Tests.Journey
{
    public class AfterSelectedJourneysTest : TestBase
    {
        public async Task AfterSelectedJourneysShouldAddJourneysToReadModel()
        {
            //Arrange
            var selectJourneysScenario = new SelectJourneysScenario(CommandBus);

            //Act
            await HostedService.StartAsync(CancellationToken.None);
            await selectJourneysScenario.Execute();
            await HostedService.StopAsync(CancellationToken.None);

            var bookingId = selectJourneysScenario.BookingId;
        }
    }
}