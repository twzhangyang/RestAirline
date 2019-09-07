using System.Threading;
using System.Threading.Tasks;
using RestAirline.TestsHelper.TestScenario;
using Xunit;

namespace EventFlow.AsynchronousBus.MassTransit.Tests.BookingReadModel
{
    public class AfterSelectedJourneysTest : TestBase
    {
        public async Task AfterSelectedJourneysShouldAddJourneysToReadModel()
        {
            //Arrange
            var selectJourneysScenario = new SelectJourneysScenario(CommandBus);

            //Act
            await selectJourneysScenario.Execute();
            var bookingId = selectJourneysScenario.BookingId;
        }
    }
}