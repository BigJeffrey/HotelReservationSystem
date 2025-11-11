using HotelReservationSystem.Application.DTOs.BookingDetails;
using HotelReservationSystem.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationSystem.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingDetailController(IBookingDetailService bookingDetailService) : ControllerBase
    {
        private readonly IBookingDetailService _bookingDetailService = bookingDetailService;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var details = await _bookingDetailService.GetAllAsync(page, pageSize);
            return !details.Items.Any() ? NotFound() : Ok(details);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var detail = await _bookingDetailService.GetByIdAsync(id);
            return detail is null ? NotFound() : Ok(detail);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookingDetailRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var created = await _bookingDetailService.AddAsync(request);
                return CreatedAtAction(nameof(GetById), new { id = created.BookingDetailId }, created);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBookingDetailRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updated = await _bookingDetailService.UpdateAsync(id, request);
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
                await _bookingDetailService.DeleteAsync(id);
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
