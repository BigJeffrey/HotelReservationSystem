using HotelReservationSystem.Application.DTOs.Payments;
using HotelReservationSystem.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationSystem.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController(IPaymentService paymentService) : ControllerBase
    {
        private readonly IPaymentService _paymentService = paymentService;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var payments = await _paymentService.GetAllAsync(page, pageSize);
            return !payments.Items.Any() ? NotFound() : Ok(payments);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var payment = await _paymentService.GetByIdAsync(id);
            return payment is null ? NotFound() : Ok(payment);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePaymentRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _paymentService.AddAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = created.PaymentId }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePaymentRequest request)
        {
            var updated = await _paymentService.UpdateAsync(id, request);
            return updated is null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _paymentService.DeleteAsync(id);
            return NoContent();
        }
    }
}
