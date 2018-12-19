using System;
using Microsoft.AspNetCore.Mvc;
using RestAirline.Api.Controllers;
using RestAirline.Api.Hypermedia;
using RestAirline.Domain.Booking.Trip;

namespace RestAirline.Api.Resources.Booking
{
    public class ChangeFlightCommand : HypermediaCommand<FlightChangeResource>
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

    public class ChangeFlightCommand1 : HypermediaCommand<FlightChangeResource>
    {
        public ChangeFlightCommand1(IUrlHelper urlHelper) : base(urlHelper.Link((BookingController c) => c.ChangeFlight(null)))
        {
        }

        public Trip.Journey Journey { get; set; }

        public Flight Flight { get; set; }
    }

}