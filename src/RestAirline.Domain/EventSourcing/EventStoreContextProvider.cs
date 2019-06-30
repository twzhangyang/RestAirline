using System;
using EventFlow.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace RestAirline.Domain.EventSourcing
{
    public class EventStoreContextProvider: IDbContextProvider<EventStoreContext>, IDisposable
    {
        private readonly IConfiguration _configuration;
        private DbContextOptions<EventStoreContext> _options;

        public EventStoreContextProvider(IConfiguration configuration)
        {
            _configuration = configuration;

            _options = new DbContextOptionsBuilder<EventStoreContext>()
                .UseSqlServer(configuration["EventStoreConnectionString"])
                .Options;
        }
        
        public EventStoreContext CreateContext()
        {
            var db = new EventStoreContext(_options);
            db.Database.EnsureCreated();

            return db;
        }

        public void Dispose()
        {
        }
    }
}