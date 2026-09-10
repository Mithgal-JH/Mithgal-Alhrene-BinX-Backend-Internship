using CardiacPatientMonitoringSystem.DTOs.Patients;

namespace CardiacPatientMonitoringSystem.Services.Interfaces;

public interface IPatientService
{
    Task<PaginatedResponseDto<PatientResponseDto>> GetAllAsync(
        int page,
        int pageSize,
        string? search,
        string? gender,
        string? sort);

    Task<PaginatedResponseDto<PatientResponseDto>> GetMyPatientsAsync(
        string userId,
        int page,
        int pageSize,
        string? search,
        string? gender,
        string? sort);

    Task<(PatientResponseDto? Patient, bool NotOwner)> GetMyPatientAsync(
        string userId);

    Task<(PatientResponseDto? Patient, bool NotOwner)> GetByIdAsync(
        int id,
        string userId,
        bool isAdmin,
        bool isDoctor);

    Task<PatientResponseDto> CreateAsync(
        CreatePatientDto dto);

    Task<(PatientResponseDto? Patient, bool NotOwner)> UpdateAsync(
        int id,
        UpdatePatientDto dto,
        string userId,
        bool isDoctor);

    Task<bool> DeleteAsync(int id);
}