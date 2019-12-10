using System;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;
using RestAirline.FlightAvailability.Commands;
using RestAirline.FlightAvailability.Domain;

namespace RestAirline.FlightAvailability.CommandHandlers
{
    public class AddFlightCommandHandler : CommandHandler<Domain.FlightAvailability,FlightAvailabilityId, AddFlightCommand>
    {
        public override Task ExecuteAsync(Domain.FlightAvailability aggregate, AddFlightCommand command, CancellationToken cancellationToken)
        { 
            aggregate.AddFlight(ToFlight(command));
            
            return Task.CompletedTask;
        }

        private Flight ToFlight(AddFlightCommand command)
        {
            return new Flight
            {
                Aircraft = command.Aircraft,
                Number = command.Number,
                Price = command.Price,
                ArriveDate = command.ArriveDate,
                ArriveStation = command.ArriveStation,
                DepartureDate = command.DepartureDate,
                DepartureStation = command.DepartureStation,
                FlightKey = command.FlightKey
            };
        }
    }
}