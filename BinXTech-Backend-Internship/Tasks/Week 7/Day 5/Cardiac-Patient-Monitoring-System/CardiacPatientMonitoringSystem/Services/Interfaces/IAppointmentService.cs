using CardiacPatientMonitoringSystem.DTOs.Appointments;

namespace CardiacPatientMonitoringSystem.Services.Interfaces;

public interface IAppointmentService
{
    Task<IEnumerable<AppointmentResponseDto>> GetAllAsync(
        string userId,
        bool isAdmin,
        bool isDoctor);

    Task<(AppointmentResponseDto? Appointment, bool NotOwner)> GetByIdAsync(
        int id,
        string userId,
        bool isAdmin,
        bool isDoctor);

    Task<(AppointmentResponseDto? Appointment, bool NotOwner)> CreateAsync(
        CreateAppointmentDto dto,
        string userId,
        bool isAdmin,
        bool isDoctor);

    Task<(AppointmentResponseDto? Appointment, bool NotOwner)> UpdateAsync(
        int id,
        UpdateAppointmentDto dto,
        string userId,
        bool isAdmin,
        bool isDoctor);

    Task<bool> DeleteAsync(int id);
}