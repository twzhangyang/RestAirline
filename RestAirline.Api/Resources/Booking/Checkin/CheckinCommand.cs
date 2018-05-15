using System;
using RestAirline.Api.HyperMedia;

namespace RestAirline.Api.Resources.Booking.Checkin
{
    public class CheckinCommand : HyperMediaCommand<CheckinResultResource>
    {
        [Obsolete("For serialization")]
        public CheckinCommand()
        {
        }

        public CheckinCommand(Link<CheckinResultResource> postUrl) : base(postUrl)
        {
        }
    }
}