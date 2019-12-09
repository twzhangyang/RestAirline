using System.Collections.Generic;
using EventFlow.Aggregates;
using RestAirline.FlightAvailability.Domain.Events;

namespace RestAirline.FlightAvailability.Domain
{
    public class FlightAvailabilityState : AggregateState<FlightAvailability, FlightAvailabilityId, FlightAvailabilityState>,
        IApply<FlightAddedEvent>
    {
        public IReadOnlyList<Flight> Flights => _flights.AsReadOnly();

        private readonly List<Flight> _flights;

        public FlightAvailabilityState()
        {
            _flights = new List<Flight>();
        }
        
        public void Apply(FlightAddedEvent aggregateEvent)
        {
           _flights.Add(aggregateEvent.ToModel()); 
        }
    }
}