using System;
using Microsoft.AspNetCore.Mvc;
using RestAirline.Api.Controllers;
using RestAirline.Api.Hypermedia;

namespace RestAirline.Api.Resources.Booking.Passenger
{
    public class AddPassengerCommand : HypermediaCommand<PassengerAddedResource>
    {
        [Obsolete("For serialization")]
        public AddPassengerCommand()
        {
        }

        public AddPassengerCommand(IUrlHelper urlHelper, string bookingId) : base(urlHelper.Link(
            (BookingController c) => c.AddPassenger(bookingId, null)
        ))
        {
        }

        public Domain.Booking.Passenger Passenger { get; set; }
    }
}