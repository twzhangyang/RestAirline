using System;
using Microsoft.AspNetCore.Mvc;
using RestAirline.Api.Controllers;
using RestAirline.Api.HyperMedia;

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
    }
}