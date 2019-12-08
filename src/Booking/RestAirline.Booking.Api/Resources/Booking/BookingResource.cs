using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RestAirline.Booking.Api.Resources.Booking.Journey;
using RestAirline.Booking.Api.Resources.Booking.Passenger;
using RestAirline.Booking.Api.Controllers;
using RestAirline.Booking.Api.HyperMedia;
using RestAirline.Booking.ReadModel.EntityFramework;

namespace RestAirline.Booking.Api.Resources.Booking
{
    public class BookingResource
    {
        [Obsolete("For serialization")]
        public BookingResource()
        {
        }

        public BookingResource(IUrlHelper urlHelper, BookingReadModel booking)
        {
            Id = booking.Id;
            ResourceLinks = new Links(urlHelper, Id);
            Journeys = booking.Journeys.Select(j => j.ToResource()).ToList();
            Passengers = booking.Passengers.Select(p => p.ToResource()).ToList();
        }

        public string Id { get; set; }

        public List<Journey.Journey> Journeys { get; set; }

        public List<Passenger.Passenger> Passengers { get; set; }

        public Links ResourceLinks { get; set; }

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
                Self = _urlHelper.Link((BookingController c) => c.GetBooking(_bookingId));
                Home = _urlHelper.Link((HomeController c) => c.Index());
            }

            public Link<BookingResource> Self { get; set; }
            public Link<RestAirlineHomeResource> Home { get; set; }
        }
    }
}