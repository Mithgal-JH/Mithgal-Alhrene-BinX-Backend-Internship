using CardiacPatientMonitoringSystem.DTOs.Doctors;

namespace CardiacPatientMonitoringSystem.Services.Interfaces;

public interface IDoctorService
{
    Task<IEnumerable<DoctorResponseDto>> GetAllAsync();

    Task<DoctorResponseDto?> GetByIdAsync(int id);

    Task<(DoctorResponseDto? Doctor, bool LicenseExists)> CreateAsync(
        CreateDoctorDto dto);

    Task<(DoctorResponseDto? Doctor, bool LicenseExists)> UpdateAsync(
        int id,
        UpdateDoctorDto dto);

    Task<bool> DeleteAsync(int id);
}