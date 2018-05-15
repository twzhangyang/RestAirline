using System;
using Microsoft.AspNetCore.Mvc;
using RestAirline.Api.Controllers;
using RestAirline.Api.HyperMedia;
using RestAirline.Domain.Shared;

namespace RestAirline.Api.Resources.Booking
{
    public class ChangeFlightCommand : HyperMediaCommand<ChangeFlightResultResource>
    {
        [Obsolete("For serialization")]
        public ChangeFlightCommand()
        {
        }

        public ChangeFlightCommand(IUrlHelper urlHelper) : base(urlHelper.Link((BookingController c) => c.ChangeFlight(null)))
        {

        }

        public Guid BookingId { get; set; }

        public Trip.Journey Journey { get; set; }

        public Flight Flight { get; set; }
    }
}