using Microsoft.AspNetCore.Mvc;
using RestAirline.FlightAvailability.Api.Controllers;
using RestAirline.Web.Hypermedia;

namespace RestAirline.FlightAvailability.Api.Resources
{
    public class ScheduleFlightsCommand : HypermediaCommand<FlightAvailabilityResource>
    {
        public ScheduleFlightsCommand() { }

        public ScheduleFlightsCommand(IUrlHelper urlHelper): base(urlHelper.Link((AvailabilityController c) => c.ScheduleFlights()))
        {
        }
    }
}