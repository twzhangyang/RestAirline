using Microsoft.AspNetCore.Mvc;
using RestAirline.Api.Resources.Booking.Checkin;

namespace RestAirline.Api.Controllers
{
    public class CheckinController : Controller
    {
        [Route("api/booking/{0}/checkin")]
        public CheckinResource Checkin(CheckinCommand command)
        {
            return new CheckinResource();
        }
    }
}