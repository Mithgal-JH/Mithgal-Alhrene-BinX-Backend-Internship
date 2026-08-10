using CardiacPatientMonitoringSystem.DTOs.PatientMedications;

namespace CardiacPatientMonitoringSystem.Services.Interfaces;

public interface IPatientMedicationService
{
    Task<IEnumerable<PatientMedicationResponseDto>> GetAllAsync();

    Task<PatientMedicationResponseDto?> GetByIdAsync(int id);

    Task<PatientMedicationResponseDto?> CreateAsync(
        CreatePatientMedicationDto dto);

    Task<PatientMedicationResponseDto?> UpdateAsync(
        int id,
        UpdatePatientMedicationDto dto);

    Task<bool> DeleteAsync(int id);
}