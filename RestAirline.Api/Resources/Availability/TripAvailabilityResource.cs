using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RestAirline.Api.Controllers;
using RestAirline.Api.Hypermedia;
using RestAirline.Api.Resources.Booking;
using RestAirline.Domain.Shared;

namespace RestAirline.Api.Resources.Availability
{
    public class TripAvailabilityResource
    {
        public TripAvailabilityResource(IUrlHelper urlHelper)
        {
            ResourceCommands = new Commands(urlHelper);
            ResourceLinks = new Links(urlHelper);
        }

        public List<Trip> AvailableTrips { get; set; }
        public Commands ResourceCommands { get; set; }
        public Links ResourceLinks { get; set; }

        public class Commands
        {
            public Commands(IUrlHelper urlHelper)
            {
                SelectTripCommand = new SelectTripCommand(urlHelper.Link((BookingController c) => c.SelectTrip(null)));
            }

            public SelectTripCommand SelectTripCommand { get; set; }
        }

        public class Links
        {
            public Links(IUrlHelper urlHelper)
            {
                Previous = urlHelper.Link((HomeController c) => c.Index());
                Self = urlHelper.Link((TripAvailabilityController c) => c.SearchTrips(null));
            }

            public Link<RestAirlineHomeResource> Previous { get; set; }
            public Link<TripAvailabilityResource> Self { get; set; }
        }
    }
}