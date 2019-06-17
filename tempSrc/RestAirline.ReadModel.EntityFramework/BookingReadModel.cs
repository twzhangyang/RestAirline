using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EventFlow.Aggregates;
using EventFlow.ReadStores;
using RestAirline.Domain.Booking;
using RestAirline.Domain.Booking.Events;
using RestAirline.Domain.Booking.Trip.Events;
using RestAirline.ReadModel.EntityFramework.Booking;
using Journey = RestAirline.ReadModel.EntityFramework.Booking.Journey;
using Passenger = RestAirline.ReadModel.EntityFramework.Booking.Passenger;

namespace RestAirline.ReadModel.EntityFramework
{
    public class BookingReadModel : IReadModel,
        IAmReadModelFor<Domain.Booking.Booking, BookingId, JourneysSelectedEvent>,
        IAmReadModelFor<Domain.Booking.Booking, BookingId, PassengerAddedEvent>
    {
        [Key]
        public string Id { get; protected set; }
        
        [ConcurrencyCheck] 
        public long Version { get; set; }
        
        public string DepartureStation { get; set; }
        
        public int PingsReceived { get; set; }
        
        public void Apply(IReadModelContext context,
            IDomainEvent<Domain.Booking.Booking, BookingId, JourneysSelectedEvent> domainEvent)
        {
            DepartureStation = domainEvent.AggregateEvent.Journeys.First().DepartureStation;
        }

        public void Apply(IReadModelContext context, IDomainEvent<Domain.Booking.Booking, BookingId, PassengerAddedEvent> domainEvent)
        {
            PingsReceived++;
        }
    }
}