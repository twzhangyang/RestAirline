using System;
using Microsoft.AspNetCore.Mvc;
using RestAirline.Api.Controllers;
using RestAirline.Api.Hypermedia;

namespace RestAirline.Api.Resources.Booking.Seat
{
    public class AssignSeatAutomaticallyCommand : HypermediaCommand<AssignSeatAutomaticallyResultResource>
    {
        [Obsolete("For serialization")]
        public AssignSeatAutomaticallyCommand() { }

        public AssignSeatAutomaticallyCommand(IUrlHelper urlHelper) : base(urlHelper.Link((BookingController c) => c.AssignSeatAutomatically(null)))
        {

        }

        public Guid BookingId { get; set; }
    }
}