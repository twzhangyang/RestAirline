using System;
using Microsoft.AspNetCore.Mvc;
using RestAirline.Api.Controllers;
using RestAirline.Api.Hypermedia;
using RestAirline.Api.Resources.Availability;
using RestAirline.Api.Resources.Booking.Seat;

namespace RestAirline.Api.Resources.Booking
{

    public class TripSelectionResource
    {
        [Obsolete("For serialization")]
        public TripSelectionResource() { }

        public TripSelectionResource(IUrlHelper urlHelper, Guid bookingId)
        {
            ResourceLinks = new Links(urlHelper, bookingId);
            ResourceCommands = new Commands(urlHelper);
        }

        public Guid BookingId { get; set; }
        public Links ResourceLinks { get; set; }
        public Commands ResourceCommands { get; set; }

        public class Links
        {
            private readonly IUrlHelper _urlHelper;
            private readonly Guid _bookingId;

            public Links(IUrlHelper urlHelper, Guid bookingId)
            {
                _urlHelper = urlHelper;
                _bookingId = bookingId;
            }

            public Link<TripSelectionResource> Self => _urlHelper.Link((BookingController c) => c.SelectTrip(null));
            public Link<TripAvailabilityResource> Parent => _urlHelper.Link((TripAvailabilityController c) => c.SearchTrips(null));
            public Link<BookingResource> Booking => _urlHelper.Link((BookingController c) => c.GetBooking(_bookingId));
        }

        public class Commands
        {
            private readonly IUrlHelper _urlHelper;

            public Commands(IUrlHelper urlHelper)
            {
                _urlHelper = urlHelper;
            }

            public ChangeFlightCommand ChangeFlight => new ChangeFlightCommand(_urlHelper);
            public AssignSeatCommand AssignSeat => new AssignSeatCommand(_urlHelper);
            public AssignSeatAutomaticallyCommand AssignSeatAutomaticallyCommand => new AssignSeatAutomaticallyCommand(null);
        }
    }


    public class TripSelectionResource1
    {
        private readonly IUrlHelper _urlHelper;

        public TripSelectionResource1(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }
        public Guid BookingId { get; set; }
        public string BookingResource => _urlHelper.Action("GetBooking", "Booking");
        public string FlightChange => _urlHelper.Action("ChangeFlight", "Booking");
        public string SeatAssignment => _urlHelper.Action("AssignSeat", "Booking");
    }

    public class TripSelectionResource2
    {
        private readonly IUrlHelper _urlHelper;

        public TripSelectionResource2(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }
        public Guid BookingId { get; set; }
        public string BookingResource => _urlHelper.Link((BookingController c) => c.GetBooking(BookingId)).ToString();
        public string FlightChange => _urlHelper.Link((BookingController c) => c.ChangeFlight(null)).ToString();
        public string SeatAssignment => _urlHelper.Link((BookingController c) => c.AssignSeat(null)).ToString();
    }

    public class TripSelectionResource3
    {
        private readonly IUrlHelper _urlHelper;

        public TripSelectionResource3(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }
        public Guid BookingId { get; set; }
        public Link<BookingResource> Booking => _urlHelper.Link((BookingController c) => c.GetBooking(BookingId));
        public ChangeFlightCommand ChangeFlight => new ChangeFlightCommand(_urlHelper);
        public AssignSeatCommand AssignSeat => new AssignSeatCommand(_urlHelper);
    }

}