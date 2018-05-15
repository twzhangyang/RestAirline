using System;
using Microsoft.AspNetCore.Mvc;
using RestAirline.Api.Controllers;
using RestAirline.Api.HyperMedia;
using RestAirline.Domain.Shared;

namespace RestAirline.Api.Resources.Booking.Seat
{
    public class AddAirportTransferServiceCommand : HyperMediaCommand<AddAirportTransferServiceResultResource>
    {
        [Obsolete("For serialization")]
        public AddAirportTransferServiceCommand() { }

        public AddAirportTransferServiceCommand(IUrlHelper urlHelper) : base(urlHelper.Link((BookingController c) => c.AddAirportTransferService(null)))
        {
        }

        public Guid BookingId { get; set; }
        public AirportTransfer AirportTransfer { get; set; }
    }
}