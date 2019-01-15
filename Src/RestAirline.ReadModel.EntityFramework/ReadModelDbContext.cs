using EventFlow.EntityFramework.Extensions;
using Microsoft.EntityFrameworkCore;
using RestAirline.ReadModel.Booking;
using Journey = RestAirline.ReadModel.EntityFramework.Booking.Journey;
using Passenger = RestAirline.ReadModel.EntityFramework.Booking.Passenger;

namespace RestAirline.ReadModel.EntityFramework
{
    public class ReadModelDbContext : DbContext
    {
        public ReadModelDbContext(DbContextOptions<ReadModelDbContext> options) : base(options)
        {
        }
        
        public DbSet<BookingReadModel> Bookings { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .AddEventFlowEvents()
                .AddEventFlowSnapshots();
        }
    }
}