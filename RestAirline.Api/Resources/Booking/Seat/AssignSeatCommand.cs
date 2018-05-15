using System;
using RestAirline.Api.HyperMedia;
using RestAirline.Domain.Booking;

namespace RestAirline.Api.Resources.Booking.Seat
{
    public class AssignSeatCommand : HyperMediaCommand<AssignSeatResultResource>
    {
        [Obsolete("For serialization")]
        public AssignSeatCommand()
        {
        }

        public AssignSeatCommand(Link<AssignSeatResultResource> postUrl) : base(postUrl)
        {

        }

        public Domain.Shared.Seat Seat { get; set; }
        public Passenger Passenger { get; set; }
    }
}