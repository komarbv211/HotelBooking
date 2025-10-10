using Application.DTOs;

namespace Application.Interfaces;

public interface IRoomService
{
    Task<IEnumerable<RoomDto>> GetAllAsync();
    Task<RoomDto?> GetByIdAsync(int id);
    Task<IEnumerable<RoomDto>> GetAvailableRoomsAsync(int hotelId, DateTime checkIn, DateTime checkOut);
    Task<RoomDto> CreateAsync(RoomDto dto);
    Task UpdateAsync(int id, RoomDto dto);
    Task DeleteAsync(int id);
}
