using System;
using System.Collections.Generic;

namespace RestAirline.Domain.Booking.Trip
{
    public class Trip
    {
        public List<Journey> Journeys { get; private set; }

        public Trip()
        {
            Journeys = new List<Journey>();
        }

        public class Journey
        {
            public Guid Id { get; private set; }

            public DateTime DepartureDate { get; private set; }

            public string DepartureStation { get; private set; }

            public DateTime ArriveDate { get; private set; }

            public string ArriveStation { get; private set; }

            public string Description { get; private set; }

            public Flight Flight { get; private set; }
        }
    }
}