using System;
using EventFlow.Aggregates;
using EventFlow.ReadStores;
using Nest;
using RestAirline.FlightAvailability.Domain;
using RestAirline.FlightAvailability.Domain.Events;

namespace RestAirline.FlightAvailability.ReadModel.Elasticsearch
{
    [ElasticsearchType(IdProperty = "Id", Name = "flightAvailability")]
    public class FlightAvailabilityReadModel : IReadModel,
        IAmReadModelFor<Domain.FlightAvailability,FlightAvailabilityId, FlightAddedEvent>
    {
        public string Id { get; set; }
        
        public string FlightKey { get; set; }

        public string Number { get;  set; }

        public DateTime DepartureDate { get; set; }

        public string DepartureStation { get; set; }

        public DateTime ArriveDate { get; set; }

        public string ArriveStation { get; set; }

        public decimal Price { get; set; }

        public Aircraft Aircraft { get; set; }
        
        public void Apply(IReadModelContext context, IDomainEvent<Domain.FlightAvailability, FlightAvailabilityId, FlightAddedEvent> domainEvent)
        {
            var evt = domainEvent.AggregateEvent;
            FlightKey = evt.FlightKey;
            Number = evt.Number;
            DepartureDate = evt.DepartureDate;
            DepartureStation = evt.DepartureStation;
            ArriveDate = evt.ArriveDate;
            ArriveStation = evt.ArriveStation;
            Price = evt.Price;
            Aircraft = (Aircraft)evt.Aircraft;
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