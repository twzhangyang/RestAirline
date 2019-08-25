using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;
using EventFlow.ReadStores;
using Nest;
using RestAirline.Domain.Booking;
using RestAirline.Domain.Booking.Passenger.Events;
using RestAirline.Domain.Booking.Trip.Events;
using RestAirline.ReadModel.Elasticsearch.Booking;
using Passenger = RestAirline.ReadModel.Elasticsearch.Booking.Passenger;

namespace RestAirline.ReadModel.Elasticsearch
{
    [ElasticsearchType(IdProperty = "Id", Name = "booking")]
    public class BookingReadModel : IReadModel,
        IAmReadModelFor<Domain.Booking.Booking, BookingId, JourneysSelectedEvent>,
        IAmReadModelFor<Domain.Booking.Booking, BookingId, PassengerAddedEvent>,
        IAmReadModelFor<Domain.Booking.Booking, BookingId, PassengerNameUpdatedEvent>
    {
        public BookingReadModel()
        {
            Passengers = new List<Passenger>();
            Journeys = new List<Journey>();
        }

        [Keyword(Index = true)] public string Id { get; protected set; }

        [Nested] public List<Passenger> Passengers { get; set; }

        [Nested] public List<Journey> Journeys { get; set; }

        public void Apply(IReadModelContext context,
            IDomainEvent<Domain.Booking.Booking, BookingId, JourneysSelectedEvent> domainEvent)
        {
            var journeys = domainEvent.AggregateEvent.Journeys.Select(j => j.ToReadModel());

            Journeys = journeys.ToList();
        }

        public void Apply(IReadModelContext context,
            IDomainEvent<Domain.Booking.Booking, BookingId, PassengerAddedEvent> domainEvent)
        {
            Passengers.Add(domainEvent.AggregateEvent.Passenger.ToReadModel());
        }

        public void Apply(IReadModelContext context,
            IDomainEvent<Domain.Booking.Booking, BookingId, PassengerNameUpdatedEvent> domainEvent)
        {
            var passenger = Passengers.Single(x => x.PassengerKey == domainEvent.AggregateEvent.PassengerKey);

            passenger.Name = domainEvent.AggregateEvent.Name;
        }
    }
}