using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EventFlow.Aggregates;
using EventFlow.ReadStores;
using RestAirline.Booking.Domain.Booking;
using RestAirline.Booking.Domain.Booking.Passenger.Events;
using RestAirline.Booking.Domain.Booking.Trip.Events;
using RestAirline.Booking.ReadModel.EntityFramework.Booking;
using Journey = RestAirline.Booking.ReadModel.EntityFramework.Booking.Journey;
using Passenger = RestAirline.Booking.ReadModel.EntityFramework.Booking.Passenger;

namespace RestAirline.Booking.ReadModel.EntityFramework
{
    public class BookingReadModel : IReadModel,
        IAmReadModelFor<Domain.Booking.Booking, BookingId, JourneysSelectedEvent>,
        IAmReadModelFor<Domain.Booking.Booking, BookingId, PassengerAddedEvent>,
        IAmReadModelFor<Domain.Booking.Booking, BookingId, PassengerNameUpdatedEvent>
    {
        public BookingReadModel()
        {
            Passengers = new List<Booking.Passenger>();
            Journeys = new List<Booking.Journey>();
        }

        [Key] public string Id { get; protected set; }

        [ConcurrencyCheck] public long Version { get; set; }

        public List<Booking.Passenger> Passengers { get; set; }

        public List<Booking.Journey> Journeys { get; set; }

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
            //TODO: Not sure why there is not element in passenger list
//            var passenger = Passengers.Single(x => x.PassengerKey == domainEvent.AggregateEvent.PassengerKey);

//            passenger.Name = domainEvent.AggregateEvent.Name;
        }
    }
}