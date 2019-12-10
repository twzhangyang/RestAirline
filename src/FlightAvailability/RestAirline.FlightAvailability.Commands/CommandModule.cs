using EventFlow;
using EventFlow.Configuration;
using EventFlow.Extensions;

namespace RestAirline.FlightAvailability.Commands
{
    public class CommandModule: IModule
    {
        public void Register(IEventFlowOptions eventFlowOptions)
        {
            eventFlowOptions.AddDefaults(typeof(CommandModule).Assembly);
        }
    }
}