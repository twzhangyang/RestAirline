using Microsoft.AspNetCore.Mvc;
using RestAirline.Api.Resources.Booking.Checkin;

namespace RestAirline.Api.Controllers
{
    public class CheckinController : Controller
    {
        [Route("api/booking/{0}/checkin")]
        public CheckinResultResource Checkin(CheckinCommand command)
        {
            return new CheckinResultResource();
        }
    }
}