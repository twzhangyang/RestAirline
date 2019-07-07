using System.Threading;
using System.Threading.Tasks;
using EventFlow.ReadStores.InMemory;
using FluentAssertions;
using RestAirline.Shared;
using RestAirline.Shared.ModelBuilders;
using RestAirline.TestsHelper;
using RestAirline.TestsHelper.TestScenario;
using Xunit;

namespace RestAirline.ReadModel.Tests.BookingReadModel
{
    public class AfterAddedPassengerTest : TestBase
    {
        private readonly IInMemoryReadStore<ReadModel.BookingReadModel> _bookingReadModel;

        public AfterAddedPassengerTest()
        {
            _bookingReadModel = Resolver.Resolve<IInMemoryReadStore<ReadModel.BookingReadModel>>();
        }

        [Fact]
        public async Task AfterAddedPassengerShouldAddPassengerToReadModel()
        {
            //Arrange
            var addPassengerScenario = new AddPassengerScenario(CommandBus);

            //Act
            await addPassengerScenario.Execute();
            var bookingId = addPassengerScenario.BookingId;

            //Assert
            var booking = await _bookingReadModel.GetAsync(bookingId.Value, CancellationToken.None);
            booking.ReadModel.Passengers.Should().HaveCount(1);
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
            var booking = await _bookingReadModel.GetAsync(bookingId.Value, CancellationToken.None);
            booking.ReadModel.Passengers.Should().HaveCount(2);
        }
    }
}