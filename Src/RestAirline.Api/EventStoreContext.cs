using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace RestAirline.Api
{
    public class EventStoreContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public EventStoreContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration["EventStoreConnectionString"]);
        }
    }
}