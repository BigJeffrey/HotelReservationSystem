using HotelReservationSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationSystem.Persistence
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingDetail> BookingDetails { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ExtraService> ExtraServices { get; set; }
        public DbSet<BookingService> BookingServices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapping tables
            modelBuilder.Entity<Customer>().ToTable("customers");
            modelBuilder.Entity<Room>().ToTable("rooms");
            modelBuilder.Entity<Booking>().ToTable("bookings");
            modelBuilder.Entity<BookingDetail>().ToTable("booking_details");
            modelBuilder.Entity<Payment>().ToTable("payments");
            modelBuilder.Entity<ExtraService>().ToTable("extra_services");
            modelBuilder.Entity<BookingService>().ToTable("booking_services");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId).HasColumnName("customer_id");
                entity.Property(e => e.FirstName).HasColumnName("first_name");
                entity.Property(e => e.LastName).HasColumnName("last_name");
                entity.Property(e => e.Email).HasColumnName("email");
                entity.Property(e => e.PhoneNumber).HasColumnName("phone_number");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at"); 
            });
        }
    }
}
