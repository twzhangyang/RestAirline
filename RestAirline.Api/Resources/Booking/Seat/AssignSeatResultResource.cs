using System;
using Microsoft.AspNetCore.Mvc;
using RestAirline.Api.HyperMedia;
using RestAirline.Api.Resources.Availability;

namespace RestAirline.Api.Resources.Booking.Seat
{
    public class AssignSeatResultResource
    {
        [Obsolete("For serialization")]
        public AssignSeatResultResource()
        {

        }

        public AssignSeatResultResource(IUrlHelper urlHelper)
        {
            ResourceLinks = new Links();
            ResourceCommands = new Commands();
        }

        public Guid BookingId { get; set; }
        public Links ResourceLinks { get; set; }
        public Commands ResourceCommands { get; set; }

        public class Links
        {
            public Link<AssignSeatResultResource> Self { get; set; }
            public Link<SelectTripResultResource> Parent { get; set; }
            public Link<BookingResource> Booking { get; set; }
        }

        public class Commands
        {
            public ChangeFlightCommand ChangeFlight { get; set; }
            public UnassignSeatCommand UnassignSeat { get; set; }
            public AddAirportTransferServiceCommand AddAirportTransferServiceCommand { get; set; }
        }
    }
}