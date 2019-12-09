using System;
using EventFlow.Aggregates;
using EventFlow.EventStores;

namespace RestAirline.FlightAvailability.Domain.Events
{
    [EventVersion("FLightAdded", 1)]
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
        
        public string FlightKey { get; private set; }

        public string Number { get; private set; }

        public DateTime DepartureDate { get; private set; }

        public string DepartureStation { get; private set; }

        public DateTime ArriveDate { get; private set; }

        public string ArriveStation { get; private set; }

        public decimal Price { get; private set; }

        public Aircraft Aircraft { get; private set; }
    }
}