using System;
using System.ComponentModel.DataAnnotations;

namespace RestAirline.ReadModel.MongoDb.Booking
{
    public class Passenger
    {
        public Guid Id { get; set; }

        public string PassengerKey { get; set; }

        public string Name { get; set; }

        public PassengerType PassengerType { get; set; }

        public int Age { get; set; }

        public string Email { get; set; }
    }

    public static class PassengerMapper
    {
        public static Passenger ToReadModel(this Domain.Booking.Passenger passenger)
        {
            var model = new Passenger
            {
                Id = Guid.NewGuid(),
                PassengerKey = passenger.PassengerKey,
                Name = passenger.Name,
                PassengerType = (PassengerType) passenger.PassengerType,
                Age = passenger.Age,
                Email = passenger.Email
            };

            return model;
        }
    }

    public enum PassengerType
    {
        Male,
        Female,
        Infant,
        Unknown
    }
}