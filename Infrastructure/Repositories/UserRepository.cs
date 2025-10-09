using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public UserRepository(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<User?> RegisterAsync(User user, string password)
    {
        var appUser = new AppUser
        {
            UserName = user.Username,
            Email = user.Email,
            BirthDate = user.BirthDate
        };

        var result = await _userManager.CreateAsync(appUser, password);
        if (!result.Succeeded) return null;

        await _userManager.AddToRoleAsync(appUser, "Client");
        await _signInManager.SignInAsync(appUser, isPersistent: false);

        return new User
        {
            Id = appUser.Id,
            Username = appUser.UserName!,
            Email = appUser.Email!,
            BirthDate = appUser.BirthDate
        };
    }

    public async Task<User?> LoginAsync(string email, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(email, password, false, false);
        if (!result.Succeeded) return null;

        var appUser = await _userManager.FindByEmailAsync(email);
        if (appUser == null) return null;

        return new User
        {
            Id = appUser.Id,
            Username = appUser.UserName!,
            Email = appUser.Email!,
            BirthDate = appUser.BirthDate
        };
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        var appUser = await _userManager.FindByEmailAsync(email);
        if (appUser == null) return null;

        return new User
        {
            Id = appUser.Id,
            Username = appUser.UserName!,
            Email = appUser.Email!,
            BirthDate = appUser.BirthDate
        };
    }
}
