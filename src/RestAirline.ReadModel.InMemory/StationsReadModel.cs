using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;
using EventFlow.ReadStores;
using RestAirline.Domain.Booking;
using RestAirline.Domain.Booking.Trip.Events;

namespace RestAirline.ReadModel
{
    public class StationsReadModel : IReadModel,
        IAmReadModelFor<Domain.Booking.Booking, BookingId, JourneysSelectedEvent>
    {
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