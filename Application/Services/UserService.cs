using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<User?> RegisterAsync(RegisterDto dto)
        => _userRepository.RegisterAsync(new User
        {
            Username = dto.Username,
            Email = dto.Email,
            BirthDate = dto.BirthDate
        }, dto.Password);

    public Task<User?> LoginAsync(LoginDto dto)
        => _userRepository.LoginAsync(dto.Email, dto.Password);

    public Task LogoutAsync()
        => _userRepository.LogoutAsync();
    public Task<User?> GetByEmailAsync(string email)
    {
        return _userRepository.GetByEmailAsync(email);
    }
}
