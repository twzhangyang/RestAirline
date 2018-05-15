using System;
using RestAirline.Api.HyperMedia;

namespace RestAirline.Api.Resources.Booking.Seat
{
    public class AssignSeatAutomaticallyCommand : HyperMediaCommand<AssignSeatAutomaticallyResultResource>
    {
        [Obsolete("For serialization")]
        public AssignSeatAutomaticallyCommand()
        {
        }

        public AssignSeatAutomaticallyCommand(Link<AssignSeatAutomaticallyResultResource> postUrl) : base(postUrl)
        {
        }
    }
}