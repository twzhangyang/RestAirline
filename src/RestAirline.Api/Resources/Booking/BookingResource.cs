using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RestAirline.Api.Controllers;
using RestAirline.Api.Hypermedia;
using RestAirline.Api.Resources.Booking.Journey;
using RestAirline.Api.Resources.Booking.Passenger;
using RestAirline.ReadModel.EntityFramework;

namespace RestAirline.Api.Resources.Booking
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
            Journeys = booking.Journeys.Select(j=>j.ToResource()).ToList();
            Passengers = booking.Passengers.Select(p=>p.ToResource()).ToList();
        }

        public string Id { get; }

        public List<Journey.Journey> Journeys { get; }

        public List<Passenger.Passenger> Passengers { get; }

        public Links ResourceLinks { get; set; }

        public class Links
        {
            private readonly IUrlHelper _urlHelper;
            private readonly string _bookingId;

            public Links(IUrlHelper urlHelper, string bookingId)
            {
                _urlHelper = urlHelper;
                _bookingId = bookingId;
            }

            public Link<BookingResource> Self => _urlHelper.Link((BookingController c) => c.GetBooking(_bookingId));
            public Link<RestAirlineHomeResource> Home => _urlHelper.Link((HomeController c) => c.Index());
        }
    }
}