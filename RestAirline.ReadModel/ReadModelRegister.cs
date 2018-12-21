using System.Reflection;
using EventFlow;
using EventFlow.Extensions;

namespace RestAirline.ReadModel
{
    public static class ReadModelRegister
    {
        public static Assembly Assembly { get; } = typeof(ReadModelRegister).Assembly;
        
        public static IEventFlowOptions ConfigureReadModel(this IEventFlowOptions eventFlowOptions)
        {
            return eventFlowOptions
                .AddDefaults(Assembly)
                .UseInMemoryReadStoreFor<BookingReadModel>()
                .UseInMemoryReadStoreFor<StationsReadModel>();
        }
    }
}