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
}