using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RoomRepository : GenericRepository<Room>, IRoomRepository
{
    public RoomRepository(HotelBookingDbContext context) : base(context) { }

    public async Task<IEnumerable<Room>> GetAvailableRoomsAsync(int hotelId, DateTime checkIn, DateTime checkOut)
    {
        return await _context.Rooms
            .Where(r => r.HotelId == hotelId &&
                        !_context.Bookings.Any(b =>
                            b.RoomId == r.Id &&
                            b.CheckIn < checkOut &&
                            b.CheckOut > checkIn))
            .ToListAsync();
    }
}

