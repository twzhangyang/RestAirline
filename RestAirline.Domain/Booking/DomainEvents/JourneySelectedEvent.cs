using System.Collections.Generic;
using EventFlow.Aggregates;
using EventFlow.EventStores;
using RestAirline.Domain.Booking.Trip;

namespace RestAirline.Domain.Booking.DomainEvents
{
    [EventVersion("JourneySelected", 1)]
    public class JourneySelectedEvent : AggregateEvent<Booking, BookingId>
    {
        public JourneySelectedEvent(List<Journey> journeys)
        {
            Journeys = journeys;
        }
        
        public List<Journey> Journeys { get; }
    }
}