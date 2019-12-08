using System.ComponentModel.DataAnnotations;
using RestAirline.Booking.Domain.Booking;
using RestAirline.Booking.Domain.Booking.Passenger;

namespace RestAirline.Booking.Api.Resources.Booking.Passenger
{
    public class Passenger
    {
        [Key] public string Id { get; set; }

        public string PassengerKey { get; set; }

        public string Name { get; set; }

        public PassengerType PassengerType { get; set; }

        public int Age { get; set; }

        public string Email { get; set; }
    }

    public static class PassengerMapper
    {
        public static Passenger ToResource(this RestAirline.Booking.ReadModel.EntityFramework.Booking.Passenger passenger)
        {
            var model = new Passenger
            {
                Id = passenger.PassengerKey,
                PassengerKey = passenger.PassengerKey,
                Name = passenger.Name,
                PassengerType = (PassengerType) passenger.PassengerType,
                Age = passenger.Age,
                Email = passenger.Email,
            };

            return model;
        }
    }
}