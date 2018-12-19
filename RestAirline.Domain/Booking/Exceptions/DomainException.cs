using System;
using EventFlow.Aggregates;
using RestAirline.Domain.Booking.Trip;

namespace RestAirline.Domain.Booking.Exceptions
{
    public class DomainException : ApplicationException
    {
        public DomainException(string message) : base(message)
        {
        }
    }

    public class AggregateIsNotNewException : DomainException
    {
        public AggregateIsNotNewException(IAggregateRoot obj) : base(
            "'{obj.Name}' with ID '{obj.GetIdentity()}' is not new")
        {
        }
    }

    public class DepartureDateTimeIsLessThanNowException : DomainException
    {
        public DepartureDateTimeIsLessThanNowException(Journey journey) 
            : base($"Departure time of Jouney {journey.JourneyKey} is less than now")
        {
        }
    }
}