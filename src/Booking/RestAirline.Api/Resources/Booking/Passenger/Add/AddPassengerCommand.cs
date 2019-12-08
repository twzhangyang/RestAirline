using Microsoft.AspNetCore.Mvc;
using RestAirline.Api.Controllers;
using RestAirline.Api.HyperMedia;
using RestAirline.Domain.Booking;
using RestAirline.Domain.Booking.Passenger;

namespace RestAirline.Api.Resources.Booking.Passenger.Add
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