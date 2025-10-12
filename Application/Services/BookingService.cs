using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System.Data;

namespace Application.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IRoomRepository _roomRepository;

    public BookingService(IBookingRepository bookingRepository, IRoomRepository roomRepository)
    {
        _bookingRepository = bookingRepository;
        _roomRepository = roomRepository;
    }

    public async Task<IEnumerable<BookingDto>> GetUserBookingsAsync(string userId)
    {
        var bookings = await _bookingRepository.GetBookingsByUserAsync(userId);

        return bookings.Select(b => new BookingDto
        {
            RoomNumber = b.Room.Number,
            HotelName = b.Room.Hotel.Name,
            CheckIn = b.CheckIn,
            CheckOut = b.CheckOut
        });
    }

    public async Task<BookingDto> CreateBookingAsync(CreateBookingDto dto)
    {
        var room = await GetRoomOrThrowAsync(dto.RoomId);
        await EnsureRoomIsAvailableAsync(room, dto.CheckIn, dto.CheckOut);

        var booking = await CreateAndSaveBookingAsync(dto);

        return MapToBookingDto(room, booking);
    }

    private async Task<Room> GetRoomOrThrowAsync(int roomId)
    {
        var room = await _roomRepository.GetByIdAsync(roomId);
        if (room == null)
            throw new KeyNotFoundException("Кімната не знайдена.");

        return room;
    }

    private async Task EnsureRoomIsAvailableAsync(Room room, DateTime checkIn, DateTime checkOut)
    {
        var availableRooms = await _roomRepository.GetAvailableRoomsAsync(room.HotelId, checkIn, checkOut);
        if (!availableRooms.Any(r => r.Id == room.Id))
            throw new InvalidOperationException("Кімната недоступна на вибрані дати.");
    }

    private async Task<Booking> CreateAndSaveBookingAsync(CreateBookingDto dto)
    {
        var booking = new Booking
        {
            RoomId = dto.RoomId,
            UserId = dto.UserId,
            CheckIn = dto.CheckIn,
            CheckOut = dto.CheckOut
        };

        await _bookingRepository.AddAsync(booking);
        await _bookingRepository.SaveChangesAsync();

        return booking;
    }

    private static BookingDto MapToBookingDto(Room room, Booking booking)
    {
        return new BookingDto
        {
            RoomNumber = room.Number,
            HotelName = room.Hotel?.Name ?? string.Empty,
            CheckIn = booking.CheckIn,
            CheckOut = booking.CheckOut
        };
    }

    public async Task<IEnumerable<BookingDto>> GetAllBookingsAsync()
    {
        var bookings = await _bookingRepository.GetAllBookingsAsync();

        return bookings.Select(b => new BookingDto
        {
            RoomNumber = b.Room.Number,
            HotelName = b.Room.Hotel.Name,
            CheckIn = b.CheckIn,
            CheckOut = b.CheckOut,
            UserId = b.UserId
        });
    }
}
