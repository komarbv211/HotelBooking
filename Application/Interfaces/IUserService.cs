using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface IUserService
{
    Task<User?> RegisterAsync(RegisterDto dto);
    Task<User?> LoginAsync(LoginDto dto);
    Task LogoutAsync();
}
