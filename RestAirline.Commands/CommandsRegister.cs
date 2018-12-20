using System.Reflection;
using EventFlow;
using EventFlow.Extensions;

namespace RestAirline.CommandHandlers
{
    public static class CommandsRegister
    {
        public static Assembly Assembly { get; } = typeof(CommandsRegister).Assembly;
        
        public static IEventFlowOptions ConfigureBookingCommands(this IEventFlowOptions eventFlowOptions)
        {
            return eventFlowOptions
                .AddDefaults(Assembly);
        }
    }
}