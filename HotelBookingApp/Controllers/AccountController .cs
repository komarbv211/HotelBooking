using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var user = await _userService.RegisterAsync(dto);
        if (user == null) return BadRequest("Помилка реєстрації");
        return Ok(user);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var user = await _userService.LoginAsync(dto);
        if (user == null) return Unauthorized("Невірний логін або пароль");
        return Ok(user);
    }

    [HttpPost("Logout")]
    public async Task<IActionResult> Logout()
    {
        await _userService.LogoutAsync();
        return Ok("Вихід виконано");
    }
    [HttpGet("Profile")]
    public async Task<IActionResult> Profile()
    {
        if (!User.Identity.IsAuthenticated) return Unauthorized();
        var email = User.Identity.Name;
        var user = await _userService.GetByEmailAsync(email);
        if (user == null) return NotFound();
        return Ok(user);
    }
}
