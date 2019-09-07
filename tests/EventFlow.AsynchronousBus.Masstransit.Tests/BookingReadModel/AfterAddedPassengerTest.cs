using System.Threading;
using System.Threading.Tasks;
using RestAirline.Shared.ModelBuilders;
using RestAirline.TestsHelper.TestScenario;
using Xunit;

namespace EventFlow.AsynchronousBus.MassTransit.Tests.BookingReadModel
{
    public class AfterAddedPassengerTest : TestBase
    {
        public async Task AfterAddedPassengerShouldAddPassengerToReadModel()
        {
            //Arrange
            var addPassengerScenario = new AddPassengerScenario(CommandBus);

            //Act
            await addPassengerScenario.Execute();
            var bookingId = addPassengerScenario.BookingId;

            //Assert
        }
    }
}