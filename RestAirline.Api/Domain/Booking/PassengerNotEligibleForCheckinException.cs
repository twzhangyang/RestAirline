using System;

namespace RestAirline.Api.Domain.Booking
{
    public class PassengerNotEligibleForCheckinException : Exception
    {
        public PassengerNotEligibleForCheckinException(string message) : base(message)
        {

        }
    }
}