using System;
using Microsoft.AspNetCore.Mvc;
using RestAirline.FlightAvailability.Api.Controllers;
using RestAirline.Web.Hypermedia;

namespace RestAirline.FlightAvailability.Api.Resources
{
    public class FlightAvailabilityHomeResource
    {
        [Obsolete("For serialization")]
        public FlightAvailabilityHomeResource()
        {
        }

        public FlightAvailabilityHomeResource(IUrlHelper urlHelper)
        {
            ResourceCommands = new Commands(urlHelper);
            ResourceLinks = new Links(urlHelper);
        }

        public Commands ResourceCommands { get; set; }

        public Links ResourceLinks { get; set; }


        public class Commands
        {
            [Obsolete("For serialization")]
            public Commands()
            {
            }

            public Commands(IUrlHelper urlHelper)
            {
                ScheduleFlights = new ScheduleFlightsCommand(urlHelper);
            }
            
            public ScheduleFlightsCommand ScheduleFlights { get; set; }
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
                FlightAvailability = _urlHelper.Link((AvailabilityController c) => c.GetFlightAvailability("MEL"));
            }

            public Link<FlightAvailabilityHomeResource> Self { get; set; }
            
            public Link<FlightAvailabilityResource> FlightAvailability { get; set; }
        }
    }
}