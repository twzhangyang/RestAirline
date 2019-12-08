using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;
using RestAirline.Commands.Passenger;
using RestAirline.Domain.Booking;

namespace RestAirline.CommandHandlers.Passenger
{
    public class UpdatePassengerNameCommandHandler : CommandHandler<Booking, BookingId, UpdatePassengerNameCommand>
    {
        public override Task ExecuteAsync(Booking aggregate, UpdatePassengerNameCommand command,
            CancellationToken cancellationToken)
        {
            aggregate.UpdatePassengerName(command.PassengerKey, command.Name);
            
            return Task.FromResult(0);
        }
    }
}