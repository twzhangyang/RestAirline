using System;
using Microsoft.AspNetCore.Mvc;
using RestAirline.Api.Controllers;
using RestAirline.Api.Hypermedia;
using RestAirline.Api.Resources.Booking.Journey;

namespace RestAirline.Api.Resources
{
    public class RestAirlineHomeResource
    {
        [Obsolete("For serialization")]
        public RestAirlineHomeResource() { }

        public RestAirlineHomeResource(IUrlHelper urlHelper)
        {
            ResourceCommands = new Commands(urlHelper);
            ResourceLinks = new Links(urlHelper);
        }

        public Commands ResourceCommands { get; set; }

        public Links ResourceLinks { get; set; }


        public class Commands
        {
            private readonly IUrlHelper _urlHelper;

            public Commands(IUrlHelper urlHelper)
            {
                _urlHelper = urlHelper;
            }

            public SelectJourneysCommand SelectJourneysCommand => new SelectJourneysCommand(_urlHelper);
        }

        public class Links
        {
            private readonly IUrlHelper _urlHelper;

            public Links(IUrlHelper urlHelper)
            {
                _urlHelper = urlHelper;
            }

            public Link<RestAirlineHomeResource> Self => _urlHelper.Link((HomeController c) => c.Index());
        }
    }
}