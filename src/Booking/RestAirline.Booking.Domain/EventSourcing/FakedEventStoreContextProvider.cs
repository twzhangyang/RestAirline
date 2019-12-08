using EventFlow.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace RestAirline.Booking.Domain.EventSourcing
{
    public class FakedEventStoreContextProvider : IDbContextProvider<EventStoreContext>
    {
        public EventStoreContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<EventStoreContext>()
                .UseInMemoryDatabase("for testing")
                .Options;

            var context= new EventStoreContext(options);
            return context;
        }
    }
}