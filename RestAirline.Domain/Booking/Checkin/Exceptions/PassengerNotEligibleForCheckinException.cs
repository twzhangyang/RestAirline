using System;

namespace RestAirline.Domain.Booking.Checkin.Exceptions
{
    public class PassengerNotEligibleForCheckinException : Exception
    {
        public PassengerNotEligibleForCheckinException(string message) : base(message)
        {

        }
    }
}