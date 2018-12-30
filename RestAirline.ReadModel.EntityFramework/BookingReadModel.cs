using System.Collections.Generic;
using EventFlow.Aggregates;
using EventFlow.ReadStores;
using RestAirline.Domain.Booking;
using RestAirline.Domain.Booking.Events;
using RestAirline.Domain.Booking.Trip;
using RestAirline.Domain.Booking.Trip.Events;

namespace RestAirline.ReadModel.EntityFramework
{
    public class BookingReadModel : VersionedReadModel,
        IAmReadModelFor<Booking, BookingId, JourneysSelectedEvent>,
        IAmReadModelFor<Booking, BookingId, PassengerAddedEvent>
    {
        public List<Journey> Journeys { get; private set; }

        public List<Passenger> Passengers { get; private set; }

        public BookingReadModel()
        {
            Journeys = new List<Journey>();
            Passengers = new List<Passenger>();
        }

        public void Apply(IReadModelContext context,
            IDomainEvent<Booking, BookingId, JourneysSelectedEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity.Value;

            Journeys = domainEvent.AggregateEvent.Journeys;
        }


        public void Apply(IReadModelContext context, IDomainEvent<Booking, BookingId, PassengerAddedEvent> domainEvent)
        {
            Passengers.Add(domainEvent.AggregateEvent.Passenger);
        }
    }
}