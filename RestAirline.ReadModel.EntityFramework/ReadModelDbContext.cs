using EventFlow.EntityFramework.Extensions;
using Microsoft.EntityFrameworkCore;

namespace RestAirline.ReadModel.EntityFramework
{
    public class ReadModelDbContext : DbContext
    {
        public ReadModelDbContext(DbContextOptions<ReadModelDbContext> options) : base(options)
        {
        }
        
        public DbSet<BookingReadModel> Bookings { get; set; }
        
        public DbSet<StationsReadModel> Stations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
//            modelBuilder
//                .AddEventFlowEvents()
//                .AddEventFlowSnapshots();
        }
    }
}