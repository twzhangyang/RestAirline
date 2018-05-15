using System;
using Microsoft.AspNetCore.Mvc;
using RestAirline.Api.HyperMedia;

namespace RestAirline.Api.Resources.Booking.Seat
{
    public class AssignSeatAutomaticallyResultResource
    {
        [Obsolete("For serialization")]
        public AssignSeatAutomaticallyResultResource()
        {

        }

        public AssignSeatAutomaticallyResultResource(IUrlHelper urlHelper)
        {
            ResourceLinks = new AssignSeatResultResource.Links();
            ResourceCommands = new AssignSeatResultResource.Commands();
        }

        public Guid BookingId { get; set; }
        public AssignSeatResultResource.Links ResourceLinks { get; set; }
        public AssignSeatResultResource.Commands ResourceCommands { get; set; }

        public class Links
        {
            public Link<AssignSeatAutomaticallyResultResource> Self { get; set; }
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