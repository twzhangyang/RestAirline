using System;
using Nest;

namespace RestAirline.Booking.ReadModel.Elasticsearch.Booking
{
    public class Journey
    {
        [Keyword] public Guid Id { get; set; }

        [Keyword] public string JourneyKey { get; set; }

        [Date] public DateTime DepartureDate { get; set; }

        [Text] public string DepartureStation { get; set; }

        [Date] public DateTime ArriveDate { get; set; }

        [Text] public string ArriveStation { get; set; }

        [Text] public string Description { get; set; }

        [Nested] public Flight Flight { get; set; }
    }

    public static class JourneyMapper
    {
        public static Journey ToReadModel(this Domain.Booking.Trip.Journey journey)
        {
            var model = new Journey
            {
                Id = Guid.NewGuid(),
                Flight = journey.Flight.ToReadModel(),
                ArriveDate = journey.ArriveDate,
                DepartureDate = journey.DepartureDate,
                ArriveStation = journey.ArriveStation,
                DepartureStation = journey.DepartureStation,
                Description = journey.Description,
                JourneyKey = journey.JourneyKey
            };

            return model;
        }
    }
}