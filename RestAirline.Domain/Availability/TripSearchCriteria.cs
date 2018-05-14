using System;
using System.Collections.Generic;
using RestAirline.Domain.Booking;

namespace RestAirline.Domain.Availability
{
    public class TripSearchCriteria
    {
        public string DepartureStation { get; set; }
        
        public DateTime DepartureDateTime { get; set; }

        public string ArriveStation { get; set; }

        public DateTime ArriveDateTime { get; set; }

        public List<Passenger> Passengers { get; set; }
    }
}