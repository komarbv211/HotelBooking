using Domain.Entities;
using Domain.ReadModels;

namespace Domain.Interfaces
{
    public interface IAdminBookingRepository
    {
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<IEnumerable<BookingStatistics>> GetBookingStatisticsAsync();
    }
}
