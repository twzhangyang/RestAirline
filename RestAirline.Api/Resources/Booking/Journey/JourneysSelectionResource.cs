using System;
using Microsoft.AspNetCore.Mvc;
using RestAirline.Api.Controllers;
using RestAirline.Api.Hypermedia;
using RestAirline.Api.Resources.Booking.Passenger;


namespace RestAirline.Api.Resources.Booking.Journey
{
    public class JourneysSelectionResource
    {
        [Obsolete("For serialization")]
        public JourneysSelectionResource()
        {
        }

        public JourneysSelectionResource(IUrlHelper urlHelper, string bookingId)
        {
            ResourceLinks = new Links(urlHelper, bookingId);
            ResourceCommands = new Commands(urlHelper, bookingId);
        }

        public Links ResourceLinks { get; set; }
        public Commands ResourceCommands { get; set; }

        public class Links
        {
            private readonly IUrlHelper _urlHelper;
            private readonly string _bookingId;

            public Links(IUrlHelper urlHelper, string bookingId)
            {
                _urlHelper = urlHelper;
                _bookingId = bookingId;
            }

            public Link<BookingResource> Booking => _urlHelper.Link((BookingController c) => c.GetBooking(_bookingId));
            public Link<RestAirlineHomeResource> Home => _urlHelper.Link((HomeController c) => c.Index());
        }

        public class Commands
        {
            private readonly IUrlHelper _urlHelper;
            private readonly string _bookingId;

            public Commands(IUrlHelper urlHelper, string bookingId)
            {
                _urlHelper = urlHelper;
                _bookingId = bookingId;
            }

            public AddPassengerCommand AddPassengerCommand => new AddPassengerCommand(_urlHelper, _bookingId);
        }
    }
}