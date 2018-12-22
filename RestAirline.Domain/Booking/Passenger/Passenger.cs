using EventFlow.ValueObjects;

namespace RestAirline.Domain.Booking
{
    public class Passenger : ValueObject
    {
        public string PassengerKey { get; set; }
        
        public string Name { get; set; }

        public PassengerType PassengerType { get; set; }

        public int Age { get; set; }

        public string Email { get; set; }
    }

    public enum PassengerType
    {
        Male,
        Female,
        Infant,
        Unknown
    }
}