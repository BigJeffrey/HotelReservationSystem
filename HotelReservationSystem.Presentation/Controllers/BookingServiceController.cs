using HotelReservationSystem.Application.DTOs.BookingServices;
using HotelReservationSystem.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationSystem.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingServiceController(IBookingServiceService bookingServiceService) : ControllerBase
    {
        private readonly IBookingServiceService _bookingServiceService = bookingServiceService;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var services = await _bookingServiceService.GetAllAsync(page, pageSize);
            return !services.Items.Any() ? NotFound() : Ok(services);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var service = await _bookingServiceService.GetByIdAsync(id);
            return service is null ? NotFound() : Ok(service);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookingServiceRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var created = await _bookingServiceService.AddAsync(request);
                return CreatedAtAction(nameof(GetById), new { id = created.BookingServiceId }, created);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBookingServiceRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updated = await _bookingServiceService.UpdateAsync(id, request);
                return updated is null ? NotFound() : Ok(updated);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _bookingServiceService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }
    }
}
