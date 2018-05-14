using System;
using System.Collections.Generic;
using System.Linq;
using RestAirline.Domain.Booking;
using RestAirline.Domain.Shared;

namespace RestAirline.Domain.Availability
{
    public class TripAvailability
    {
        public List<Trip> SearchTrips(TripSearchCriteria searchCriteria)
        {
            //Validation for search criteria, such as passenger count etc...

            return CreateFakedTrips();
        }

        private List<Trip> CreateFakedTrips()
        {
            return Enumerable.Range(1, 10).Select(CreateTrip).ToList();
        }

        private Trip CreateTrip(int number)
        {
            var trip = new Trip()
            {
                Journeys = new List<Trip.Journey>()
                {
                    new Trip.Journey()
                    {
                        DepartureDate = DateTime.Now.AddDays(number),
                        DepartureStation = "MEL",
                        ArriveDate = DateTime.Now.AddDays(1+number),
                        ArriveStation = "SYD",
                        Id = Guid.NewGuid(),
                        Flight = new Flight()
                        {
                            ArriveStation = "SYD",
                            ArriveDate = DateTime.Now.AddDays(1+number),
                            Aircraft = Aircraft.A320,
                            Number = "JQ100",
                            Price = 100
                        }
                    }
                }
            };

            return trip;
        }
    }
}