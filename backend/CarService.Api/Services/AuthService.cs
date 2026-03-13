using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CarService.Api.Data;
using CarService.Api.DTOs;
using CarService.Api.Interfaces;
using CarService.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CarService.Api.Services;

public class AuthService(AppDbContext dbContext, IConfiguration configuration) : IAuthService
{
    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        var userExists = await dbContext.Users.AnyAsync(x => x.Email == request.Email);
        if (userExists) throw new InvalidOperationException("User already exists.");

        var user = new User
        {
            FullName = request.FullName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            PasswordHash = HashPassword(request.Password),
            Role = request.Role
        };

        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();

        return BuildAuthResponse(user);
    }

    public async Task<AuthResponse?> LoginAsync(LoginRequest request)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Email == request.Email && x.IsActive);
        if (user is null || user.PasswordHash != HashPassword(request.Password)) return null;

        return BuildAuthResponse(user);
    }

    private AuthResponse BuildAuthResponse(User user)
    {
        var key = configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key missing.");
        var issuer = configuration["Jwt:Issuer"];
        var audience = configuration["Jwt:Audience"];

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(ClaimTypes.Role, user.Role.ToString()),
            new(ClaimTypes.Name, user.FullName)
        };

        var token = new JwtSecurityToken(
            issuer,
            audience,
            claims,
            expires: DateTime.UtcNow.AddHours(12),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256)
        );

        return new AuthResponse(new JwtSecurityTokenHandler().WriteToken(token), user.Id, user.FullName, user.Role);
    }

    private static string HashPassword(string plainText)
    {
        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(plainText));
        return Convert.ToHexString(hash);
    }
}
