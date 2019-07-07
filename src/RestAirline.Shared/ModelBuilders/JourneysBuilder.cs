using System;
using System.Collections.Generic;
using RestAirline.Domain.Booking.Trip;

namespace RestAirline.Shared.ModelBuilders
{
    public class JourneysBuilder
    {
        public List<Journey> BuildJourneys()
        {
            return new List<Journey> {Build()};
        }

        public Journey Build()
        {
            var flight = new FlightBuilder().Build();

            return new Journey
            {
                DepartureDate = flight.DepartureDate,
                Flight = flight,
                ArriveStation = flight.ArriveStation,
                DepartureStation = flight.DepartureStation,
                ArriveDate = flight.ArriveDate,
                Description = "description",
                JourneyKey = Guid.NewGuid().ToString()
            };
        }
    }
}