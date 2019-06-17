using System.Reflection;
using EventFlow;
using EventFlow.Extensions;

namespace RestAirline.ReadModel.InMemory
{
    public static class InMemoryReadModelRegister
    {
        public static Assembly Assembly { get; } = typeof(InMemoryReadModelRegister).Assembly;
        
        public static IEventFlowOptions ConfigureInMemoryReadModel(this IEventFlowOptions eventFlowOptions)
        {
            return eventFlowOptions
                .AddDefaults(Assembly)
                .UseInMemoryReadStoreFor<BookingReadModel>()
                .UseInMemoryReadStoreFor<StationsReadModel>();
        }
    }
}