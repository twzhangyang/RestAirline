using System;
using RestAirline.Api.HyperMedia;

namespace RestAirline.Api.Resources.Booking
{
    public class ChangeFlightCommand : HyperMediaCommand<ChangeFlightResultResource>
    {
        [Obsolete("For serialization")]
        public ChangeFlightCommand()
        {
        }

        public ChangeFlightCommand(Link<ChangeFlightResultResource> postUrl) : base(postUrl)
        {

        }
    }
}