using System;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;
using RestAirline.Booking.Commands.Passenger;
using RestAirline.Booking.Domain.Booking;

namespace RestAirline.CommandHandlers.Passenger
{
    public class AddPassengerCommandHandler : CommandHandler<Booking.Domain.Booking.Booking, BookingId, AddPassengerCommand>
    {
        public override Task ExecuteAsync(Booking.Domain.Booking.Booking aggregate, AddPassengerCommand command,
            CancellationToken cancellationToken)
        {
            var passenger = new Booking.Domain.Booking.Passenger.Passenger
            {
                Age = command.Age,
                Name = command.Name,
                Email = command.Email,
                PassengerType = command.PassengerType,
                PassengerKey = (aggregate.Passengers.Count + 1).ToString()
            };

            aggregate.AddPassenger(passenger);

            return Task.FromResult(0);
        }
    }
}