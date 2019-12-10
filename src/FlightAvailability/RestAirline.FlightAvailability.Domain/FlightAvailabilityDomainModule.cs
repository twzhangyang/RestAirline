using EventFlow;
using EventFlow.Configuration;
using EventFlow.Extensions;

namespace RestAirline.FlightAvailability.Domain
{
    public class FlightAvailabilityDomainModule : IModule
    {
        public void Register(IEventFlowOptions eventFlowOptions)
        {
            eventFlowOptions
                .AddDefaults(typeof(FlightAvailabilityDomainModule).Assembly);
        }
    }
}