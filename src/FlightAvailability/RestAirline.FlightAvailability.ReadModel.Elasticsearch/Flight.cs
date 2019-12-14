using System;

namespace RestAirline.FlightAvailability.ReadModel.Elasticsearch
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

    public enum Aircraft
    {
        A320,
        A380,
        Boeing707,
        Boeing717,
        Boeing737
    }
}