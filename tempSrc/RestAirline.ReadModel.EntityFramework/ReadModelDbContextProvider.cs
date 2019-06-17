using System;
using EventFlow.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace RestAirline.ReadModel.EntityFramework
{
    public class ReadModelDbContextProvider : IDbContextProvider<ReadModelDbContext>, IDisposable
    {
        private readonly DbContextOptions<ReadModelDbContext> _options;

        public ReadModelDbContextProvider(IConfiguration configuration)
        {
            _options = new DbContextOptionsBuilder<ReadModelDbContext>()
                .UseSqlServer(configuration["ReadModelConnectionString"])
                .Options;
        }

        public ReadModelDbContext CreateContext()
        {
            var context = new ReadModelDbContext(_options);
            context.Database.EnsureCreated();
            return context;
        }

        public void Dispose()
        {
        }
    }
}