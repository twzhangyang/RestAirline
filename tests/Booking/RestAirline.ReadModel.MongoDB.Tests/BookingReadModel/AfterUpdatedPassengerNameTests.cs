using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.ReadStores.InMemory;
using FluentAssertions;
using RestAirline.Booking.Queries.MongoDB.Booking;
using RestAirline.TestsHelper.TestScenario;
using Xunit;

namespace RestAirline.ReadModel.MongoDb.Tests.BookingReadModel
{
    [Collection("mongodb read model tests")]
    public class AfterUpdatedPassengerNameTests : TestBase
    {
        public async Task AfterUpdatedPassengerNameShouldUpdateNameToReadModel()
        {
            //Arrange
            var newName = "new-name";
            var updatePassengerNameScenario = new UpdatePassengerNameScenario(CommandBus, newName);
            
            //Act
            await updatePassengerNameScenario.Execute();
            var bookingId = updatePassengerNameScenario.BookingId;
            var passengerKey = updatePassengerNameScenario.PassengerKey;
            
            //Assert
            var query = new BookingIdQuery(bookingId.Value);
            var booking = await QueryProcessor.ProcessAsync(query, CancellationToken.None); 

            var passenger = booking.Passengers.Single(p => p.PassengerKey == passengerKey);
            passenger.Name.Should().Be(newName);
        }
    }
}