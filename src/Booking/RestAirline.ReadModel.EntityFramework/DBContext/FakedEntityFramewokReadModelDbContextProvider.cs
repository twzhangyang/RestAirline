using EventFlow.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace RestAirline.ReadModel.EntityFramework.DBContext
{
    public class FakedEntityFramewokReadModelDbContextProvider : IDbContextProvider<RestAirlineReadModelContext>
    {
        public RestAirlineReadModelContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<RestAirlineReadModelContext>()
                .UseInMemoryDatabase("for testing")
                .Options;

            var context= new RestAirlineReadModelContext(options);
            return context;
        }
    }
}