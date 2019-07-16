using System;
using Microsoft.AspNetCore.Mvc;
using RestAirline.Api.Controllers;
using RestAirline.Api.Hypermedia;
using RestAirline.Api.Resources.Booking.Passenger;


namespace RestAirline.Api.Resources.Booking.Journey
{
    public class JourneysSelectedResource
    {
        [Obsolete("For serialization")]
        public JourneysSelectedResource()
        {
        }

        public JourneysSelectedResource(IUrlHelper urlHelper, string bookingId)
        {
            ResourceLinks = new Links(urlHelper, bookingId);
            ResourceCommands = new Commands(urlHelper, bookingId);
            BookingId = bookingId;
        }
        
        public string BookingId { get; set; }

        public Links ResourceLinks { get; set; }
        public Commands ResourceCommands { get; set; }

        public class Links
        {
            private readonly IUrlHelper _urlHelper;
            private readonly string _bookingId;

            [Obsolete("For serialization")]
            public Links()
            {
            }

            public Links(IUrlHelper urlHelper, string bookingId)
            {
                _urlHelper = urlHelper;
                _bookingId = bookingId;
                Booking = _urlHelper.Link((BookingController c) => c.GetBooking(_bookingId));
                Home = _urlHelper.Link((HomeController c) => c.Index());
            }

            public Link<BookingResource> Booking { get; set; }
            public Link<RestAirlineHomeResource> Home { get; set; }
        }

        public class Commands
        {
            private readonly IUrlHelper _urlHelper;
            private readonly string _bookingId;

            [Obsolete("For serialization")]
            public Commands()
            {
            }

            public Commands(IUrlHelper urlHelper, string bookingId)
            {
                _urlHelper = urlHelper;
                _bookingId = bookingId;
                AddPassengerCommand = new AddPassengerCommand(_urlHelper, _bookingId);
            }

            public AddPassengerCommand AddPassengerCommand { get; set; }
        }
    }
}