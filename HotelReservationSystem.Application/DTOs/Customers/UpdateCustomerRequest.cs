using System.ComponentModel.DataAnnotations;

namespace HotelReservationSystem.Application.DTOs.Customers
{
    public class UpdateCustomerRequest
    {
        [MaxLength(50)]
        public string? FirstName { get; set; }

        [MaxLength(50)]
        public string? LastName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }
    }
}
