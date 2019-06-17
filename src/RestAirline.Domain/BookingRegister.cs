using System.Reflection;
using EventFlow;
using EventFlow.Extensions;

namespace RestAirline.Domain
{
    public static class BookingRegister
    {
        public static Assembly Assembly { get; } = typeof(BookingRegister).Assembly;
        
        public static IEventFlowOptions ConfigureBookingDomain(this IEventFlowOptions eventFlowOptions)
        {
            return eventFlowOptions
                .AddDefaults(Assembly);
        }
    }
}