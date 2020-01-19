using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using EventFlow.Aggregates.ExecutionResults;
using RestAirline.FlightAvailability.Commands;
using RestAirline.FlightAvailability.Domain;

namespace RestAirline.FlightAvailability.Management
{
    public class FlightsScheduler
    {
        private readonly ICommandBus _commandBus;
        private readonly Guid _id = Guid.Parse("6751f3c6-8e4e-4a76-be09-2f9351169a4f");

        public FlightsScheduler(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        public Task<IExecutionResult>[] AddFlights =>
            Enumerable.Range(1, 10)
                .Select(x => _commandBus.PublishAsync(Create(x), CancellationToken.None))
                .ToArray();

        private AddFlightCommand Create(int id)
        {
            var flightAvailabilityId = FlightAvailabilityId.With(_id);
            var command = new AddFlightCommand(flightAvailabilityId)
            {
                Aircraft = Aircraft.A320,
                Number = "FL500",
                Price = (id + 100),
                ArriveDate = DateTime.Now.AddDays(2),
                ArriveStation = "SYD",
                DepartureDate = DateTime.Now,
                DepartureStation = "MEL",
                FlightKey = Guid.NewGuid().ToString()
            };

            return command;
        }
    }
}