using CardiacPatientMonitoringSystem.DTOs.Doctors;

namespace CardiacPatientMonitoringSystem.Services.Interfaces;

public interface IDoctorService
{
    Task<IEnumerable<DoctorResponseDto>> GetAllAsync();

    Task<DoctorResponseDto?> GetByIdAsync(int id);

    Task<(DoctorResponseDto? Doctor, bool LicenseExists)> CreateAsync(
        CreateDoctorDto dto);

    // Updates a doctor and verifies that the authenticated user owns the doctor.
    Task<(DoctorResponseDto? Doctor, bool LicenseExists, bool NotOwner)> UpdateAsync(
        int id,
        UpdateDoctorDto dto,
        string userId);

    Task<bool> DeleteAsync(int id);
}