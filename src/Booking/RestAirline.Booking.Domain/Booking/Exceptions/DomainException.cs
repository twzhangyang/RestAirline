using System;
using EventFlow.Aggregates;
using RestAirline.Booking.Domain.Booking.Trip;

namespace RestAirline.Booking.Domain.Booking.Exceptions
{
    public class DomainException : ApplicationException
    {
        public DomainException(string message) : base(message)
        {
        }
    }
}