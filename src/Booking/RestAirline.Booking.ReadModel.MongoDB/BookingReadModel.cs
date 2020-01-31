using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;
using EventFlow.MongoDB.ReadStores;
using EventFlow.ReadStores;
using RestAirline.Booking.Domain.Booking;
using RestAirline.Booking.Domain.Booking.Passenger.Events;
using RestAirline.Booking.Domain.Booking.Trip.Events;
using RestAirline.ReadModel.MongoDb.Booking;
using Journey = RestAirline.ReadModel.MongoDb.Booking.Journey;
using Passenger = RestAirline.ReadModel.MongoDb.Booking.Passenger;

namespace RestAirline.ReadModel.MongoDb
{
    public class BookingReadModel : IMongoDbReadModel,
        IAmReadModelFor<RestAirline.Booking.Domain.Booking.Booking, BookingId, JourneysSelectedEvent>,
        IAmReadModelFor<RestAirline.Booking.Domain.Booking.Booking, BookingId, PassengerAddedEvent>,
        IAmReadModelFor<RestAirline.Booking.Domain.Booking.Booking, BookingId, PassengerNameUpdatedEvent>
    {
        public BookingReadModel()
        {
            Passengers = new List<Passenger>();
            Journeys = new List<Journey>();
        }

        public string Id { get; protected set; }

        public long? Version { get; set; }

        public List<Passenger> Passengers { get; set; }

        public List<Journey> Journeys { get; set; }

        public void Apply(IReadModelContext context,
            IDomainEvent<RestAirline.Booking.Domain.Booking.Booking, BookingId, JourneysSelectedEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity.Value;
            var journeys = domainEvent.AggregateEvent.Journeys.Select(j => j.ToReadModel());

            Journeys = journeys.ToList();
        }

        public void Apply(IReadModelContext context,
            IDomainEvent<RestAirline.Booking.Domain.Booking.Booking, BookingId, PassengerAddedEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity.Value;
            Passengers.Add(domainEvent.AggregateEvent.Passenger.ToReadModel());
        }

        public void Apply(IReadModelContext context,
            IDomainEvent<RestAirline.Booking.Domain.Booking.Booking, BookingId, PassengerNameUpdatedEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity.Value;

            var passenger = Passengers.Single(x => x.PassengerKey == domainEvent.AggregateEvent.PassengerKey);
            passenger.Name = domainEvent.AggregateEvent.Name;
        }
    }
}