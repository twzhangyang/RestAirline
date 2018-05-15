using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RestAirline.Api.Controllers;
using RestAirline.Api.HyperMedia;
using RestAirline.Domain.Booking;

namespace RestAirline.Api.Resources.Availability
{
    public class SearchTripsCommand : HyperMediaCommand<TripAvailabilityResource>
    {
        [Obsolete("For serialization")]
        public SearchTripsCommand() { }

        public SearchTripsCommand(IUrlHelper urlHelper) : base(urlHelper.Link((TripAvailabilityController c)=>c.SearchTrips(null)))
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