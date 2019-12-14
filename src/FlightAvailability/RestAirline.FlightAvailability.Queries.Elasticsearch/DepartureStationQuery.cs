using System.Collections.Generic;
using EventFlow.Queries;
using RestAirline.FlightAvailability.ReadModel.Elasticsearch;

namespace RestAirline.FlightAvailability.Queries.Elasticsearch
{
    public class DepartureStationQuery : IQuery<List<FlightAvailabilityReadModel>>
    {
        public string Departure { get; }

        public DepartureStationQuery(string departure)
        {
            Departure = departure;
        }
    }
}