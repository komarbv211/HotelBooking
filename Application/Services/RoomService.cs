using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _roomRepository;

    public RoomService(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<IEnumerable<RoomDto>> GetAllAsync()
    {
        var rooms = await _roomRepository.GetAllAsync();
        return rooms.Select(r => new RoomDto
        {
            Id = r.Id,
            HotelId = r.HotelId,
            HotelName = r.Hotel?.Name ?? string.Empty,
            Number = r.Number,
            PricePerNight = r.PricePerNight,
            Capacity = r.Capacity
        });
    }

    public async Task<RoomDto?> GetByIdAsync(int id)
    {
        var room = await _roomRepository.GetByIdAsync(id);
        if (room == null) return null;
        return new RoomDto
        {
            Id = room.Id,
            HotelId = room.HotelId,
            HotelName = room.Hotel?.Name ?? string.Empty,
            Number = room.Number,
            PricePerNight = room.PricePerNight,
            Capacity = room.Capacity
        };
    }

    public async Task<IEnumerable<RoomDto>> GetAvailableRoomsAsync(int hotelId, DateTime checkIn, DateTime checkOut)
    {
        var rooms = await _roomRepository.GetAvailableRoomsAsync(hotelId, checkIn, checkOut);
        return rooms.Select(r => new RoomDto
        {
            Id = r.Id,
            HotelId = r.HotelId,
            HotelName = r.Hotel?.Name ?? string.Empty,
            Number = r.Number,
            PricePerNight = r.PricePerNight,
            Capacity = r.Capacity
        });
    }

    public async Task<RoomDto> CreateAsync(RoomDto dto)
    {
        var room = new Room
        {
            HotelId = dto.HotelId,
            Number = dto.Number,
            PricePerNight = dto.PricePerNight,
            Capacity = dto.Capacity
        };

        await _roomRepository.AddAsync(room);
        await _roomRepository.SaveChangesAsync();

        dto.Id = room.Id;
        return dto;
    }

    public async Task UpdateAsync(int id, RoomDto dto)
    {
        var room = await _roomRepository.GetByIdAsync(id);
        if (room == null) throw new Exception("Кімната не знайдена");

        room.Number = dto.Number;
        room.PricePerNight = dto.PricePerNight;
        room.Capacity = dto.Capacity;

        await _roomRepository.UpdateAsync(room);
    }

    public async Task DeleteAsync(int id)
    {
        var room = await _roomRepository.GetByIdAsync(id);
        if (room == null) throw new Exception("Кімната не знайдена");

        await _roomRepository.DeleteAsync(room);
    }
}
