using System;

namespace RestAirline.Api.Tests
{
    public class ApiTestingBadRequestException : Exception
    {
        public ApiTestingBadRequestException(string message) : base(message)
        {
        }
    }
}