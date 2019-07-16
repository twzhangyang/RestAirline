using System;
using Microsoft.AspNetCore.Mvc;
using Remotion.Linq.Clauses.ResultOperators;
using RestAirline.Api.Controllers;
using RestAirline.Api.Hypermedia;

namespace RestAirline.Api.Resources.Booking.Passenger
{
    public class PassengerAddedResource
    {
        [Obsolete("For serialization")]
        public PassengerAddedResource()
        {
        }

        public PassengerAddedResource(IUrlHelper urlHelper, string bookingId, string passengerKey)
        {
            ResourceLinks = new Links(urlHelper, bookingId);
            ResourceCommands = new Commands(urlHelper, bookingId, passengerKey);
        }

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
            private readonly string _passengerKey;

            [Obsolete("For serialization")]
            public Commands()
            {
            }

            public Commands(IUrlHelper urlHelper, string bookingId, string passengerKey)
            {
                _urlHelper = urlHelper;
                _bookingId = bookingId;
                _passengerKey = passengerKey;
                AddPassengerCommand = new AddPassengerCommand(_urlHelper, _bookingId);
                UpdatePassengerNameCommand = new UpdatePassengerNameCommand(_urlHelper, _bookingId, _passengerKey);
            }

            public AddPassengerCommand AddPassengerCommand { get; set; }

            public UpdatePassengerNameCommand UpdatePassengerNameCommand { get; set; }
        }
    }
}