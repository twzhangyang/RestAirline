using System;
using RestAirline.Domain.Booking.Trip;

namespace RestAirline.TestsHelper
{
    public class FlightBuilder
    {
        public Flight Build()
        {
            return new Flight
            {
                Aircraft = Aircraft.A320,
                ArriveDate = DateTime.Today.AddDays(2),
                Number = "100",
                Price = 150m,
                ArriveStation = "SYD",
                DepartureDate = DateTime.Today.AddDays(1),
                DepartureStation = "MEL",
                FlightKey = Guid.NewGuid().ToString()
            };
        }
    }
}