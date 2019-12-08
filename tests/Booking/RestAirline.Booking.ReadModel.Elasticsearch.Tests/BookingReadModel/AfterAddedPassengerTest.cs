using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using RestAirline.Booking.Queries.Elasticsearch.Booking;
using RestAirline.Shared.ModelBuilders;
using RestAirline.TestsHelper.TestScenario;
using Xunit;

namespace RestAirline.Booking.ReadModel.Elasticsearch.Tests.BookingReadModel
{
    [Collection("elasticsearch read model tests")]
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
            var query = new BookingIdQuery(bookingId.Value);
            var booking = await QueryProcessor.ProcessAsync(query, CancellationToken.None); 

            booking.Passengers.Should().HaveCount(1);
        }

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