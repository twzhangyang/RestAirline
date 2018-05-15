using System;
using Microsoft.AspNetCore.Mvc;
using RestAirline.Api.Controllers;
using RestAirline.Api.HyperMedia;

namespace RestAirline.Api.Resources.Booking.Seat
{
    public class UnassignSeatCommand : HyperMediaCommand<UnassignSeatResultResource>
    {
        [Obsolete("For serialization")]
        public UnassignSeatCommand() { }

        public UnassignSeatCommand(IUrlHelper urlHelper) : base(urlHelper.Link((BookingController c) => c.UnassignSeat(null)))
        {
        }
    }
}