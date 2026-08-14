using CardiacPatientMonitoringSystem.DTOs.Patients;

namespace CardiacPatientMonitoringSystem.Services.Interfaces;

public interface IPatientService
{
    Task<IEnumerable<PatientResponseDto>> GetAllAsync();

    Task<PatientResponseDto?> GetByIdAsync(int id);

    Task<PatientResponseDto> CreateAsync(CreatePatientDto dto);

    // Update patient data for the authenticated owner
    Task<(PatientResponseDto? Patient, bool NotOwner)> UpdateAsync(
        int id,
        UpdatePatientDto dto,
        string userId);
    Task<bool> DeleteAsync(int id);
}