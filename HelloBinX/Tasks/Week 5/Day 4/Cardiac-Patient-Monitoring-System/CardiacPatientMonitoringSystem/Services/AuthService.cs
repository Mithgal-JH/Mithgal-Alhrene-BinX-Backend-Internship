using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CardiacPatientMonitoringSystem.Data;
using CardiacPatientMonitoringSystem.DTOs.Auth;
using CardiacPatientMonitoringSystem.Models;
using CardiacPatientMonitoringSystem.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace CardiacPatientMonitoringSystem.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(
        UserManager<IdentityUser> userManager,
        ApplicationDbContext context,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _context = context;
        _configuration = configuration;
    }

    public async Task<IdentityResult> RegisterPatientAsync(
        RegisterPatientDto dto)
    {
        await using var transaction =
            await _context.Database.BeginTransactionAsync();

        var user = new IdentityUser
        {
            UserName = dto.Email,
            Email = dto.Email
        };

        var result = await _userManager.CreateAsync(
            user,
            dto.Password);

        if (!result.Succeeded)
            return result;

        var roleResult = await _userManager.AddToRoleAsync(
            user,
            "Patient");

        if (!roleResult.Succeeded)
        {
            await transaction.RollbackAsync();
            return roleResult;
        }

        var patient = new Patient
        {
            UserId = user.Id,
            MedicalRecordNumber = dto.MedicalRecordNumber,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            DateOfBirth = dto.DateOfBirth,
            Gender = dto.Gender,
            Phone = dto.Phone,
            Email = dto.Email,
            Address = dto.Address,
            EmergencyContactName = dto.EmergencyContactName,
            EmergencyContactPhone = dto.EmergencyContactPhone,
            MedicalNotes = dto.MedicalNotes
        };

        _context.Patients.Add(patient);

        await _context.SaveChangesAsync();
        await transaction.CommitAsync();

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> RegisterDoctorAsync(
        RegisterDoctorDto dto)
    {
        await using var transaction =
            await _context.Database.BeginTransactionAsync();

        var user = new IdentityUser
        {
            UserName = dto.Email,
            Email = dto.Email
        };

        var result = await _userManager.CreateAsync(
            user,
            dto.Password);

        if (!result.Succeeded)
            return result;

        var roleResult = await _userManager.AddToRoleAsync(
            user,
            "Doctor");

        if (!roleResult.Succeeded)
        {
            await transaction.RollbackAsync();
            return roleResult;
        }

        var doctor = new Doctor
        {
            UserId = user.Id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Phone = dto.Phone,
            Specialization = dto.Specialization,
            LicenseNumber = dto.LicenseNumber
        };

        _context.Doctors.Add(doctor);

        await _context.SaveChangesAsync();
        await transaction.CommitAsync();

        return IdentityResult.Success;
    }

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

        // Get all roles assigned to the authenticated user
        var roles = await _userManager.GetRolesAsync(user);

        // Define the claims that will be included in the JWT
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email!)
        };

        // Add the user's roles to the JWT as role claims
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        // Create the signing key used to secure the JWT
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                _configuration["Jwt:Key"]!));

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        // Read the token expiration time from configuration
        var expirationMinutes =
            int.Parse(
                _configuration["Jwt:ExpirationInMinutes"]!);

        // Create the JWT
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                expirationMinutes),
            signingCredentials: credentials);

        // Serialize the JWT
        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }
}