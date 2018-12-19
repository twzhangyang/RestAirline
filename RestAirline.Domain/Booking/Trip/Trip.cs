using System.Collections.Generic;
using EventFlow.ValueObjects;

namespace RestAirline.Domain.Booking.Trip
{
    public class Trip : ValueObject
    {
        public List<Journey> Journeys { get; }

        public Trip()
        {
            Journeys = new List<Journey>();
        }
    }
}