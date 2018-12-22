using System.Collections.Generic;
using EventFlow.Aggregates;
using EventFlow.ReadStores;
using RestAirline.Domain.Booking;
using RestAirline.Domain.Booking.Trip;
using RestAirline.Domain.Booking.Trip.Events;

namespace RestAirline.ReadModel
{
    public class BookingReadModel : IReadModel,
        IAmReadModelFor<Booking, BookingId, JourneysSelectedEvent>
    {
        public BookingId Id { get; set; }
        
        public List<Journey> Journeys { get; private set; }
        
        public void Apply(IReadModelContext context, IDomainEvent<Booking, BookingId, JourneysSelectedEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity;

            Journeys = domainEvent.AggregateEvent.Journeys;
        }
    }
}