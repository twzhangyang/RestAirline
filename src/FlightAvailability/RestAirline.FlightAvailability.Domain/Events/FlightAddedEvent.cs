using System;
using EventFlow.Aggregates;
using EventFlow.EventStores;

namespace RestAirline.FlightAvailability.Domain.Events
{
    [EventVersion("FlightAdded", 1)]
    public class FlightAddedEvent : AggregateEvent<FlightAvailability, FlightAvailabilityId>
    {
        public FlightAddedEvent()
        {
        }

        public FlightAddedEvent(Flight flight)
        {
            FlightKey = flight.FlightKey;
            Aircraft = flight.Aircraft;
            Number = flight.Number;
            Price = flight.Price;
            ArriveDate = flight.ArriveDate;
            ArriveStation = flight.ArriveStation;
            DepartureDate = flight.DepartureDate;
            DepartureStation = flight.DepartureStation;
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