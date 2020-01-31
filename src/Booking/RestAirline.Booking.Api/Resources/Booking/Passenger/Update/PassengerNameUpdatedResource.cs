using Microsoft.AspNetCore.Mvc;
using RestAirline.Booking.Api.Controllers;
using RestAirline.Booking.Api.HyperMedia;
using RestAirline.Booking.Api.Resources.Booking.Passenger.Add;
using RestAirline.Web.Hypermedia;

namespace RestAirline.Booking.Api.Resources.Booking.Passenger.Update
{
    public class PassengerNameUpdatedResource
    {
        public PassengerNameUpdatedResource()
        {
        }

        public PassengerNameUpdatedResource(IUrlHelper urlHelper, string bookingId,
            RestAirline.ReadModel.MongoDb.Booking.Passenger passenger)
        {
            ResourceLinks = new Links(urlHelper, bookingId);
            ResourceCommands = new Commands(urlHelper, bookingId, passenger.PassengerKey);
            Passenger = passenger.ToResource();
        }

        public Passenger Passenger { get; set; }
        public Links ResourceLinks { get; set; }
        public Commands ResourceCommands { get; set; }

        public class Links
        {
            private readonly IUrlHelper _urlHelper;
            private readonly string _bookingId;

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

            public Commands()
            {
            }

            public Commands(IUrlHelper urlHelper, string bookingId, string passengerKey)
            {
                _urlHelper = urlHelper;
                _bookingId = bookingId;
                AddPassengerCommand = new AddPassengerCommand(_urlHelper, _bookingId);
                UpdatePassengerNameCommand = new UpdatePassengerNameCommand(_urlHelper, bookingId, passengerKey);
            }

            public AddPassengerCommand AddPassengerCommand { get; set; }

            public UpdatePassengerNameCommand UpdatePassengerNameCommand { get; set; }
        }
    }
}