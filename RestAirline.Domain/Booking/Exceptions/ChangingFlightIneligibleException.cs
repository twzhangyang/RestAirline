using System;

namespace RestAirline.Domain.Booking.Exceptions
{
    public class ChangingFlightIneligibleException : Exception
    {
        public ChangingFlightIneligibleException(string message) : base(message)
        {
        }
    }
}