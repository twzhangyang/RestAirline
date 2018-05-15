using System;
using Microsoft.AspNetCore.Mvc;
using RestAirline.Api.Controllers;
using RestAirline.Api.Hypermedia;
using RestAirline.Api.Resources.Availability;
using RestAirline.Api.Resources.Booking.Seat;

namespace RestAirline.Api.Resources.Booking
{
    public class ChangeFlightResultResource
    {
        [Obsolete("For serialization")]
        public ChangeFlightResultResource() { }


        public ChangeFlightResultResource(IUrlHelper urlHelper, Guid bookingId)
        {
            ResourceLinks = new Links(urlHelper, bookingId);
            ResourceCommands = new Commands(urlHelper);
        }

        public Guid BookingId { get; set; }
        public Links ResourceLinks { get; set; }
        public Commands ResourceCommands { get; set; }

        public class Links
        {
            private readonly IUrlHelper _urlHelper;
            private readonly Guid _bookingId;

            public Links(IUrlHelper urlHelper, Guid bookingId)
            {
                _urlHelper = urlHelper;
                _bookingId = bookingId;
            }

            public Link<ChangeFlightResultResource> Self => _urlHelper.Link((BookingController c) => c.ChangeFlight(null));
            public Link<SelectTripResultResource> Parent => _urlHelper.Link((BookingController c) => c.SelectTrip(null));
            public Link<BookingResource> Booking => _urlHelper.Link((BookingController c) => c.GetBooking(_bookingId));
        }

        public class Commands
        {
            private readonly IUrlHelper _urlHelper;

            public Commands(IUrlHelper urlHelper)
            {
                _urlHelper = urlHelper;
            }

            public AssignSeatCommand AssignSeat => new AssignSeatCommand(_urlHelper);
            public AssignSeatAutomaticallyCommand AssignSeatAutomaticallyCommand => new AssignSeatAutomaticallyCommand(_urlHelper);
        }
    }
}