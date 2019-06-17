using System.Collections.Generic;
using EventFlow.Aggregates;
using EventFlow.EventStores;

namespace RestAirline.Domain.Booking.Trip.Events
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