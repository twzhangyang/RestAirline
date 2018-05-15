using System;
using Microsoft.AspNetCore.Mvc;
using RestAirline.Api.HyperMedia;
using RestAirline.Api.Resources.Availability;
using RestAirline.Api.Resources.Booking.Seat;

namespace RestAirline.Api.Resources.Booking
{
    public class SelectTripResultResource
    {
        [Obsolete("For serialization")]
        public SelectTripResultResource()
        {

        }

        public SelectTripResultResource(IUrlHelper urlHelper)
        {
            ResourceLinks = new Links();
            ResourceCommands = new Commands();
        }

        public Guid BookingId { get; set; }
        public Links ResourceLinks { get; set; }
        public Commands ResourceCommands { get; set; }

        public class Links
        {
            public Link<SelectTripResultResource> Self { get; set; }
            public Link<TripAvailabilityResource> Parent { get; set; }
            public Link<BookingResource> Booking { get; set; }
        }

        public class Commands
        {
            public ChangeFlightCommand ChangeFlight { get; set; }
            public AssignSeatCommand AssignSeat { get; set; }
            public AssignSeatAutomaticallyCommand AssignSeatAutomaticallyCommand { get; set; }
        }
    }
}