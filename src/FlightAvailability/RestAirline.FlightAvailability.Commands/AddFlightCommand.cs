using System;
using EventFlow.Commands;
using RestAirline.FlightAvailability.Domain;

namespace RestAirline.FlightAvailability.Commands
{
    public class AddFlightCommand : Command<Domain.FlightAvailability, FlightAvailabilityId>
    {
        public AddFlightCommand(FlightAvailabilityId aggregateId) : base(aggregateId)
        {
            
        }

        public string FlightKey { get; set; }

        public string Number { get; set; }

        public DateTime DepartureDate { get; set; }

        public string DepartureStation { get; set; }

        public DateTime ArriveDate { get; set; }

        public string ArriveStation { get; set; }

        public decimal Price { get; set; }

        public Aircraft Aircraft { get; set; }
    }
}