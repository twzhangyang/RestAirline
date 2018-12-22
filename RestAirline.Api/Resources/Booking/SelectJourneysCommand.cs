using System;
using Microsoft.AspNetCore.Mvc;
using RestAirline.Api.Controllers;
using RestAirline.Api.Hypermedia;

namespace RestAirline.Api.Resources.Booking
{
    public class SelectJourneysCommand : HypermediaCommand<JourneysSelectionResource>
    {
        [Obsolete("For serialization")]
        public SelectJourneysCommand() { }

        public SelectJourneysCommand(IUrlHelper urlHelper) : base(urlHelper.Link((BookingController c) => c.SelectJourneys1()))
        {
        }
    }
}