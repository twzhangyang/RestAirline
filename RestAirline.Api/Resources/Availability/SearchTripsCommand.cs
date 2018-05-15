using System;
using System.Collections.Generic;
using RestAirline.Api.HyperMedia;
using RestAirline.Domain.Booking;

namespace RestAirline.Api.Resources.Availability
{
    public class SearchTripsCommand : HyperMediaCommand<TripAvailabilityResource>
    {
        [Obsolete("For serialization")]
        public SearchTripsCommand()
        {
        }

        public SearchTripsCommand(Link<TripAvailabilityResource> postLink) : base(postLink)
        {
            Passengers = new List<Passenger>();
        }

        public string DepartureStation { get; set; }

        public DateTime DepartureDateTime { get; set; }

        public string ArriveStation { get; set; }

        public DateTime ArriveDateTime { get; set; }

        public List<Passenger> Passengers { get; set; }
    }
}