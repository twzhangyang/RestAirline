using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;
using EventFlow.ReadStores;
using RestAirline.Domain.Booking;
using RestAirline.Domain.Booking.Events;
using RestAirline.Domain.Booking.Trip.Events;
using RestAirline.ReadModel.InMemory.Booking;
using Passenger = RestAirline.ReadModel.InMemory.Booking.Passenger;

namespace RestAirline.ReadModel.InMemory
{
    public class BookingReadModel : IReadModel,
        IAmReadModelFor<Domain.Booking.Booking, BookingId, JourneysSelectedEvent>,
        IAmReadModelFor<Domain.Booking.Booking, BookingId, PassengerAddedEvent>,
        IAmReadModelFor<Domain.Booking.Booking, BookingId, PassengerNameUpdatedEvent>
    {
        public BookingId Id { get; private set; }

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