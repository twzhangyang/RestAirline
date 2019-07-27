using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using EventFlow.Queries;
using Microsoft.AspNetCore.Mvc;
using RestAirline.Api.Resources.Booking;
using RestAirline.Api.Resources.Booking.Journey;
using RestAirline.Api.Resources.Booking.Passenger;
using RestAirline.Api.Resources.Booking.Passenger.Add;
using RestAirline.Domain.Booking;
using RestAirline.QueryHandlers.Booking;
using RestAirline.ReadModel.EntityFramework;
using RestAirline.Shared.ModelBuilders;
using UpdatePassengerNameCommand = RestAirline.CommandHandlers.Passenger.UpdatePassengerNameCommand;

namespace RestAirline.Api.Controllers
{
    [Route("api/booking")]
    public class BookingController : Controller
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryHandler<ReadModelByIdQuery<BookingReadModel>, BookingReadModel> _bookingQueryHandler;
        private readonly BookingQueryHandler _queryHandler;

        public BookingController(ICommandBus commandBus,
            IQueryHandler<ReadModelByIdQuery<BookingReadModel>, BookingReadModel> bookingQueryHandle,
            BookingQueryHandler queryHandler
        )
        {
            _commandBus = commandBus;
            _bookingQueryHandler = bookingQueryHandle;
            _queryHandler = queryHandler;
        }

        [Route("journeys")]
        [HttpPost]
        public async Task<JourneysSelectedResource> SelectJourneys(SelectJourneysCommand selectJourneysCommand)
        {
            //Will integrate journey availability micro-service before select journey, passenger query journey availability micro-service in UI
            //Get journey from journey availability micro-service
            var journeys = new JourneysBuilder().BuildJourneys();
            var bookingId = BookingId.New;

            var command = new Commands.Journey.SelectJourneysCommand(bookingId, journeys);
            await _commandBus.PublishAsync(command, CancellationToken.None);

            return new JourneysSelectedResource(Url, bookingId.Value);
        }

        [Route("{bookingId}")]
        [HttpGet]
        public async Task<BookingResource> GetBooking(string bookingId)
        {
            // Not sure why this does not work
//            var booking = await _bookingQueryHandler.ExecuteQueryAsync(
//                new ReadModelByIdQuery<BookingReadModel>(bookingId),
//                new CancellationToken());

            var booking = await _queryHandler.ExecuteQueryAsync(
                new ReadModelByIdQuery<BookingReadModel>(bookingId),
                new CancellationToken());

            return new BookingResource(Url, booking);
        }

        /// <summary>
        /// Add a passenger in booking
        /// </summary>
        /// <param name="bookingId">A unique id for current booking</param>
        /// <param name="addPassengerCommand">Request for adding passenger</param>
        /// <returns></returns>
        [Route("{bookingId}/passenger")]
        [HttpPost]
        public async Task<PassengerAddedResource> AddPassenger(string bookingId,
            [FromBody]AddPassengerCommand addPassengerCommand)
        {
            var command = new CommandHandlers.Passenger.AddPassengerCommand(new BookingId(bookingId))
            {
                Age = addPassengerCommand.Age,
                Email = addPassengerCommand.Email,
                Name = addPassengerCommand.Name,
                PassengerType = addPassengerCommand.PassengerType
            };
            
            await _commandBus.PublishAsync(command, CancellationToken.None);

            var booking = await _queryHandler.ExecuteQueryAsync(
                new ReadModelByIdQuery<BookingReadModel>(bookingId),
                new CancellationToken());

            return new PassengerAddedResource(Url, bookingId, booking.Passengers.Last().PassengerKey);
        }

        [Route("{bookingId}/passenger/{passengerKey}/name")]
        [HttpPost]
        public async Task<PassengerNameUpdatedResource> UpdatePassengerName(string bookingId, string passengerKey,
            string name)
        {
            name = "new-name";
            var command = new UpdatePassengerNameCommand(new BookingId(bookingId), passengerKey, name);
            await _commandBus.PublishAsync(command, CancellationToken.None);

            return new PassengerNameUpdatedResource(Url, bookingId);
        }
    }
}