using System;
using EventFlow.ValueObjects;

namespace RestAirline.Domain.Booking.Trip
{
    public class Journey : ValueObject
    {
        //TODO: used for EF
        public string Id { get; set; }
        
        public string JourneyKey { get; set; }

        public DateTime DepartureDate { get; set; }

        public string DepartureStation { get; set; }

        public DateTime ArriveDate { get; set; }

        public string ArriveStation { get; set; }

        public string Description { get; set; }

        public Flight Flight { get; set; }
    }
}