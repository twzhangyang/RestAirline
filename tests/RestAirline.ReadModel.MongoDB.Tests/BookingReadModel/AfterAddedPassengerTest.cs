using System.Threading;
using System.Threading.Tasks;
using EventFlow.ReadStores.InMemory;
using FluentAssertions;
using RestAirline.Queries.MongoDB.Booking;
using RestAirline.Shared.ModelBuilders;
using RestAirline.TestsHelper.TestScenario;
using Xunit;

namespace RestAirline.ReadModel.MongoDb.Tests.BookingReadModel
{
    public class AfterAddedPassengerTest : TestBase
    {

        [Fact]
        public async Task AfterAddedPassengerShouldAddPassengerToReadModel()
        {
            //Arrange
            var addPassengerScenario = new AddPassengerScenario(CommandBus);

            //Act
            await addPassengerScenario.Execute();
            var bookingId = addPassengerScenario.BookingId;

            //Assert
            var query = new BookingIdQuery(bookingId.Value);
            var booking = await QueryProcessor.ProcessAsync(query, CancellationToken.None); 

            booking.Passengers.Should().HaveCount(1);
        }

        [Fact]
        public async Task AfterAddedPassengerTwiceShouldAddTwoPassengersToReadModel()
        {
            //Arrange
            var addPassengerScenario = new AddPassengerScenario(CommandBus);
            await addPassengerScenario.Execute();
            var bookingId = addPassengerScenario.BookingId;

            //Act
            var p = new PassengerBuilder().CreatePassenger(x => { x.Name = "AnotherYang"; });
            var addAnotherPassengerScenario = new AddPassengerScenario(CommandBus, bookingId, p);
            await addAnotherPassengerScenario.Execute();

            //Assert
            var query = new BookingIdQuery(bookingId.Value);
            var booking = await QueryProcessor.ProcessAsync(query, CancellationToken.None); 

            booking.Passengers.Should().HaveCount(2);
        }
    }
}