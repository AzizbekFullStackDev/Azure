using Azure.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Azure.Data.DbContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Room> Rooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>()
                .HasOne(r => r.Hotel)   
                .WithMany(h => h.Rooms) 
                .HasForeignKey(r => r.HotelId); 

            modelBuilder.Entity<Booking>()
                .HasKey(b => new { b.HotelId, b.UserId });

        }
    }

}
