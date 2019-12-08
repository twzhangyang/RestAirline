using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;
using RestAirline.Booking.Commands.Journey;
using RestAirline.Booking.Domain.Booking;

namespace RestAirline.CommandHandlers.Journey
{
    public class SelectJourneysCommandHandler : CommandHandler<Booking.Domain.Booking.Booking, BookingId, SelectJourneysCommand>
    {
        public override Task ExecuteAsync(Booking.Domain.Booking.Booking aggregate, SelectJourneysCommand command, CancellationToken cancellationToken)
        {
            aggregate.SelectJourneys(command.Journeys);

            return Task.FromResult(0);
        }
    }
}