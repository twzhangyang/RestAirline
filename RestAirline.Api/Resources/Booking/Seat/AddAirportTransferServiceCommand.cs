using System;
using RestAirline.Api.HyperMedia;

namespace RestAirline.Api.Resources.Booking.Seat
{
    public class AddAirportTransferServiceCommand : HyperMediaCommand<AddAirportTransferServiceResultResource>
    {
        [Obsolete("For serialization")]
        public AddAirportTransferServiceCommand()
        {
        }

        public AddAirportTransferServiceCommand(Link<AddAirportTransferServiceResultResource> postUrl) : base(postUrl)
        {
        }
    }
}