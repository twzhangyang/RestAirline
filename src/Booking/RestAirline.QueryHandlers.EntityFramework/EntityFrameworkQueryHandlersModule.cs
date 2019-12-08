using EventFlow;
using EventFlow.Configuration;
using EventFlow.Extensions;

namespace RestAirline.QueryHandlers.EntityFramework
{
    public class EntityFrameworkQueryHandlersModule : IModule
    {
        public void Register(IEventFlowOptions eventFlowOptions)
        {
            eventFlowOptions.AddQueryHandlers(typeof(EntityFrameworkQueryHandlersModule).Assembly);
        }
    }
}