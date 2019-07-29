using System.Reflection;
using EventFlow;
using EventFlow.Configuration;
using EventFlow.Extensions;

namespace RestAirline.QueryHandlers
{
    public class QueryHandlersModule : IModule
    {
        public void Register(IEventFlowOptions eventFlowOptions)
        {
            eventFlowOptions.AddQueryHandlers(typeof(QueryHandlersModule).Assembly);
        }
    }
}