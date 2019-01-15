using System.Reflection;
using EventFlow;
using EventFlow.Extensions;

namespace RestAirline.CommandHandlers
{
    public static class CommandHandlersRegister
    {
        public static Assembly Assembly { get; } = typeof(CommandHandlersRegister).Assembly;
        
        public static IEventFlowOptions ConfigureBookingCommandHandlers(this IEventFlowOptions eventFlowOptions)
        {
            return eventFlowOptions
                .AddDefaults(Assembly);
        }
    }
}