using CardiacPatientMonitoringSystem.Data;
using CardiacPatientMonitoringSystem.DTOs.Appointments;
using CardiacPatientMonitoringSystem.Models;
using CardiacPatientMonitoringSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CardiacPatientMonitoringSystem.Services;

public class AppointmentService : IAppointmentService
{
    private readonly ApplicationDbContext _context;

    public AppointmentService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AppointmentResponseDto>> GetAllAsync()
    {
        return await _context.Appointments
            .AsNoTracking()
            .Select(a => new AppointmentResponseDto
            {
                AppointmentId = a.AppointmentId,
                PatientId = a.PatientId,
                DoctorId = a.DoctorId,
                AppointmentDate = a.AppointmentDate,
                AppointmentType = a.AppointmentType,
                Status = a.Status,
                Reason = a.Reason,
                Notes = a.Notes
            })
            .ToListAsync();
    }

    public async Task<AppointmentResponseDto?> GetByIdAsync(int id)
    {
        return await _context.Appointments
            .AsNoTracking()
            .Where(a => a.AppointmentId == id)
            .Select(a => new AppointmentResponseDto
            {
                AppointmentId = a.AppointmentId,
                PatientId = a.PatientId,
                DoctorId = a.DoctorId,
                AppointmentDate = a.AppointmentDate,
                AppointmentType = a.AppointmentType,
                Status = a.Status,
                Reason = a.Reason,
                Notes = a.Notes
            })
            .FirstOrDefaultAsync();
    }

    public async Task<AppointmentResponseDto?> CreateAsync(
        CreateAppointmentDto dto)
    {
        var patientExists = await _context.Patients
            .AnyAsync(p => p.PatientId == dto.PatientId);

        if (!patientExists)
            return null;

        var doctorExists = await _context.Doctors
            .AnyAsync(d => d.DoctorId == dto.DoctorId);

        if (!doctorExists)
            return null;

        var appointment = new Appointment
        {
            PatientId = dto.PatientId,
            DoctorId = dto.DoctorId,
            AppointmentDate = dto.AppointmentDate,
            AppointmentType = dto.AppointmentType,
            Status = dto.Status,
            Reason = dto.Reason,
            Notes = dto.Notes
        };

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();

        return await GetByIdAsync(appointment.AppointmentId);
    }

    public async Task<AppointmentResponseDto?> UpdateAsync(
        int id,
        UpdateAppointmentDto dto)
    {
        var appointment = await _context.Appointments
            .FirstOrDefaultAsync(a => a.AppointmentId == id);

        if (appointment is null)
            return null;

        var doctorExists = await _context.Doctors
            .AnyAsync(d => d.DoctorId == dto.DoctorId);

        if (!doctorExists)
            return null;

        appointment.DoctorId = dto.DoctorId;
        appointment.AppointmentDate = dto.AppointmentDate;
        appointment.AppointmentType = dto.AppointmentType;
        appointment.Status = dto.Status;
        appointment.Reason = dto.Reason;
        appointment.Notes = dto.Notes;

        await _context.SaveChangesAsync();

        return await GetByIdAsync(id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var appointment = await _context.Appointments
            .FirstOrDefaultAsync(a => a.AppointmentId == id);

        if (appointment is null)
            return false;

        _context.Appointments.Remove(appointment);
        await _context.SaveChangesAsync();

        return true;
    }
}