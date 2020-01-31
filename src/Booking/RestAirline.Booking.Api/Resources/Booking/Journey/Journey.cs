using System;
using System.ComponentModel.DataAnnotations;
using RestAirline.ReadModel.MongoDb;

namespace RestAirline.Booking.Api.Resources.Booking.Journey
{
    public class Journey
    {
        [Key]
        public string Id { get; set; }

        public string JourneyKey { get; set; }

        public DateTime DepartureDate { get; set; }

        public string DepartureStation { get; set; }

        public DateTime ArriveDate { get; set; }

        public string ArriveStation { get; set; }

        public string Description { get; set; }

        public Flight Flight { get; set; }
    }

    public static class JourneyMapper
    {
        public static Journey ToResource(this RestAirline.ReadModel.MongoDb.Booking.Journey journey)
        {
            var model = new Journey
            {
                Id=journey.JourneyKey,
                Flight = journey.Flight.ToResource(),
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