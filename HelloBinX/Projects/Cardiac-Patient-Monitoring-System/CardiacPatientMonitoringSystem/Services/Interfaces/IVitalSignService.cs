using CardiacPatientMonitoringSystem.DTOs.VitalSigns;

namespace CardiacPatientMonitoringSystem.Services.Interfaces;

public interface IVitalSignService
{
    Task<IEnumerable<VitalSignResponseDto>> GetAllAsync();

    Task<VitalSignResponseDto?> GetByIdAsync(int id);

    Task<VitalSignResponseDto?> CreateAsync(
        CreateVitalSignDto dto);

    Task<bool> DeleteAsync(int id);
}