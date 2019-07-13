using System.Reflection;
using EventFlow;
using EventFlow.Configuration;
using EventFlow.Extensions;
using RestAirline.QueryHandlers.Booking;
using RestAirline.QueryHandlers.Stations;

namespace RestAirline.QueryHandlers
{
    public class QueryHandlersModule : IModule
    {
        public void Register(IEventFlowOptions eventFlowOptions)
        {
            // Not sure why this not work
//            eventFlowOptions.AddQueryHandlers(typeof(QueryHandlersModule).Assembly);

            eventFlowOptions.RegisterServices(r => { r.Register<BookingQueryHandler, BookingQueryHandler>(); });
        }
    }
}