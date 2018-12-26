using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using RestAirline.Commands.Journey;
using RestAirline.Domain.Booking;
using RestAirline.Domain.Booking.Trip;

namespace RestAirline.TestsHelper.TestScenario
{
    public class SelectJourneysScenario : ScenarioBase
    {
        private readonly List<Journey> _journeys;

        public SelectJourneysScenario(ICommandBus commandBus, BookingId bookingId = null, List<Journey> journeys = null)
            : base(commandBus)
        {
            BookingId = bookingId;
            _journeys = journeys;
            CommandBus = commandBus;
        }

        public override Task Execute()
        {
            var journeys = _journeys ?? new JourneysBuilder().BuildJourneys();
            BookingId = BookingId ?? BookingId.New;

            var command = new SelectJourneysCommand(BookingId, journeys);

            return CommandBus.PublishAsync(command, CancellationToken.None);
        }
    }
}