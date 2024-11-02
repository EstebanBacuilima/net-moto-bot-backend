using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using net_moto_bot.Domain.Entities;
using net_moto_bot.Infrastructure.Connectoins;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace net_moto_bot.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController (IConfiguration _configuration, PostgreSQLContext _context) : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest loginRequest)
    {
        // Validar usuario y obtener roles
        User? user = _context.Users.AsNoTracking().FirstOrDefault(u => u.Email == loginRequest.Email && u.Password == loginRequest.Password);

        if (user == null || !user.Disabled)
            return Unauthorized();

        var roles = _context.UserRoles
            .Where(ur => ur.UserId == user.Id)
            .Select(ur => ur.Role.Name)
            .ToList();

        var token = GenerateJwtToken(user, roles);
        return Ok(new { token });
    }

    [HttpGet("all")]
    [Authorize]
    public IActionResult Get()
    {
        // Validar usuario y obtener roles
        List<User> users = [.. _context.Users.AsNoTracking()];

        return Ok(new { users });
    }

    private string GenerateJwtToken(User user, List<string> roles)
    {
        var jwtSettings = _configuration.GetSection("Jwt");

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.DisplayName ?? string.Empty),
            new ("UserId", user.Id.ToString())
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings["Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpiresInMinutes"])),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}


public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}