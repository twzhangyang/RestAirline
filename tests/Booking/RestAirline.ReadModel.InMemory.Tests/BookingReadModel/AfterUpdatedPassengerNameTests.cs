using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.ReadStores.InMemory;
using FluentAssertions;
using RestAirline.TestsHelper.TestScenario;
using Xunit;

namespace RestAirline.ReadModel.Tests.BookingReadModel
{
    public class AfterUpdatedPassengerNameTests : TestBase
    {
        private readonly IInMemoryReadStore<InMemory.BookingReadModel> _bookingReadModel;

        public AfterUpdatedPassengerNameTests()
        {
            _bookingReadModel = Resolver.Resolve<IInMemoryReadStore<InMemory.BookingReadModel>>();
        }

        [Fact]
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
            var booking = await _bookingReadModel.GetAsync(bookingId.Value, CancellationToken.None);
            var passenger = booking.ReadModel.Passengers.Single(p => p.PassengerKey == passengerKey);
            passenger.Name.Should().Be(newName);
        }
    }
}