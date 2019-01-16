using System;
using EventFlow.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace RestAirline.ReadModel.EntityFramework
{
    public class ReadModelDbContextProvider : IDbContextProvider<ReadModelDbContext>, IDisposable
    {
        private readonly DbContextOptions<ReadModelDbContext> _options;

        public ReadModelDbContextProvider()
        {
            _options = new DbContextOptionsBuilder<ReadModelDbContext>()
                .UseSqlServer(@"Server=mssql.data;Database=RestAirlineRead;User Id=sa;Password=RestAirline123")
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