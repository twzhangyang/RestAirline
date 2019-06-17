using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using RestAirline.CommandHandlers.Passenger;
using RestAirline.Domain.Booking;
using RestAirline.Shared;

namespace RestAirline.TestsHelper.TestScenario
{
    public class AddPassengerScenario : ScenarioBase
    {
        private readonly Passenger _passenger;
        
        public string PassengerKey { get; private set; }

        public AddPassengerScenario(ICommandBus commandBus, BookingId bookingId = null, Passenger passenger = null) :
            base(commandBus)
        {
            CommandBus = commandBus;
            BookingId = bookingId;
            _passenger = passenger;
        }

        public override async Task Execute()
        {
            if (BookingId == null)
            {
                var selectJourneysScenario = new SelectJourneysScenario(CommandBus);
                await selectJourneysScenario.Execute();
                BookingId = selectJourneysScenario.BookingId;
            }
           
            var passenger = _passenger ?? new PassengerBuilder().CreatePassenger();
            var command = new AddPassengerCommand(BookingId, passenger);
            PassengerKey = passenger.PassengerKey;

            CommandBus.PublishAsync(command, CancellationToken.None);
        }
    }
}