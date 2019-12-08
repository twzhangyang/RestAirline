using System;

namespace RestAirline.Booking.Api.Tests
{
    public class ApiTestingBadRequestException : Exception
    {
        public ApiTestingBadRequestException(string message) : base(message)
        {
        }
    }
}