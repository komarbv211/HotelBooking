using Application.DTOs;

namespace Application.Interfaces;

public interface IAdminBookingService
{
    Task<IEnumerable<BookingDto>> GetAllBookingsAsync();
    Task<BookingStatisticsDto> GetBookingStatisticsAsync();
}
