using System;
using EventFlow.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace RestAirline.ReadModel.EntityFramework.DBContext
{
    public class RestAirlineReadModelDbContextProvider : IDbContextProvider<RestAirlineReadModelContext>
    {
        private readonly DbContextOptions<RestAirlineReadModelContext> _options;

        public RestAirlineReadModelDbContextProvider(IConfiguration configuration)
        {
            _options = new DbContextOptionsBuilder<RestAirlineReadModelContext>()
                .UseSqlServer(configuration["ReadModelConnectionString"])
                .Options;
        }

        public RestAirlineReadModelContext CreateContext()
        {
            var context = new RestAirlineReadModelContext(_options);
            context.Database.Migrate();
            return context;
        }
    }
}