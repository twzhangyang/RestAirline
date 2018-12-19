using System.Collections.Generic;
using EventFlow.Aggregates;
using RestAirline.Domain.Booking.DomainEvents;
using RestAirline.Domain.Booking.Trip;

namespace RestAirline.Domain.Booking
{
    public class BookingState : AggregateState<Booking, BookingId, BookingState>,
        IApply<JourneySelectedEvent>
    {
        public IReadOnlyList<Journey> Journeys => _journeys.AsReadOnly();
        
        private List<Journey> _journeys;
        
        public void Apply(JourneySelectedEvent aggregateEvent)
        {
            _journeys = aggregateEvent.Journeys;
        }
    }
}