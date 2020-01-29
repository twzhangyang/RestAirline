using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RestAirline.FlightAvailability.ReadModel.Elasticsearch;

namespace RestAirline.FlightAvailability.Api.Resources
{
    public class FlightAvailabilityResource
    {
        [Obsolete]
        public FlightAvailabilityResource()
        {
        }

        public FlightAvailabilityResource(IUrlHelper urlHelper, List<FlightAvailabilityReadModel> flightAvailabilityReadModels)
        {
           ResourceLinks = new Links(urlHelper); 
           ResourceCommands = new Commands(urlHelper);
           Flights = flightAvailabilityReadModels.ToFlights();
        }
        
        public List<Flight> Flights { get; set; }
        
        public Links ResourceLinks { get; private set; }
        
        public Commands ResourceCommands { get; private set; }
        
        public class Links
        {
            [Obsolete]
            public Links() { }

            public Links(IUrlHelper urlHelper)
            {
            }
        }
        
        public class Commands
        {
            [Obsolete]
            public Commands() { }

            public Commands(IUrlHelper urlHelper)
            {
                
            }
                
        }
        
    }
}