using System;
using Microsoft.AspNetCore.Mvc;
using RestAirline.Booking.Api.Controllers;
using RestAirline.Booking.Api.HyperMedia;
using RestAirline.Booking.Api.Resources.Booking.Journey;
using RestAirline.Web.Hypermedia;

namespace RestAirline.Booking.Api.Resources
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
            private readonly IUrlHelper _urlHelper;

            [Obsolete("For serialization")]
            public Commands()
            {
            }

            public Commands(IUrlHelper urlHelper)
            {
                _urlHelper = urlHelper;
                SelectJourneysCommand = new SelectJourneysCommand(_urlHelper);
            }

            public SelectJourneysCommand SelectJourneysCommand { get; set; }
        }

        public class Links
        {
            private readonly IUrlHelper _urlHelper;

            [Obsolete("For serialization")]
            public Links()
            {
            }

            public Links(IUrlHelper urlHelper)
            {
                _urlHelper = urlHelper;
                Self = _urlHelper.Link((HomeController c) => c.Index());
            }

            public Link<RestAirlineHomeResource> Self { get; set; }
        }
    }
}