using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;
using EventFlow.ReadStores;
using RestAirline.Domain.Booking;
using RestAirline.Domain.Booking.Trip.Events;

namespace RestAirline.ReadModel.EntityFramework
{
    public class StationsReadModel : VersionedReadModel,
        IAmReadModelFor<Booking, BookingId, JourneysSelectedEvent>
    {
        public List<Item> Items { get; private set; }

        public StationsReadModel()
        {
            Items = new List<Item>();
        }

        public void Apply(IReadModelContext context,
            IDomainEvent<Booking, BookingId, JourneysSelectedEvent> domainEvent)
        {
            var journeys = domainEvent.AggregateEvent.Journeys;

            Items = journeys.Select(j => new Item
            {
                Id = domainEvent.AggregateIdentity.Value,
                DepartureDate = j.DepartureDate,
                DepartureStation = j.DepartureStation,
                ArriveStation = j.ArriveStation
            }).ToList();
        }

        public class Item
        {
            public string Id { get; set; }
            
            public DateTime DepartureDate { get; set; }

            public string DepartureStation { get; set; }

            public string ArriveStation { get; set; }
        }
    }
}