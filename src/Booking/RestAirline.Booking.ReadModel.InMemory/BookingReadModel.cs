using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;
using EventFlow.ReadStores;
using RestAirline.Booking.ReadModel.InMemory.Booking;
using RestAirline.Booking.Domain.Booking;
using RestAirline.Booking.Domain.Booking.Passenger.Events;
using RestAirline.Booking.Domain.Booking.Trip.Events;
using Passenger = RestAirline.Booking.ReadModel.InMemory.Booking.Passenger;

namespace RestAirline.Booking.ReadModel.InMemory
{
    public class BookingReadModel : IReadModel,
        IAmReadModelFor<Domain.Booking.Booking, BookingId, JourneysSelectedEvent>,
        IAmReadModelFor<Domain.Booking.Booking, BookingId, PassengerAddedEvent>,
        IAmReadModelFor<Domain.Booking.Booking, BookingId, PassengerNameUpdatedEvent>
    {
        public BookingId Id { get; private set; }

        public List<Journey> Journeys { get; private set; }

        public List<Booking.Passenger> Passengers { get; private set; }

        public BookingReadModel()
        {
            Journeys = new List<Journey>();
            Passengers = new List<Booking.Passenger>();
        }

        public void Apply(IReadModelContext context,
            IDomainEvent<Domain.Booking.Booking, BookingId, JourneysSelectedEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity;

            Journeys = domainEvent.AggregateEvent.Journeys.Select(j=>j.ToReadModel()).ToList();
        }


        public void Apply(IReadModelContext context, IDomainEvent<Domain.Booking.Booking, BookingId, PassengerAddedEvent> domainEvent)
        {
            Passengers.Add(domainEvent.AggregateEvent.Passenger.ToReadModel());
        }
        
        public void Apply(IReadModelContext context, IDomainEvent<Domain.Booking.Booking, BookingId, PassengerNameUpdatedEvent> domainEvent)
        {
            var passenger = Passengers.Single(p => p.PassengerKey == domainEvent.AggregateEvent.PassengerKey);
            passenger.Name = domainEvent.AggregateEvent.Name;
        }
    }
}