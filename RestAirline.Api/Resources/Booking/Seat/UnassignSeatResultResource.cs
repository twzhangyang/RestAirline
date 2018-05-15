using System;
using Microsoft.AspNetCore.Mvc;
using RestAirline.Api.HyperMedia;

namespace RestAirline.Api.Resources.Booking.Seat
{
    public class UnassignSeatResultResource
    {
        [Obsolete("For serialization")]
        public UnassignSeatResultResource()
        {

        }

        public UnassignSeatResultResource(IUrlHelper urlHelper)
        {
            ResourceLinks = new Links();
            ResourceCommands = new Commands();
        }

        public Guid BookingId { get; set; }
        public Links ResourceLinks { get; set; }
        public Commands ResourceCommands { get; set; }

        public class Links
        {
            public Link<UnassignSeatResultResource> Self { get; set; }
            public Link<AssignSeatResultResource> Parent { get; set; }
            public Link<BookingResource> Booking { get; set; }
        }

        public class Commands
        {
            public ChangeFlightCommand ChangeFlight { get; set; }
            public AssignSeatCommand AssignSeat { get; set; }
            public AssignSeatAutomaticallyCommand AssignSeatAutomatically { get; set; }
        }

    }
}