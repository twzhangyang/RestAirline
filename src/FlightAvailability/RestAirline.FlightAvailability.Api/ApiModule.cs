using EventFlow;
using EventFlow.Configuration;

namespace RestAirline.FlightAvailability.Api
{
    public class ApiModule : IModule
    {
        public void Register(IEventFlowOptions eventFlowOptions)
        {
            eventFlowOptions.RegisterServices(s => s.Register<FlightsScheduler, FlightsScheduler>());
        }
    }
}