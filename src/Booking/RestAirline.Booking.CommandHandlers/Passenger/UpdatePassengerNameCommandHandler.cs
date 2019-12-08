using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;
using RestAirline.Booking.Commands.Passenger;
using RestAirline.Booking.Domain.Booking;

namespace RestAirline.CommandHandlers.Passenger
{
    public class UpdatePassengerNameCommandHandler : CommandHandler<Booking.Domain.Booking.Booking, BookingId, UpdatePassengerNameCommand>
    {
        public override Task ExecuteAsync(Booking.Domain.Booking.Booking aggregate, UpdatePassengerNameCommand command,
            CancellationToken cancellationToken)
        {
            aggregate.UpdatePassengerName(command.PassengerKey, command.Name);
            
            return Task.FromResult(0);
        }
    }
}