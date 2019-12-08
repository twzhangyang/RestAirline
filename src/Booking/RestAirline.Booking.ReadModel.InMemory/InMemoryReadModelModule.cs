using System.Reflection;
using EventFlow;
using EventFlow.Configuration;
using EventFlow.Extensions;

namespace RestAirline.Booking.ReadModel.InMemory
{
    public class InMemoryReadModelModule : IModule
    {
        public void Register(IEventFlowOptions eventFlowOptions)
        {
            eventFlowOptions.AddDefaults(typeof(InMemoryReadModelModule).Assembly)
                .UseInMemoryReadStoreFor<BookingReadModel>()
                .UseInMemoryReadStoreFor<StationsReadModel>();
        }
    }
}