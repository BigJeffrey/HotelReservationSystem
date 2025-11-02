using HotelReservationSystem.Domain.Entities;
using HotelReservationSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Domain.Entities {
    public class Room
    {
        public int RoomId { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public Enums.RoomType RoomType { get; set; }
        public decimal PricePerNight { get; set; }
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; } = true;

        public ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();
    }
}
