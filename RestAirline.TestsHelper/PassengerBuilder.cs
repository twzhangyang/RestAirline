using System;
using RestAirline.Domain.Booking;

namespace RestAirline.TestsHelper
{
    public class PassengerBuilder
    {
        public Passenger CreatePassenger(Action<Passenger> modifier = null)
        {
            var p = new Passenger
            {
                PassengerKey = Guid.NewGuid().ToString(),
                Name = "Yang",
                Age = 12,
                Email = "yang@test.com",
                PassengerType = PassengerType.Male
            };
            modifier?.Invoke(p);

            return p;
        }
    }
}