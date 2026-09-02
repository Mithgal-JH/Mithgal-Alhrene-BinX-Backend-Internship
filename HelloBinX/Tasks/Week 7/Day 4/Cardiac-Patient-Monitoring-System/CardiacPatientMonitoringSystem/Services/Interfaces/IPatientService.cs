using CardiacPatientMonitoringSystem.DTOs.Patients;

namespace CardiacPatientMonitoringSystem.Services.Interfaces;

public interface IPatientService
{
    Task<PaginatedResponseDto<PatientResponseDto>> GetAllAsync(int page,
                                                                    int pageSize,
                                                                    string? search,
                                                                    string? gender,
                                                                    string? sort);

    Task<(PatientResponseDto? Patient, bool NotOwner)> GetByIdAsync(
        int id,
        string userId,
        bool isPatient);

    Task<PatientResponseDto> CreateAsync(CreatePatientDto dto);

    // Update patient data only for the authenticated owner
    Task<(PatientResponseDto? Patient, bool NotOwner)> UpdateAsync(
        int id,
        UpdatePatientDto dto,
        string userId);

    Task<bool> DeleteAsync(int id);
}