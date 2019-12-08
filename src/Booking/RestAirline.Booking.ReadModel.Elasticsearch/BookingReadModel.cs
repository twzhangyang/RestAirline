using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;
using EventFlow.ReadStores;
using Nest;
using RestAirline.Booking.ReadModel.Elasticsearch.Booking;
using RestAirline.Booking.Domain.Booking;
using RestAirline.Booking.Domain.Booking.Passenger.Events;
using RestAirline.Booking.Domain.Booking.Trip.Events;
using Passenger = RestAirline.Booking.ReadModel.Elasticsearch.Booking.Passenger;

namespace RestAirline.Booking.ReadModel.Elasticsearch
{
    [ElasticsearchType(IdProperty = "Id", Name = "booking")]
    public class BookingReadModel : IReadModel,
        IAmReadModelFor<Domain.Booking.Booking, BookingId, JourneysSelectedEvent>,
        IAmReadModelFor<Domain.Booking.Booking, BookingId, PassengerAddedEvent>,
        IAmReadModelFor<Domain.Booking.Booking, BookingId, PassengerNameUpdatedEvent>
    {
        public BookingReadModel()
        {
            Passengers = new List<Booking.Passenger>();
            Journeys = new List<Journey>();
        }

        [Keyword] 
        [PropertyName("_id")]
        public string Id { get; protected set; }

        [Nested] public List<Booking.Passenger> Passengers { get; set; }

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