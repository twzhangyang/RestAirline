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
        public TripSelectionResource SelectTrip(SelectTripCommand command)
        {
            var booking = Booking.SelectTrip(command.Trip, command.Passengers);

            return new TripSelectionResource(Url, booking.Id);
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
        public FlightChangeResource ChangeFlight(ChangeFlightCommand command)
        {
            var booking = _bookingRepository.Get(command.BookingId);
            booking.ChangeFlight(command.Journey, command.Flight);


            return new FlightChangeResource(Url, command.BookingId);
        }

        [Route("api/booking/{0}/seatAssignment")]
        [HttpPost]
        public SeatAssignmentResource AssignSeat(AssignSeatCommand command)
        {
            var booking = _bookingRepository.Get(command.BookingId);
            booking.AssignSeat(command.Seat, command.Passenger);

            return new SeatAssignmentResource(Url, command.BookingId);
        }

        [Route("api/booking/{0}/seatUnassign")]
        [HttpPost]
        public SeatUnassignmentResrouce UnassignSeat(UnassignSeatCommand command)
        {
            var booking = _bookingRepository.Get(command.BookingId);
            booking.UnassignSeat(command.Passenger);

            return new SeatUnassignmentResrouce(Url, command.BookingId);
        }

        [Route("api/booking/{0}/automaticallySeatAssignment")]
        [HttpPost]
        public SeatAssignmentAutomaticallyResoruce AssignSeatAutomatically(AssignSeatAutomaticallyCommand command)
        {
            var booking = _bookingRepository.Get(command.BookingId);
            booking.AssignSeatsAutomaticallyForAllPassengers(_seatAllocator);

            return new SeatAssignmentAutomaticallyResoruce(Url, command.BookingId);
        }

        [Route("api/booking/{0}/airportTransferService")]
        [HttpPost]
        public AirportTransferServiceAddedResource AddAirportTransferService(AddAirportTransferServiceCommand command)
        {
            var booking = _bookingRepository.Get(command.BookingId);
            booking.AddAirportTransferService(command.AirportTransfer);

            return new AirportTransferServiceAddedResource();
        }
    }

}