using EventFlow;
using EventFlow.Configuration;
using EventFlow.Extensions;

namespace RestAirline.QueryHandlers.MongoDB
{
    public class MongoDbQueryHandlersModule : IModule
    {
        public void Register(IEventFlowOptions eventFlowOptions)
        {
            eventFlowOptions.AddQueryHandlers(typeof(MongoDbQueryHandlersModule).Assembly);
        }
    }
}