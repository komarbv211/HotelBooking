using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    private readonly IRoomService _roomService;

    public RoomsController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _roomService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var room = await _roomService.GetByIdAsync(id);
        return room == null ? NotFound() : Ok(room);
    }

    [HttpGet("available")]
    public async Task<IActionResult> GetAvailable(int hotelId, DateTime checkIn, DateTime checkOut)
        => Ok(await _roomService.GetAvailableRoomsAsync(hotelId, checkIn, checkOut));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RoomDto dto)
        => Ok(await _roomService.CreateAsync(dto));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] RoomDto dto)
    {
        await _roomService.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _roomService.DeleteAsync(id);
        return NoContent();
    }
}
