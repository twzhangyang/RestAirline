using System;
using RestAirline.Api.HyperMedia;

namespace RestAirline.Api.Resources.Booking.Seat
{
    public class UnassignSeatCommand : HyperMediaCommand<UnassignSeatResultResource>
    {
        [Obsolete("For serialization")]
        public UnassignSeatCommand()
        {
        }

        public UnassignSeatCommand(Link<UnassignSeatResultResource> postUrl) : base(postUrl)
        {
        }
    }
}