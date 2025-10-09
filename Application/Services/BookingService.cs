using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

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
        // Перевірка доступності кімнати
        var availableRooms = await _roomRepository.GetAvailableRoomsAsync(dto.RoomId, dto.CheckIn, dto.CheckOut);
        if (!availableRooms.Any(r => r.Id == dto.RoomId))
            throw new Exception("Room is not available for selected dates");

        var booking = new Booking
        {
            RoomId = dto.RoomId,
            UserId = dto.UserId,
            CheckIn = dto.CheckIn,
            CheckOut = dto.CheckOut
        };

        await _bookingRepository.AddAsync(booking);
        await _bookingRepository.SaveChangesAsync(); 

        var room = availableRooms.First(r => r.Id == dto.RoomId);

        return new BookingDto
        {
            RoomNumber = room.Number,
            HotelName = room.Hotel.Name,
            CheckIn = booking.CheckIn,
            CheckOut = booking.CheckOut
        };
    }
}
