using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class HotelService : IHotelService
{
    private readonly IHotelRepository _hotelRepository;

    public HotelService(IHotelRepository hotelRepository)
    {
        _hotelRepository = hotelRepository;
    }

    public async Task<IEnumerable<HotelDto>> GetAllAsync()
    {
        var hotels = await _hotelRepository.GetAllAsync();
        return hotels.Select(h => new HotelDto
        {
            Id = h.Id,
            Name = h.Name,
            Address = h.Address,
            Description = h.Description
        });
    }

    public async Task<HotelDto?> GetByIdAsync(int id)
    {
        var hotel = await _hotelRepository.GetByIdAsync(id);
        if (hotel == null) return null;
        return new HotelDto
        {
            Id = hotel.Id,
            Name = hotel.Name,
            Address = hotel.Address,
            Description = hotel.Description
        };
    }

    public async Task<IEnumerable<HotelDto>> GetByCityAsync(string city)
    {
        var hotels = await _hotelRepository.GetHotelsByCityAsync(city);
        return hotels.Select(h => new HotelDto
        {
            Id = h.Id,
            Name = h.Name,
            Address = h.Address,
            Description = h.Description
        });
    }

    public async Task<HotelDto> CreateAsync(HotelDto dto)
    {
        var hotel = new Hotel
        {
            Name = dto.Name,
            Address = dto.Address,
            Description = dto.Description
        };

        await _hotelRepository.AddAsync(hotel);
        await _hotelRepository.SaveChangesAsync();

        dto.Id = hotel.Id;
        return dto;
    }

    public async Task UpdateAsync(int id, HotelDto dto)
    {
        var hotel = await _hotelRepository.GetByIdAsync(id);
        if (hotel == null) throw new Exception("Готель не знайдено");

        hotel.Name = dto.Name;
        hotel.Address = dto.Address;
        hotel.Description = dto.Description;

        await _hotelRepository.UpdateAsync(hotel);
    }

    public async Task DeleteAsync(int id)
    {
        var hotel = await _hotelRepository.GetByIdAsync(id);
        if (hotel == null) throw new Exception("Готель не знайдено");

        await _hotelRepository.DeleteAsync(hotel);
    }
}
