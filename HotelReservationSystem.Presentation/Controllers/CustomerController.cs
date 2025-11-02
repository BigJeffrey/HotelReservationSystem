using Microsoft.AspNetCore.Mvc;
using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Domain.Entities;

namespace HotelReservationSystem.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController(ICustomerService customerService) : ControllerBase
    {
        private readonly ICustomerService _customerService = customerService;

        // GET: api/customers
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        // GET: api/customers/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        // POST: api/customers
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _customerService.AddCustomerAsync(customer);
            return CreatedAtAction(nameof(GetById), new { id = customer.CustomerId }, customer);
        }

        // PUT: api/customers/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Customer updatedCustomer)
        {
            if (id != updatedCustomer.CustomerId)
                return BadRequest("ID mismatch");

            var existing = await _customerService.GetCustomerByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _customerService.UpdateCustomerAsync(updatedCustomer);
            return NoContent();
        }

        // DELETE: api/customers/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
                return NotFound();

            await _customerService.DeleteCustomerAsync(id);
            return NoContent();
        }
    }
}
