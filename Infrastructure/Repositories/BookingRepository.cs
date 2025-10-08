using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BookingRepository : GenericRepository<Booking>, IBookingRepository
{
    public BookingRepository(HotelBookingDbContext context) : base(context) { }

    public async Task<IEnumerable<Booking>> GetBookingsByUserAsync(string userId)
        => await _context.Bookings
            .Include(b => b.Room)
            .Where(b => b.UserId == userId)
            .ToListAsync();

    public async Task<int> GetBookingCountForHotelAsync(int hotelId)
        => await _context.Bookings
            .CountAsync(b => b.Room.HotelId == hotelId);
}

