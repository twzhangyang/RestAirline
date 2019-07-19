using System;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;
using RestAirline.Domain.Booking;

namespace RestAirline.CommandHandlers.Passenger
{
    public class AddPassengerCommandHandler : CommandHandler<Booking, BookingId, AddPassengerCommand>
    {
        public override Task ExecuteAsync(Booking aggregate, AddPassengerCommand command,
            CancellationToken cancellationToken)
        {
            var passenger = new Domain.Booking.Passenger
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