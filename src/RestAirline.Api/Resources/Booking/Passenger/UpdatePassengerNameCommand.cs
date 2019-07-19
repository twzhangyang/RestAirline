using System;
using Microsoft.AspNetCore.Mvc;
using RestAirline.Api.Controllers;
using RestAirline.Api.Hypermedia;

namespace RestAirline.Api.Resources.Booking.Passenger
{
    public class UpdatePassengerNameCommand : HypermediaCommand<PassengerNameUpdatedResource>
    {
        public UpdatePassengerNameCommand(){}

        public UpdatePassengerNameCommand(IUrlHelper urlHelper, string bookingId, string passengerKey)
            : base(urlHelper.Link((BookingController c) => c.UpdatePassengerName(bookingId, passengerKey, null)))
        {
            BookingId = bookingId;
            PassengerKey = passengerKey;
        }

        public string BookingId { get; set; }
        
        public string PassengerKey { get; set; }

        public string Name { get; set; }
    }
}