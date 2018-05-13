using System;

namespace RestAirline.Domain.Booking.Exceptions
{
    public class NoAllocatedSeatForPassengerException : Exception
    {
        public NoAllocatedSeatForPassengerException(string message) : base(message)
        {

        }
    }
}