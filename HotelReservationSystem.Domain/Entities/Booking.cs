using HotelReservationSystem.Domain.Entities;
using System;
using System.Collections.Generic;

namespace HotelReservationSystem.Domain.Entities
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int CustomerId { get; set; }
        public DateTime BookingDate { get; set; } = DateTime.UtcNow;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = "pending";

        public Customer Customer { get; set; } = null!;
        public ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public ICollection<BookingService> BookingServices { get; set; } = new List<BookingService>();
    }
}
