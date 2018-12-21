using System.Collections.Generic;
using EventFlow.Aggregates;
using EventFlow.EventStores;
using RestAirline.Domain.Booking.Trip;

namespace RestAirline.Domain.Booking.DomainEvents
{
    [EventVersion("JourneysSelected", 1)]
    public class JourneysSelectedEvent : AggregateEvent<Booking, BookingId>
    {
        public JourneysSelectedEvent(List<Journey> journeys)
        {
            Journeys = journeys;
        }
        
        public List<Journey> Journeys { get; }
    }
}