using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HotelsController : ControllerBase
{
    private readonly IHotelService _hotelService;

    public HotelsController(IHotelService hotelService)
    {
        _hotelService = hotelService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _hotelService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var hotel = await _hotelService.GetByIdAsync(id);
        return hotel == null ? NotFound() : Ok(hotel);
    }

    [HttpGet("city/{city}")]
    public async Task<IActionResult> GetByCity(string city)
        => Ok(await _hotelService.GetByCityAsync(city));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] HotelDto dto)
        => Ok(await _hotelService.CreateAsync(dto));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] HotelDto dto)
    {
        await _hotelService.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _hotelService.DeleteAsync(id);
        return NoContent();
    }
}
