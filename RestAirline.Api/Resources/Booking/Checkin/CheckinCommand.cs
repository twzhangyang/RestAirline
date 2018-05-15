using System;
using RestAirline.Api.Hypermedia;

namespace RestAirline.Api.Resources.Booking.Checkin
{
    public class CheckinCommand : HypermediaCommand<CheckinResultResource>
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