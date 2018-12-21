using System.Collections.Generic;
using EventFlow.Aggregates;
using RestAirline.Domain.Booking.DomainEvents;
using RestAirline.Domain.Booking.Trip;

namespace RestAirline.Domain.Booking
{
    public class BookingState : AggregateState<Booking, BookingId, BookingState>,
        IApply<JourneysSelectedEvent>
    {
        public IReadOnlyList<Journey> Journeys => _journeys.AsReadOnly();
        
        private List<Journey> _journeys;
        
        public void Apply(JourneysSelectedEvent aggregateEvent)
        {
            _journeys = aggregateEvent.Journeys;
        }
    }
}