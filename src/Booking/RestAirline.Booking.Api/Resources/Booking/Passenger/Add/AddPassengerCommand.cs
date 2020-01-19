using Microsoft.AspNetCore.Mvc;
using RestAirline.Booking.Api.HyperMedia;
using RestAirline.Booking.Api.Controllers;
using RestAirline.Booking.Domain.Booking;
using RestAirline.Booking.Domain.Booking.Passenger;
using RestAirline.Web.Hypermedia;

namespace RestAirline.Booking.Api.Resources.Booking.Passenger.Add
{
    public class AddPassengerCommand : HypermediaCommand<PassengerAddedResource>
    {
        public AddPassengerCommand(){}

        public AddPassengerCommand(IUrlHelper urlHelper, string bookingId) : base(urlHelper.Link(
            (BookingController c) => c.AddPassenger(bookingId, null)
        ))
        {
            BookingId = bookingId;
        }
        
        public string BookingId { get; set; }
        
        public string Name { get; set; }

        public PassengerType PassengerType { get; set; }

        public int Age { get; set; }

        public string Email { get; set; }
    }
}