using CardiacPatientMonitoringSystem.DTOs.Patients;

namespace CardiacPatientMonitoringSystem.Services.Interfaces;

public interface IPatientService
{
    Task<IEnumerable<PatientResponseDto>> GetAllAsync(
        string userId,
        bool isAdmin,
        bool isDoctor);

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