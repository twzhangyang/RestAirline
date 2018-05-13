using System;
using System.Collections.Generic;
using System.Linq;

namespace RestAirline.Domain.Shared
{
    public class Trip
    {
        public List<Journey> Journeys { get; set; }

        public Trip()
        {
            Journeys = new List<Journey>();
        }

        public void ChangeFlight(Guid journeyId, Flight flight)
        {
            //Validation in here

            var journey = Journeys.Single(j => j.Id == journeyId);
            journey.ChangeFlight(flight);
        }

        public class Journey
        {
            public Guid Id { get; set; }

            public DateTime DepartureDate { get; set; }

            public string DepartureStation { get; set; }

            public DateTime ArriveDate { get; set; }

            public string ArriveStation { get; set; }

            public string Description { get; set; }

            public Flight Flight { get; private set; }

            public void ChangeFlight(Flight flight)
            {
                Flight = flight;
            }
        }
    }
}