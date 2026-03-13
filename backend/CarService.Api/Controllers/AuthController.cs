using CarService.Api.DTOs;
using CarService.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<AuthResponse>> Register(RegisterRequest request)
    {
        var response = await authService.RegisterAsync(request);
        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(LoginRequest request)
    {
        var response = await authService.LoginAsync(request);
        return response is null ? Unauthorized() : Ok(response);
    }
}
