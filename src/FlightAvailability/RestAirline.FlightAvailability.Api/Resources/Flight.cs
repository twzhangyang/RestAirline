using System;
using System.Collections.Generic;
using System.Linq;
using RestAirline.FlightAvailability.ReadModel.Elasticsearch;

namespace RestAirline.FlightAvailability.Api.Resources
{
    public class Flight  
    {
        public string FlightKey { get; set; }

        public string Number { get;  set; }

        public DateTime DepartureDate { get; set; }

        public string DepartureStation { get; set; }

        public DateTime ArriveDate { get; set; }

        public string ArriveStation { get; set; }

        public decimal Price { get; set; }

        public Aircraft Aircraft { get; set; }
    }

    public static class AvailabilityMapper
    {
        public static List<Flight> ToFlights(this List<FlightAvailabilityReadModel> flightAvailabilityReadModels)
        {
            return flightAvailabilityReadModels.Select(ToFlight).ToList();
        }
        
        private static Flight ToFlight(this FlightAvailabilityReadModel flightAvailabilityReadModel)
        {
            return new Flight
            {
                DepartureStation = flightAvailabilityReadModel.DepartureStation,
                DepartureDate = flightAvailabilityReadModel.DepartureDate,
                Number = flightAvailabilityReadModel.Number,
                Price = flightAvailabilityReadModel.Price,
                ArriveDate = flightAvailabilityReadModel.ArriveDate,
                ArriveStation = flightAvailabilityReadModel.ArriveStation,
                FlightKey = flightAvailabilityReadModel.FlightKey,
                Aircraft = flightAvailabilityReadModel.Aircraft 
            };
        }
    }
}