using System;
using System.Collections.Generic;
using EventFlow.ReadStores;
using Nest;

namespace RestAirline.FlightAvailability.ReadModel.Elasticsearch
{
    public class FlightAvailabilityReadModel : IReadModel
    {
        [Keyword]
        [PropertyName("_id")]
        public string Id { get; set; }
        
        [Nested]
        public List<Flight> Flights { get; set; }
    }
}