using System;
using RestAirline.Api.Domain.Shared;

namespace RestAirline.Api.Domain.Booking.Exceptions
{
    public class ChangingFlightIneligibleException : Exception
    {
        public ChangingFlightIneligibleException(string message) : base(message)
        {
        }
    }
}