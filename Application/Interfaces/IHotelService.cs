using Application.DTOs;

namespace Application.Interfaces;

public interface IHotelService
{
    Task<IEnumerable<HotelDto>> GetAllAsync();
    Task<HotelDto?> GetByIdAsync(int id);
    Task<IEnumerable<HotelDto>> GetByCityAsync(string city);
    Task<HotelDto> CreateAsync(HotelDto dto);
    Task UpdateAsync(int id, HotelDto dto);
    Task DeleteAsync(int id);
}
