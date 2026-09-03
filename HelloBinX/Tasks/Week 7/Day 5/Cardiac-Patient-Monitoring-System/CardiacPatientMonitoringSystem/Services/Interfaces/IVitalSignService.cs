using CardiacPatientMonitoringSystem.DTOs.VitalSigns;

namespace CardiacPatientMonitoringSystem.Services.Interfaces;

public interface IVitalSignService
{
    Task<IEnumerable<VitalSignResponseDto>> GetAllAsync(
        string userId,
        bool isAdmin,
        bool isDoctor);

    Task<(VitalSignResponseDto? VitalSign, bool NotOwner)> GetByIdAsync(
        int id,
        string userId,
        bool isAdmin,
        bool isDoctor);

    Task<(VitalSignResponseDto? VitalSign, bool NotOwner)> CreateAsync(
        CreateVitalSignDto dto,
        string userId,
        bool isAdmin,
        bool isDoctor);

    Task<(VitalSignResponseDto? VitalSign, bool NotOwner)> UpdateAsync(
        int id,
        UpdateVitalSignDto dto,
        string userId,
        bool isAdmin,
        bool isDoctor);

    Task<bool> DeleteAsync(int id);
}