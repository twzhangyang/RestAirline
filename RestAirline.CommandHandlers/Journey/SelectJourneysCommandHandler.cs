using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;
using RestAirline.Commands.Journey;
using RestAirline.Domain.Booking;

namespace RestAirline.CommandHandlers.Journey
{
    public class SelectJourneysCommandHandler : CommandHandler<Booking, BookingId, SelectJourneysCommand>
    {
        public override Task ExecuteAsync(Booking aggregate, SelectJourneysCommand command, CancellationToken cancellationToken)
        {
            aggregate.SelectJourneys(command.Journeys);

            return Task.FromResult(0);
        }
    }
}