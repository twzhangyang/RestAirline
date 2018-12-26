using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;
using RestAirline.Domain.Booking;

namespace RestAirline.CommandHandlers.Passenger
{
    public class AddPassengerCommandHandler : CommandHandler<Booking, BookingId, AddPassengerCommand>
    {
        public override Task ExecuteAsync(Booking aggregate, AddPassengerCommand command, CancellationToken cancellationToken)
        {
            aggregate.AddPassenger(command.Passenger);
            
            return Task.FromResult(0);
        }
    }
}