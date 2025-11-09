using HotelReservationSystem.Application.DTOs.ExtraServices;
using HotelReservationSystem.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationSystem.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExtraServiceController(IExtraServiceService extraServiceService) : ControllerBase
    {
        private readonly IExtraServiceService _extraServiceService = extraServiceService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        { 
            var services = await _extraServiceService.GetAllAsync();
            return !services.Any() ? NotFound() : Ok(services);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var service = await _extraServiceService.GetByIdAsync(id);
            return service is null ? NotFound() : Ok(service);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateExtraServiceRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _extraServiceService.AddAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = created.ExtraServiceId }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateExtraServiceRequest request)
        {
            var updated = await _extraServiceService.UpdateAsync(id, request);
            return updated is null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _extraServiceService.DeleteAsync(id);
            return NoContent();
        }
    }
}
