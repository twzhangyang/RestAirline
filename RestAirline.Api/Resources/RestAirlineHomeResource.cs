using System;
using Microsoft.AspNetCore.Mvc;
using RestAirline.Api.Controllers;
using RestAirline.Api.HyperMedia;

namespace RestAirline.Api.Resources
{
    public class RestAirlineHomeResource
    {
        [Obsolete("For serialization")]
        public RestAirlineHomeResource()
        {
            
        }

        public RestAirlineHomeResource(IUrlHelper urlHelper)
        {
            ResourceCommands = new Commands(urlHelper);
            ResourceLinks = new Links(urlHelper);
        }

        public Commands ResourceCommands { get; set; }

        public Links ResourceLinks { get; set; }


        public class Commands
        {
            public Commands(IUrlHelper urlHelper)
            {
                SearchTripsCommand = new SearchTripsCommand(urlHelper.Link((TripAvailabilityController c) => c.SearchTrips(null)));
            }

            public SearchTripsCommand SearchTripsCommand { get; set; }
        }

        public class Links
        {
            public Links(IUrlHelper urlHelper)
            {
                Self = urlHelper.Link((HomeController c) => c.Index());
            }

            public Link<RestAirlineHomeResource> Self { get; set; }
        }
    }
}