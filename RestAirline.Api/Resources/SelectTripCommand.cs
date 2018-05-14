using System;
using System.Collections.Generic;
using RestAirline.Api.HyperMedia;
using RestAirline.Domain.Booking;
using RestAirline.Domain.Shared;

namespace RestAirline.Api.Resources
{
    public class SelectTripCommand : HyperMediaCommand<SelectTripResultResource>
    {
        [Obsolete("For serialization")]
        public SelectTripCommand()
        {
        }

        public SelectTripCommand(Link<SelectTripResultResource> postUrl) : base(postUrl)
        {
            Passengers = new List<Passenger>();
        }

        public List<Passenger> Passengers { get; set; }
        public Trip Trip { get; set; }
    }
}