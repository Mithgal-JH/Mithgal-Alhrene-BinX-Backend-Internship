using CardiacPatientMonitoringSystem.DTOs.Appointments;

namespace CardiacPatientMonitoringSystem.Services.Interfaces;

public interface IAppointmentService
{
    Task<IEnumerable<AppointmentResponseDto>> GetAllAsync();

    Task<AppointmentResponseDto?> GetByIdAsync(int id);

    Task<AppointmentResponseDto?> CreateAsync(
        CreateAppointmentDto dto);

    Task<AppointmentResponseDto?> UpdateAsync(
        int id,
        UpdateAppointmentDto dto);

    Task<bool> DeleteAsync(int id);
}