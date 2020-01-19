using EventFlow;
using EventFlow.Configuration;

namespace RestAirline.FlightAvailability.Management
{
    public class ManagementModule : IModule
    {
        public void Register(IEventFlowOptions eventFlowOptions)
        {
            eventFlowOptions.RegisterServices(s => s.Register<FlightsScheduler, FlightsScheduler>());
        }
    }
}