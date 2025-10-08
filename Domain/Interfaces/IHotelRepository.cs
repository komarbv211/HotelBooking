using Domain.Entities;

namespace Domain.Interfaces;

public interface IHotelRepository : IGenericRepository<Hotel>
{
    Task<IEnumerable<Hotel>> GetHotelsByCityAsync(string city);
}
