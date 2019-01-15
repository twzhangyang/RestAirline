using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;
using EventFlow.ReadStores;
using RestAirline.Domain.Booking;
using RestAirline.Domain.Booking.Events;
using RestAirline.Domain.Booking.Trip.Events;
using RestAirline.ReadModel.Booking;
using RestAirline.ReadModel.EntityFramework;
using Passenger = RestAirline.ReadModel.Booking.Passenger;

namespace RestAirline.ReadModel.MsSql
{
    public class BookingReadModel : VersionedReadModel,
        IAmReadModelFor<Domain.Booking.Booking, BookingId, JourneysSelectedEvent>,
        IAmReadModelFor<Domain.Booking.Booking, BookingId, PassengerAddedEvent>
    {
        public List<Journey> Journeys { get; private set; }

        public List<Passenger> Passengers { get; private set; }

        public BookingReadModel()
        {
            Journeys = new List<Journey>();
            Passengers = new List<Passenger>();
        }

        public void Apply(IReadModelContext context,
            IDomainEvent<Domain.Booking.Booking, BookingId, JourneysSelectedEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity.Value;

            Journeys = domainEvent.AggregateEvent.Journeys.Select(j=>j.ToReadModel()).ToList();

        }

        public void Apply(IReadModelContext context, IDomainEvent<Domain.Booking.Booking, BookingId, PassengerAddedEvent> domainEvent)
        {
            Passengers.Add(domainEvent.AggregateEvent.Passenger.ToReadModel());
        }
    }
}