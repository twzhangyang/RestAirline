using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RestAirline.Api.Controllers;
using RestAirline.Api.HyperMedia;
using RestAirline.Domain.Availability;
using RestAirline.Domain.Booking;

namespace RestAirline.Api.Resources.Availability
{
    public class SearchTripsCommand : HyperMediaCommand<TripAvailabilityResource>
    {
        [Obsolete("For serialization")]
        public SearchTripsCommand() { }

        public SearchTripsCommand(IUrlHelper urlHelper) : base(urlHelper.Link((TripAvailabilityController c)=>c.SearchTrips(null)))
        {
        }

        public TripSearchCriteria SearchCriteria { get; set; }
    }
}