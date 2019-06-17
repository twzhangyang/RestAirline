using System.Reflection;
using EventFlow;
using EventFlow.Extensions;
using RestAirline.QueryHandlers.Journeys;

namespace RestAirline.QueryHandlers
{
    public static class QueryHandlersRegister
    {
        public static Assembly Assembly { get; } = typeof(QueryHandlersRegister).Assembly;
        
        public static IEventFlowOptions ConfigureBookingQueryHandlers(this IEventFlowOptions eventFlowOptions)
        {
            return eventFlowOptions
                .AddQueryHandlers(typeof(GetDepartureStationQueryHandler));
        }
    }
}