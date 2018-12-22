using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using EventFlow.ReadStores.InMemory;
using Microsoft.AspNetCore.Mvc;
using RestAirline.Api.Resources.Booking;
using RestAirline.Domain.Booking;
using RestAirline.ReadModel;
using RestAirline.TestsHelper;
using SelectJourneysCommand = RestAirline.Commands.Journey.SelectJourneysCommand;

namespace RestAirline.Api.Controllers
{
    [Route("api/booking")]
    public class BookingController : Controller
    {
        private readonly ICommandBus _commandBus;
        private readonly IInMemoryReadStore<BookingReadModel> _readStore;

        public BookingController(ICommandBus commandBus, IInMemoryReadStore<BookingReadModel> readStore)
        {
            _commandBus = commandBus;
            _readStore = readStore;
        }
        
        [Route("journeys")]
        [HttpPost]
        public async Task<BookingResource> SelectJourneys()
        {
            var journeys = new JourneysBuilder().BuildJourneys();
            var bookingId = BookingId.New;

            var command = new SelectJourneysCommand(bookingId, journeys);
            await _commandBus.PublishAsync(command, CancellationToken.None);

            var booking = await _readStore.GetAsync(bookingId.Value, CancellationToken.None);
            
            return new BookingResource(booking.ReadModel);
        }
        
        public JourneysSelectionResource SelectJourneys1()
        {
            return null;
        }
    }
}