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
        public DbSet<BookingServiceEntity> BookingServices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapping tables
            modelBuilder.Entity<Customer>().ToTable("customers");
            modelBuilder.Entity<Room>().ToTable("rooms");
            modelBuilder.Entity<Booking>().ToTable("bookings");
            modelBuilder.Entity<BookingDetail>().ToTable("booking_details");
            modelBuilder.Entity<Payment>().ToTable("payments");
            modelBuilder.Entity<ExtraService>().ToTable("extra_services");
            modelBuilder.Entity<BookingServiceEntity>().ToTable("booking_services");

            // Mapping columns
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerId);
                entity.Property(e => e.CustomerId).HasColumnName("customer_id");
                entity.Property(e => e.FirstName).HasColumnName("first_name");
                entity.Property(e => e.LastName).HasColumnName("last_name");
                entity.Property(e => e.Email).HasColumnName("email");
                entity.Property(e => e.PhoneNumber).HasColumnName("phone_number");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.RoomId);
                entity.Property(e => e.RoomId).HasColumnName("room_id");
                entity.Property(e => e.RoomNumber).HasColumnName("room_number");
                entity.Property(e => e.RoomType).HasColumnName("room_type");
                entity.Property(e => e.PricePerNight).HasColumnName("price_per_night");
                entity.Property(e => e.Capacity).HasColumnName("capacity");
                entity.Property(e => e.IsAvailable).HasColumnName("is_available");
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(e => e.BookingId);
                entity.Property(e => e.BookingId).HasColumnName("booking_id");
                entity.Property(e => e.CustomerId).HasColumnName("customer_id");
                entity.Property(e => e.BookingDate).HasColumnName("booking_date");
                entity.Property(e => e.StartDate).HasColumnName("start_date");
                entity.Property(e => e.EndDate).HasColumnName("end_date");
                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<BookingDetail>(entity =>
            {
                entity.HasKey(e => e.BookingDetailId);
                entity.Property(e => e.BookingDetailId).HasColumnName("booking_detail_id");
                entity.Property(e => e.BookingId).HasColumnName("booking_id");
                entity.Property(e => e.RoomId).HasColumnName("room_id");
                entity.Property(e => e.Price).HasColumnName("price");
                entity.Property(e => e.Nights).HasColumnName("nights");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.PaymentId);
                entity.Property(e => e.PaymentId).HasColumnName("payment_id");
                entity.Property(e => e.BookingId).HasColumnName("booking_id");
                entity.Property(e => e.Amount).HasColumnName("amount");
                entity.Property(e => e.PaymentDate).HasColumnName("payment_date");
                entity.Property(e => e.PaymentMethod).HasColumnName("payment_method");
                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<ExtraService>(entity =>
            {
                entity.HasKey(e => e.ExtraServiceId);
                entity.Property(e => e.ExtraServiceId).HasColumnName("extra_service_id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.Price).HasColumnName("price");
            });

            modelBuilder.Entity<BookingServiceEntity>(entity =>
            {
                entity.HasKey(e => e.BookingServiceId);
                entity.Property(e => e.BookingServiceId).HasColumnName("booking_services_id");
                entity.Property(e => e.BookingId).HasColumnName("booking_id");
                entity.Property(e => e.ExtraServiceId).HasColumnName("extra_service_id");
                entity.Property(e => e.Quantity).HasColumnName("quantity");
                entity.Property(e => e.TotalPrice).HasColumnName("total_price");
            });
        }
    }
}
