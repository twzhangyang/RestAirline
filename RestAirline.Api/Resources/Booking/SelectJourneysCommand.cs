using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RestAirline.Api.Controllers;
using RestAirline.Api.Hypermedia;
using RestAirline.Domain.Booking.Trip;

namespace RestAirline.Api.Resources.Booking
{
    public class SelectJourneysCommand : HypermediaCommand<JourneysSelectionResource>
    {
        [Obsolete("For serialization")]
        public SelectJourneysCommand() { }

        public SelectJourneysCommand(IUrlHelper urlHelper) : base(urlHelper.Link((BookingController c) => c.SelectJourneys1()))
        {
        }
        
        public List<Journey> Journeys { get; set; }
    }
}