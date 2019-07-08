using System;
using EventFlow.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace RestAirline.ReadModel.EntityFramework.DBContext
{
    public class ReadModelDbContextProvider : IDbContextProvider<ReadModelContext>, IDisposable
    {
        private readonly DbContextOptions<ReadModelContext> _options;

        public ReadModelDbContextProvider(IConfiguration configuration)
        {
            _options = new DbContextOptionsBuilder<ReadModelContext>()
                .UseSqlServer(configuration["ReadModelConnectionString"])
                .Options;
        }

        public ReadModelContext CreateContext()
        {
            var context = new ReadModelContext(_options);
            context.Database.Migrate();
            return context;
        }

        public void Dispose()
        {
        }
    }
}