using System;
using RestAirline.Api.Hypermedia;

namespace RestAirline.Api.Resources.Booking.Checkin
{
    public class CheckinCommand : HypermediaCommand<CheckinResource>
    {
        [Obsolete("For serialization")]
        public CheckinCommand()
        {
        }

        public CheckinCommand(Link<CheckinResource> postUrl) : base(postUrl)
        {
        }
    }
}