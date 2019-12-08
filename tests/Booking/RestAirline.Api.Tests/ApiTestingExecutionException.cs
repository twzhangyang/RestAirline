using System;

namespace RestAirline.Api.Tests
{
    public class ApiTestingExecutionException : Exception
    {
        public ApiTestingExecutionException(string response) : base(response)
        {
        }
    }
}