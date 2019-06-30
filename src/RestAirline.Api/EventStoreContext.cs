using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace RestAirline.Api
{
    public class User
    {
        [Key] public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public byte[] Timestamp { get; set; }
    }

    public class EventStoreContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public EventStoreContext(DbContextOptions<EventStoreContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(p => p.Timestamp)
                .IsRowVersion();
        }
    }
    
    public class EventStoreContextDesignFactory : IDesignTimeDbContextFactory<EventStoreContext>
    {
        public EventStoreContext CreateDbContext(string[] args)
        {
            var optionsBuilder =  new DbContextOptionsBuilder<EventStoreContext>()
                .UseSqlServer("Server=localhost;Database=RestAirline;User Id=sa;Password=RestAirline123");

            return new EventStoreContext(optionsBuilder.Options);
        }
    }
}