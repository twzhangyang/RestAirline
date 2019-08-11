using System.ComponentModel.DataAnnotations;
using RestAirline.Domain.Booking;
using RestAirline.Domain.Booking.Passenger;

namespace RestAirline.Api.Resources.Booking.Passenger
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
        public static Passenger ToResource(this ReadModel.EntityFramework.Booking.Passenger passenger)
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