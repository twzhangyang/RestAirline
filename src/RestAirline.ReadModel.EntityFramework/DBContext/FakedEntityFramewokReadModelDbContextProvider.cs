using EventFlow.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace RestAirline.ReadModel.EntityFramework.DBContext
{
    public class FakedEntityFramewokReadModelDbContextProvider : IDbContextProvider<ReadModelContext>
    {
        public ReadModelContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ReadModelContext>()
                .UseInMemoryDatabase("for testing")
                .Options;

            var context = new ReadModelContext(options);
            context.Database.Migrate();
            return context;
        }
    }
}