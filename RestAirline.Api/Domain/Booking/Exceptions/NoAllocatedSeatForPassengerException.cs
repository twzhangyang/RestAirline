using System;

namespace RestAirline.Api.Domain.Booking.Exceptions
{
    public class NoAllocatedSeatForPassengerException : Exception
    {
        public NoAllocatedSeatForPassengerException(string message) : base(message)
        {

        }
    }
}