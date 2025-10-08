using Domain.Entities;

namespace Domain.Interfaces;

public interface IBookingRepository : IGenericRepository<Booking>
{
    Task<IEnumerable<Booking>> GetBookingsByUserAsync(string userId);
    Task<int> GetBookingCountForHotelAsync(int hotelId);
}
