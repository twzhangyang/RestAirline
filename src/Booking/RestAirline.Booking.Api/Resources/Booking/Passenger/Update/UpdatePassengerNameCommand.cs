using Microsoft.AspNetCore.Mvc;
using RestAirline.Booking.Api.HyperMedia;
using RestAirline.Booking.Api.Controllers;

namespace RestAirline.Booking.Api.Resources.Booking.Passenger.Update
{
    public class UpdatePassengerNameCommand : HyperMedia.HypermediaCommand<PassengerNameUpdatedResource>
    {
        public UpdatePassengerNameCommand(){}

        public UpdatePassengerNameCommand(IUrlHelper urlHelper, string bookingId, string passengerKey)
            : base(urlHelper.Link((BookingController c) => c.UpdatePassengerName(bookingId, null)))
        {
            BookingId = bookingId;
            PassengerKey = passengerKey;
        }

        public string BookingId { get; set; }
        
        public string PassengerKey { get; set; }

        public string Name { get; set; }
    }
}