using System;
using Microsoft.AspNetCore.Mvc;
using RestAirline.Api.Controllers;
using RestAirline.Api.Hypermedia;

namespace RestAirline.Api.Resources.Booking.Passenger
{
    public class PassengerAdditionResource
    {
        [Obsolete("For serialization")]
        public PassengerAdditionResource()
        {
        }

        public PassengerAdditionResource(IUrlHelper urlHelper, string bookingId, string passengerKey)
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
            private readonly string _passengerKey;

            public Commands(IUrlHelper urlHelper, string bookingId, string passengerKey)
            {
                _urlHelper = urlHelper;
                _bookingId = bookingId;
                _passengerKey = passengerKey;
            }
            
            public AddPassengerCommand AddPassengerCommand => new AddPassengerCommand(_urlHelper, _bookingId);
            public UpdatePassengerNameCommand UpdatePassengerNameCommand => new UpdatePassengerNameCommand(_urlHelper, _bookingId, _passengerKey);
        }
    }
}