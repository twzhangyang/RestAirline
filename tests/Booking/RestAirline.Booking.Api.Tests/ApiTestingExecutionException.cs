using System;

namespace RestAirline.Booking.Api.Tests
{
    public class ApiTestingExecutionException : Exception
    {
        public ApiTestingExecutionException(string response) : base(response)
        {
        }
    }
}