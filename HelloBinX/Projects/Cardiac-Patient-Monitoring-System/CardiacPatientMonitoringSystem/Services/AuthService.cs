using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CardiacPatientMonitoringSystem.DTOs.Auth;
using CardiacPatientMonitoringSystem.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace CardiacPatientMonitoringSystem.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _configuration;

    public AuthService(
        UserManager<IdentityUser> userManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    // Creates a new Identity user and securely stores the password using ASP.NET Core Identity
    public async Task<IdentityResult> RegisterAsync(RegisterDto dto)
    {
        var user = new IdentityUser
        {
            UserName = dto.Email,
            Email = dto.Email
        };

        return await _userManager.CreateAsync(user, dto.Password);
    }

    // Validates user credentials and generates a JWT access token for successful login
    public async Task<string?> LoginAsync(LoginDto dto)
    {
        // Find the user by email
        var user = await _userManager.FindByEmailAsync(dto.Email);

        if (user is null)
        {
            return null;
        }

        // Verify the provided password against the stored password hash
        var passwordValid = await _userManager.CheckPasswordAsync(
            user,
            dto.Password);

        if (!passwordValid)
        {
            return null;
        }

        // Define the claims that will be included in the JWT
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email!)
        };

        // Create the signing key used to secure the JWT
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        // Read the token expiration time from the application configuration
        var expirationMinutes =
            int.Parse(_configuration["Jwt:ExpirationInMinutes"]!);

        // Create the JWT with issuer, audience, claims, expiration, and signing credentials
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
            signingCredentials: credentials);

        // Serialize the JWT into a string that can be returned to the client
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}