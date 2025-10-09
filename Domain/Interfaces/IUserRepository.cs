using Domain.Entities;

namespace Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> RegisterAsync(User user, string password);
    Task<User?> LoginAsync(string email, string password);
    Task LogoutAsync();
    Task<User?> GetByEmailAsync(string email);
}
