using System;
using Microsoft.AspNetCore.Mvc;
using RestAirline.Api.Resources.Booking;
using RestAirline.Api.Resources.Booking.Seat;

namespace RestAirline.Api.Controllers
{
    public class BookingController : Controller
    {
        [Route("api/booking/tripSelection")]
        [HttpPost]
        public SelectTripResultResource SelectTrip(SelectTripCommand command)
        {
            return new SelectTripResultResource(Url);
        }

        [Route("api/booking/{0}")]
        [HttpGet]
        public BookingResource GetBooking(Guid id)
        {
            return new BookingResource();
        }

        [Route("api/booking/{0}/flightChange")]
        [HttpPost]
        public ChangeFlightResultResource ChangeFlight(ChangeFlightCommand command)
        {
            return new ChangeFlightResultResource(Url);
        }

        [Route("api/booking/{0}/seatAssignment")]
        [HttpPost]
        public AssignSeatResultResource AssignSeat(AssignSeatCommand command)
        {
            return new AssignSeatResultResource(Url);
        }

        [Route("api/booking/{0}/seatUnassign")]
        [HttpPost]
        public UnassignSeatResultResource UnassignSeat(UnassignSeatCommand command)
        {
            return new UnassignSeatResultResource(Url);
        }

        [Route("api/booking/{0}/automaticallySeatAssignment")]
        [HttpPost]
        public AssignSeatAutomaticallyResultResource AssignSeatAutomatically(AssignSeatAutomaticallyCommand command)
        {
            return new AssignSeatAutomaticallyResultResource(Url);
        }

        [Route("api/booking/{0}/airportTransferService")]
        [HttpPost]
        public AddAirportTransferServiceResultResource AddAirportTransferService(AddAirportTransferServiceCommand command)
        {
            return new AddAirportTransferServiceResultResource();
        }
    }

}