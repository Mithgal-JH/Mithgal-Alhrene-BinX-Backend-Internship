using CardiacPatientMonitoringSystem.DTOs.Auth;
using Microsoft.AspNetCore.Identity;

namespace CardiacPatientMonitoringSystem.Services.Interfaces;

public interface IAuthService
{
    Task<IdentityResult> RegisterPatientAsync(RegisterPatientDto dto);

    Task<IdentityResult> RegisterDoctorAsync(RegisterDoctorDto dto);

    Task<string?> LoginAsync(LoginDto dto);
}