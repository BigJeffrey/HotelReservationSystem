using HotelReservationSystem.Application.DTOs.Rooms;
using HotelReservationSystem.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationSystem.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController(IRoomService roomService) : ControllerBase
    {
        private readonly IRoomService _roomService = roomService;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var rooms = await _roomService.GetAllAsync(page, pageSize);
            return !rooms.Items.Any() ? NotFound() : Ok(rooms);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var room = await _roomService.GetByIdAsync(id);
            return room is null ? NotFound() : Ok(room);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRoomRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _roomService.AddAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = created.RoomId }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRoomRequest request)
        {
            var updated = await _roomService.UpdateAsync(id, request);
            return updated is null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _roomService.DeleteAsync(id);
            return NoContent();
        }
    }
}
