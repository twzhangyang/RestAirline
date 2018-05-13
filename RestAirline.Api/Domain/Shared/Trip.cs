using System;
using System.Collections.Generic;

namespace RestAirline.Api.Domain.Shared
{
    public class Trip
    {

        public List<Journey> Journeys { get; set; }

        public Trip()
        {
            Journeys = new List<Journey>();
        }

        public class Journey
        {
            public DateTime DepartureDate { get; set; }

            public string DepartureStation { get; set; }

            public DateTime ArriveDate { get; set; }

            public string ArriveStation { get; set; }

            public decimal Price { get; set; }

            public string Description { get; set; }
        }
    }
}