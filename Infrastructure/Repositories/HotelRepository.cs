using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class HotelRepository : GenericRepository<Hotel>, IHotelRepository
{
    public HotelRepository(HotelBookingDbContext context) : base(context) { }

    public async Task<IEnumerable<Hotel>> GetHotelsByCityAsync(string city)
        => await _context.Hotels.Where(h => h.Address.Contains(city)).ToListAsync();
}

