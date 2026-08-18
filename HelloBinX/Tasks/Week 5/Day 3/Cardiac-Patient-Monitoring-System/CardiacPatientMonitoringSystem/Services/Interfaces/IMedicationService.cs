using CardiacPatientMonitoringSystem.DTOs.Medications;

namespace CardiacPatientMonitoringSystem.Services.Interfaces;

public interface IMedicationService
{
    Task<IEnumerable<MedicationResponseDto>> GetAllAsync();

    Task<MedicationResponseDto?> GetByIdAsync(int id);

    Task<MedicationResponseDto> CreateAsync(CreateMedicationDto dto);

    Task<MedicationResponseDto?> UpdateAsync(
        int id,
        UpdateMedicationDto dto);

    Task<bool> DeleteAsync(int id);
}