namespace RestAirline.Domain.Booking
{
    public class Passenger
    {
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