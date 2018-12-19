using System;
using EventFlow.ValueObjects;

namespace RestAirline.Domain.Booking.Trip
{
    public class Journey : ValueObject
    {
        public DateTime DepartureDate { get; }

        public string DepartureStation { get; }

        public DateTime ArriveDate { get; }

        public string ArriveStation { get; }

        public string Description { get; }

        public Flight Flight { get; }
    }
}