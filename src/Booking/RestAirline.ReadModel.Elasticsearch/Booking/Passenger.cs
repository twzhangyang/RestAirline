using System;
using Nest;

namespace RestAirline.ReadModel.Elasticsearch.Booking
{
    public class Passenger
    {
        [Keyword]
        public Guid Id { get; set; }

        [Text]
        public string PassengerKey { get; set; }

        [Text]
        public string Name { get; set; }

        [Number(NumberType.Short)]
        public PassengerType PassengerType { get; set; }

        [Number(NumberType.Integer)]
        public int Age { get; set; }

        [Text]
        public string Email { get; set; }
    }

    public static class PassengerMapper
    {
        public static Passenger ToReadModel(this Domain.Booking.Passenger.Passenger passenger)
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