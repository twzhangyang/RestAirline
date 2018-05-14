using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RestAirline.Api.Resources;
using RestAirline.Domain.Booking;
using RestAirline.Domain.Shared;

namespace RestAirline.Api.Controllers
{
    public class BookingController : Controller
    {
        [Route("api/booking/selectTrip")]
        public SelectTripResultResource SelectTrip(SelectTripCommand command)
        {
            return new SelectTripResultResource();
        }
    }
}