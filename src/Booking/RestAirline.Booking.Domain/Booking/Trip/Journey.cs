using System;
using EventFlow.ValueObjects;

namespace RestAirline.Booking.Domain.Booking.Trip
{
    public class Journey : ValueObject
    {
        public string JourneyKey { get; set; }

        public DateTime DepartureDate { get; set; }

        public string DepartureStation { get; set; }

        public DateTime ArriveDate { get; set; }

        public string ArriveStation { get; set; }

        public string Description { get; set; }

        public Flight Flight { get; set; }
    }
}