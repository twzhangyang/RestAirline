using System;
using Microsoft.AspNetCore.Mvc;
using RestAirline.Api.Resources.Booking;
using RestAirline.Api.Resources.Booking.Seat;
using RestAirline.Domain.Booking;
using RestAirline.Domain.Repository;

namespace RestAirline.Api.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ISeatAllocator _seatAllocator;

        public BookingController(IBookingRepository bookingRepository, ISeatAllocator seatAllocator)
        {
            _bookingRepository = bookingRepository;
            _seatAllocator = seatAllocator;
        }

        [Route("api/booking/tripSelection")]
        [HttpPost]
        public SelectTripResultResource SelectTrip(SelectTripCommand command)
        {
            var booking = Booking.SelectTrip(command.Trip, command.Passengers);

            return new SelectTripResultResource(Url, booking.Id);
        }

        [Route("api/booking/{0}")]
        [HttpGet]
        public BookingResource GetBooking(Guid id)
        {
            var booking = _bookingRepository.Get(id);
            return new BookingResource(booking);
        }

        [Route("api/booking/{0}/flightChange")]
        [HttpPost]
        public ChangeFlightResultResource ChangeFlight(ChangeFlightCommand command)
        {
            var booking = _bookingRepository.Get(command.BookingId);
            booking.ChangeFlight(command.Journey, command.Flight);


            return new ChangeFlightResultResource(Url, command.BookingId);
        }

        [Route("api/booking/{0}/seatAssignment")]
        [HttpPost]
        public AssignSeatResultResource AssignSeat(AssignSeatCommand command)
        {
            var booking = _bookingRepository.Get(command.BookingId);
            booking.AssignSeat(command.Seat, command.Passenger);

            return new AssignSeatResultResource(Url, command.BookingId);
        }

        [Route("api/booking/{0}/seatUnassign")]
        [HttpPost]
        public UnassignSeatResultResource UnassignSeat(UnassignSeatCommand command)
        {
            var booking = _bookingRepository.Get(command.BookingId);
            booking.UnassignSeat(command.Passenger);

            return new UnassignSeatResultResource(Url, command.BookingId);
        }

        [Route("api/booking/{0}/automaticallySeatAssignment")]
        [HttpPost]
        public AssignSeatAutomaticallyResultResource AssignSeatAutomatically(AssignSeatAutomaticallyCommand command)
        {
            var booking = _bookingRepository.Get(command.BookingId);
            booking.AssignSeatsAutomaticallyForAllPassengers(_seatAllocator);

            return new AssignSeatAutomaticallyResultResource(Url, command.BookingId);
        }

        [Route("api/booking/{0}/airportTransferService")]
        [HttpPost]
        public AddAirportTransferServiceResultResource AddAirportTransferService(AddAirportTransferServiceCommand command)
        {
            var booking = _bookingRepository.Get(command.BookingId);
            booking.AddAirportTransferService(command.AirportTransfer);

            return new AddAirportTransferServiceResultResource();
        }
    }

}