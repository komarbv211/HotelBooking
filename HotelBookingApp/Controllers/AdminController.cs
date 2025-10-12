using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingApp.Controllers;

[ApiController]
[Route("api/admin")]
[Authorize(Roles = "Admin")] 
public class AdminController : ControllerBase
{
    private readonly IHotelService _hotelService;
    private readonly IRoomService _roomService;
    private readonly IBookingService _bookingService;
    private readonly IAdminBookingService _adminBookingService;
    public AdminController(
        IHotelService hotelService,
        IRoomService roomService,
        IBookingService bookingService,
        IAdminBookingService adminBookingService)
    {
        _hotelService = hotelService;
        _roomService = roomService;
        _bookingService = bookingService;
        _adminBookingService = adminBookingService;
    }

    // --- CRUD Готелів ---
    [HttpGet("hotels")]
    public async Task<IActionResult> GetHotels() => Ok(await _hotelService.GetAllAsync());

    [HttpGet("hotels/{id}")]
    public async Task<IActionResult> GetHotelById(int id)
    {
        var hotel = await _hotelService.GetByIdAsync(id);
        return hotel == null ? NotFound() : Ok(hotel);
    }

    [HttpPost("hotels")]
    public async Task<IActionResult> CreateHotel([FromBody] HotelDto dto)
        => Ok(await _hotelService.CreateAsync(dto));

    [HttpPut("hotels/{id}")]
    public async Task<IActionResult> UpdateHotel(int id, [FromBody] HotelDto dto)
    {
        await _hotelService.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("hotels/{id}")]
    public async Task<IActionResult> DeleteHotel(int id)
    {
        await _hotelService.DeleteAsync(id);
        return NoContent();
    }

    // --- CRUD Кімнат ---
    [HttpGet("rooms")]
    public async Task<IActionResult> GetRooms() => Ok(await _roomService.GetAllAsync());

    [HttpGet("rooms/{id}")]
    public async Task<IActionResult> GetRoomById(int id)
    {
        var room = await _roomService.GetByIdAsync(id);
        return room == null ? NotFound() : Ok(room);
    }

    [HttpPost("rooms")]
    public async Task<IActionResult> CreateRoom([FromBody] RoomDto dto)
        => Ok(await _roomService.CreateAsync(dto));

    [HttpPut("rooms/{id}")]
    public async Task<IActionResult> UpdateRoom(int id, [FromBody] RoomDto dto)
    {
        await _roomService.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("rooms/{id}")]
    public async Task<IActionResult> DeleteRoom(int id)
    {
        await _roomService.DeleteAsync(id);
        return NoContent();
    }

    // --- Бронювання ---
    [HttpGet("bookings")]
    public async Task<IActionResult> GetAllBookings()
    {
        // отримати всі бронювання без прив’язки до конкретного користувача
        var bookings = await _adminBookingService.GetAllBookingsAsync();
        return Ok(bookings);
    }

    // --- Статистика ---
    [HttpGet("stats")]
    public async Task<IActionResult> GetBookingStats()
    {
        var stats = await _adminBookingService.GetBookingStatisticsAsync();
        return Ok(stats);
    }
}

