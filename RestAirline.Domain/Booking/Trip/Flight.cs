using System;
using EventFlow.ValueObjects;

namespace RestAirline.Domain.Booking.Trip
{
    public class Flight : ValueObject
    {
        public string FlightKey { get; }
        
        public string Number { get; }

        public DateTime DepartureDate { get; }

        public string DepartureStation { get; }

        public DateTime ArriveDate { get; }

        public string ArriveStation { get; }

        public decimal Price { get; }

        public Aircraft Aircraft { get; }
    }
    
    public enum Aircraft
    {
        A320,
        A380,
        Boeing707,
        Boeing717,
        Boeing737
    }
}