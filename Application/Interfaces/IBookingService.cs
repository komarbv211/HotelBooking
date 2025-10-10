using Application.DTOs;

namespace Application.Interfaces;

public interface IBookingService
{
    Task<IEnumerable<BookingDto>> GetUserBookingsAsync(string userId);
    Task<BookingDto> CreateBookingAsync(CreateBookingDto dto);
}
