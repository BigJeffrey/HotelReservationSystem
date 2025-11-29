using HotelReservationSystem.Application.DTOs.Customers;
using Microsoft.AspNetCore.Mvc;
using HotelReservationSystem.Application.Interfaces.Services;

namespace HotelReservationSystem.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController(ICustomerService customerService) : ControllerBase
    {
        private readonly ICustomerService _customerService = customerService;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var customers = await _customerService.GetAllAsync(page, pageSize);
            return !customers.Items.Any() ? NotFound() : Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            { 
                var newCustomer = await _customerService.AddAsync(request);
                return CreatedAtAction(nameof(GetById), new { id = newCustomer.CustomerId }, newCustomer);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCustomerRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var updatedCustomer = await _customerService.UpdateAsync(id, request);
                if (updatedCustomer == null)
                    return NotFound();

                return Ok(updatedCustomer);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            if (customer == null)
                return NotFound();

            await _customerService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("import")]
        public async Task<IActionResult> ImportCsv(IFormFile file)
        {
            if (!file.FileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
                return BadRequest("Invalid file type. Only CSV allowed.");

            try
            {
                using var stream = file.OpenReadStream();
                var result = await _customerService.ImportFromCsvAsync(stream);

                return Ok(new
                {
                    message = "CSV import completed.",
                    result.CreatedCount,
                    result.FailedCount,
                    result.Errors
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
