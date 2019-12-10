using System;
using System.Collections.Generic;
using EventFlow.Aggregates;
using EventFlow.Extensions;
using RestAirline.FlightAvailability.Domain.Events;

namespace RestAirline.FlightAvailability.Domain
{
    public class FlightAvailability : AggregateRoot<FlightAvailability, FlightAvailabilityId>
    {
        public IReadOnlyList<Flight> Flights => _state.Flights;
        
        private readonly FlightAvailabilityState _state = new FlightAvailabilityState();
        
        public FlightAvailability(FlightAvailabilityId id) : base(id)
        {
            Register(_state);
        }
        
        public void AddFlight(Flight flight)
        {
            if (flight == null)
            {
                throw new ArgumentNullException($"nameof(flight) is null");
            }
            
            FlightValidationSpecification.Create().ThrowDomainErrorIfNotSatisfied(flight);
            
            Emit(new FlightAddedEvent(flight));
        }
    }
}