using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EventFlow.Aggregates;
using EventFlow.ReadStores;
using RestAirline.Domain.Booking;
using RestAirline.Domain.Booking.Trip.Events;

namespace RestAirline.ReadModel.EntityFramework
{
    public class StationsReadModel : IReadModel,
        IAmReadModelFor<Domain.Booking.Booking, BookingId, JourneysSelectedEvent>
    {
        [Key]
        public string Id { get; protected set; }

        [ConcurrencyCheck] public long Version { get; set; }
        
        public List<StationItem> Items { get; private set; }

        public StationsReadModel()
        {
            Items = new List<StationItem>();
        }

        public void Apply(IReadModelContext context,
            IDomainEvent<Domain.Booking.Booking, BookingId, JourneysSelectedEvent> domainEvent)
        {
            var journeys = domainEvent.AggregateEvent.Journeys;

            Items = journeys.Select(j => new StationItem
            {
                Id = domainEvent.AggregateIdentity.Value,
                DepartureDate = j.DepartureDate,
                DepartureStation = j.DepartureStation,
                ArriveStation = j.ArriveStation
            }).ToList();
        }
    }
}