using System;
using Microsoft.AspNetCore.Mvc;
using RestAirline.Api.Controllers;
using RestAirline.Api.Hypermedia;
using RestAirline.Domain.Booking;

namespace RestAirline.Api.Resources.Booking.Seat
{
    public class UnassignSeatCommand : HypermediaCommand<SeatUnassignmentResrouce>
    {
        [Obsolete("For serialization")]
        public UnassignSeatCommand() { }

        public UnassignSeatCommand(IUrlHelper urlHelper) : base(urlHelper.Link((BookingController c) => c.UnassignSeat(null)))
        {
        }

        public Guid BookingId { get; set; }
        public Passenger Passenger { get; set; }
    }
}