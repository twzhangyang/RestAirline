using System;
using Nest;

namespace RestAirline.ReadModel.Elasticsearch.Booking
{
    public class Flight
    {
        [Keyword]
        public Guid Id { get; set; }

        [Keyword]
        public string FlightKey { get; set; }

        [Text]
        public string Number { get; set; }

        [Date]
        public DateTime DepartureDate { get; set; }

        [Text]
        public string DepartureStation { get; set; }

        [Date]
        public DateTime ArriveDate { get; set; }

        [Text]
        public string ArriveStation { get; set; }

        [Number(NumberType.Double)]
        public decimal Price { get; set; }

        [Number(NumberType.Short)]
        public Aircraft Aircraft { get; set; }
    }

    public static class FlightMapper
    {
        public static Flight ToReadModel(this Domain.Booking.Trip.Flight flight)
        {
            var model = new Flight
            {
                Id = Guid.NewGuid(),
                FlightKey = flight.FlightKey,
                Aircraft = (Aircraft) flight.Aircraft,
                Number = flight.Number,
                Price = flight.Price,
                ArriveDate = flight.ArriveDate,
                ArriveStation = flight.ArriveStation,
                DepartureDate = flight.DepartureDate,
                DepartureStation = flight.DepartureStation
            };

            return model;
        }
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