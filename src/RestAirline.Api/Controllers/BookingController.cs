using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using EventFlow.EntityFramework.ReadStores;
using EventFlow.ReadStores.InMemory;
using Microsoft.AspNetCore.Mvc;
using RestAirline.Api.Resources.Booking;
using RestAirline.Api.Resources.Booking.Journey;
using RestAirline.Api.Resources.Booking.Passenger;
using RestAirline.Domain.Booking;
using RestAirline.ReadModel;
using RestAirline.Shared;
using RestAirline.Shared.ModelBuilders;
using AddPassengerCommand = RestAirline.CommandHandlers.Passenger.AddPassengerCommand;
using SelectJourneysCommand = RestAirline.Commands.Journey.SelectJourneysCommand;
using UpdatePassengerNameCommand = RestAirline.CommandHandlers.Passenger.UpdatePassengerNameCommand;

namespace RestAirline.Api.Controllers
{
    [Route("api/booking")]
    public class BookingController : Controller
    {
        private readonly ICommandBus _commandBus;
        private readonly IInMemoryReadStore<BookingReadModel> _readStore;
        private readonly IEntityFrameworkReadModelStore<ReadModel.EntityFramework.BookingReadModel> _efReadStore;

        public BookingController(ICommandBus commandBus, IInMemoryReadStore<BookingReadModel> readStore, 
            IEntityFrameworkReadModelStore<ReadModel.EntityFramework.BookingReadModel> efReadStore)
        {
            _commandBus = commandBus;
            _readStore = readStore;
            _efReadStore = efReadStore;
        }

        [Route("journeys")]
        [HttpPost]
        public async Task<JourneysSelectionResource> SelectJourneys(List<string> journeyIds)
        {
            var journeys = new JourneysBuilder().BuildJourneys();
            var bookingId = BookingId.New;

            var command = new SelectJourneysCommand(bookingId, journeys);
            await _commandBus.PublishAsync(command, CancellationToken.None);

            return new JourneysSelectionResource(Url, bookingId.Value);
        }

        [Route("{bookingId}")]
        [HttpGet]
        public async Task<BookingResource> GetBooking(string bookingId)
        {
            var booking = await _readStore.GetAsync(bookingId, CancellationToken.None);
            
            var booking2 = await _efReadStore.GetAsync(bookingId, CancellationToken.None);

            return new BookingResource(Url, booking.ReadModel);
        }

        [Route("/{bookingId}/passenger")]
        [HttpPost]
        public async Task<PassengerAdditionResource> AddPassenger(string bookingId, Passenger passenger)
        {
            passenger = new PassengerBuilder().CreatePassenger();

            var command = new AddPassengerCommand(new BookingId(bookingId), passenger);
            await _commandBus.PublishAsync(command, CancellationToken.None);
            
            return new PassengerAdditionResource(Url, bookingId, passenger.PassengerKey);
        }

        [Route("/{bookingId}/passenger/{passengerKey}/name")]
        [HttpPost]
        public async Task<PassengerNameUpdatesResource> UpdatePassengerName(string bookingId, string passengerKey,
            string name)
        {
            name = "new-name";
            var command = new UpdatePassengerNameCommand(new BookingId(bookingId), passengerKey, name);
            await _commandBus.PublishAsync(command, CancellationToken.None);

            return new PassengerNameUpdatesResource(Url, bookingId);
        }
    }
}