using System;
using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using RestAirline.FlightAvailability.Commands;
using RestAirline.FlightAvailability.Domain;
using RestAirline.Shared.Testing;

namespace RestAirline.FlightAvailability.TestsHelper
{
    public class AddFlightScenario : ScenarioBase<FlightAvailabilityId>
    {
        public AddFlightScenario(ICommandBus commandBus) : base(commandBus)
        {
        }

        public override async Task Execute()
        {
            var id = FlightAvailabilityId.New;
            var command = new AddFlightCommand(id)
            {
                Aircraft = Aircraft.A320,
                Number = "FD320",
                Price = 120.00m,
                ArriveDate = DateTime.Now.AddDays(2),
                ArriveStation = "SYD",
                DepartureDate = DateTime.Now,
                DepartureStation = "MEL",
                FlightKey = Guid.NewGuid().ToString()
            };

            await CommandBus.PublishAsync(command, CancellationToken.None);
        }
    }
}