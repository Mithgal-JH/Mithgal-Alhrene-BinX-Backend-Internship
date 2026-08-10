using CardiacPatientMonitoringSystem.DTOs.Auth;
using Microsoft.AspNetCore.Identity;

namespace CardiacPatientMonitoringSystem.Services.Interfaces;

public interface IAuthService
{
    Task<IdentityResult> RegisterAsync(RegisterDto dto);

    Task<string?> LoginAsync(LoginDto dto);
}