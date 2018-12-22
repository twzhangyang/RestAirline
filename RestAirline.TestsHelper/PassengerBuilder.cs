using RestAirline.Domain.Booking;

namespace RestAirline.TestsHelper
{
    public class PassengerBuilder
    {
        public Passenger CreatePassenger()
        {
            return new Passenger
            {
                Name = "Yang",
                Age = 12,
                Email = "yang@test.com",
                PassengerType = PassengerType.Male
            };   
        }
    }
}