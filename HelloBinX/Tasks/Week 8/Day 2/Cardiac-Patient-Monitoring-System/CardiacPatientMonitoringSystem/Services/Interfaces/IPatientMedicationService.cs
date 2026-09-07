using CardiacPatientMonitoringSystem.DTOs.PatientMedications;

namespace CardiacPatientMonitoringSystem.Services.Interfaces;

public interface IPatientMedicationService
{
    Task<IEnumerable<PatientMedicationResponseDto>> GetAllAsync(
        string userId,
        bool isAdmin,
        bool isDoctor);

    Task<(PatientMedicationResponseDto? PatientMedication, bool NotOwner)>
        GetByIdAsync(
            int id,
            string userId,
            bool isAdmin,
            bool isDoctor);

    Task<(PatientMedicationResponseDto? PatientMedication, bool NotOwner)>
        CreateAsync(
            CreatePatientMedicationDto dto,
            string userId,
            bool isAdmin,
            bool isDoctor);

    Task<(PatientMedicationResponseDto? PatientMedication, bool NotOwner)>
        UpdateAsync(
            int id,
            UpdatePatientMedicationDto dto,
            string userId,
            bool isAdmin,
            bool isDoctor);

    Task<bool> DeleteAsync(int id);
}