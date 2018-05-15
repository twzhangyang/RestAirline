using System;
using Microsoft.AspNetCore.Mvc;
using RestAirline.Api.Controllers;
using RestAirline.Api.HyperMedia;
using RestAirline.Domain.Booking;

namespace RestAirline.Api.Resources.Booking.Seat
{
    public class UnassignSeatCommand : HyperMediaCommand<UnassignSeatResultResource>
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