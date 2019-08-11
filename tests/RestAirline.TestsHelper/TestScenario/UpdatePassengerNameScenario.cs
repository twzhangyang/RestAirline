using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RestAirline.Commands.Passenger;

namespace RestAirline.TestsHelper.TestScenario
{
    public class UpdatePassengerNameScenario : ScenarioBase
    {
        private readonly string _name;

        public string NewName { get; private set; }
        
        public string PassengerKey { get; private set; }

        public UpdatePassengerNameScenario(ICommandBus commandBus,
            string name = null) : base(commandBus)
        {
            _name = name;
        }

        public override async Task Execute()
        {
            var addPassengerScenario = new AddPassengerScenario(CommandBus);
            await addPassengerScenario.Execute();
            BookingId = addPassengerScenario.BookingId;

            NewName = _name ?? "newName";
            PassengerKey = "1";
            var command = new UpdatePassengerNameCommand(BookingId, PassengerKey, NewName);

            await CommandBus.PublishAsync(command, CancellationToken.None);
        }
    }
}