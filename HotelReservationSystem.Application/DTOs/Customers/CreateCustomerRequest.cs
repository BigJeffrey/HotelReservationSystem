using System.ComponentModel.DataAnnotations;

namespace HotelReservationSystem.Application.DTOs.Customers
{
    public class CreateCustomerRequest
    {
        [Required, MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Phone]
        public string? PhoneNumber { get; set; }
    }
}
