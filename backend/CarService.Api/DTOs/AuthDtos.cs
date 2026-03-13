using CarService.Api.Models;

namespace CarService.Api.DTOs;

public record RegisterRequest(string FullName, string Email, string PhoneNumber, string Password, UserRole Role);
public record LoginRequest(string Email, string Password);
public record AuthResponse(string Token, Guid UserId, string FullName, UserRole Role);
