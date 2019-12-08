using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using RestAirline.Booking.ReadModel.EntityFramework.Booking;

namespace RestAirline.Booking.ReadModel.EntityFramework.DBContext
{
    public class RestAirlineReadModelContext : DbContext
    {
        public RestAirlineReadModelContext(DbContextOptions<RestAirlineReadModelContext> options) : base(options)
        {
        }

        public DbSet<BookingReadModel> Bookings { get; set; }

        public DbSet<Passenger> Passengers { get; set; }

        public DbSet<Journey> Journeys { get; set; }

        public DbSet<Flight> Flights { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookingReadModel>()
                .HasMany<Passenger>()
                .WithOne()
                ;

            modelBuilder.Entity<BookingReadModel>()
                .HasMany<Journey>()
                .WithOne()
                ;

            modelBuilder.Entity<Journey>()
                .HasOne(j => j.Flight)
                .WithOne(f => f.Journey)
                .HasForeignKey<Flight>(f => f.JourneyId)
                ;
        }
    }

    public class ReadModelDbContextDesignFactory : IDesignTimeDbContextFactory<RestAirlineReadModelContext>
    {
        public RestAirlineReadModelContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RestAirlineReadModelContext>()
                .UseSqlServer("Server=localhost;Database=RestAirlineRead;User Id=sa;Password=RestAirline123");

            return new RestAirlineReadModelContext(optionsBuilder.Options);
        }
    }
}