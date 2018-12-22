using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RestAirline.Api.Controllers;
using RestAirline.Api.Hypermedia;
using RestAirline.Domain.Booking.Trip;

namespace RestAirline.Api.Resources.Booking
{
    public class SelectJourneysCommand : HypermediaCommand<BookingResource>
    {
        [Obsolete("For serialization")]
        public SelectJourneysCommand()
        {
            JourneyIds = new List<string>();
        }

        public SelectJourneysCommand(IUrlHelper urlHelper) : base(urlHelper.Link((BookingController c) => c.SelectJourneys(null)))
        {
        }
        
        public List<string> JourneyIds { get; set; }
    }
}