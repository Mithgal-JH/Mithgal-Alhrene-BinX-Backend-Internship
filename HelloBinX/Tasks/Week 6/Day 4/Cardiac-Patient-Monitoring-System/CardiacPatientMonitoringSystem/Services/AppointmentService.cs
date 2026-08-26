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

    public async Task<IEnumerable<AppointmentResponseDto>> GetAllAsync(
        string userId,
        bool isAdmin,
        bool isDoctor)
    {
        var query = _context.Appointments
            .AsNoTracking()
            .AsQueryable();

        if (!isAdmin)
        {
            if (isDoctor)
            {
                query = query.Where(a => a.Doctor.UserId == userId);
            }
            else
            {
                query = query.Where(a => a.Patient.UserId == userId);
            }
        }

        return await query
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

    public async Task<(AppointmentResponseDto? Appointment, bool NotOwner)> GetByIdAsync(
        int id,
        string userId,
        bool isAdmin,
        bool isDoctor)
    {
        var appointment = await _context.Appointments
            .AsNoTracking()
            .Include(a => a.Doctor)
            .Include(a => a.Patient)
            .FirstOrDefaultAsync(a => a.AppointmentId == id);

        if (appointment is null)
            return (null, false);

        if (!isAdmin)
        {
            var isOwner = isDoctor
                ? appointment.Doctor.UserId == userId
                : appointment.Patient.UserId == userId;

            if (!isOwner)
                return (null, true);
        }

        return (new AppointmentResponseDto
        {
            AppointmentId = appointment.AppointmentId,
            PatientId = appointment.PatientId,
            DoctorId = appointment.DoctorId,
            AppointmentDate = appointment.AppointmentDate,
            AppointmentType = appointment.AppointmentType,
            Status = appointment.Status,
            Reason = appointment.Reason,
            Notes = appointment.Notes
        }, false);
    }

    public async Task<(AppointmentResponseDto? Appointment, bool NotOwner)> CreateAsync(
    CreateAppointmentDto dto,
    string userId,
    bool isAdmin,
    bool isDoctor)
    {
        var patient = await _context.Patients
            .FirstOrDefaultAsync(p => p.PatientId == dto.PatientId);

        if (patient is null)
            return (null, false);

        var doctor = await _context.Doctors
            .FirstOrDefaultAsync(d => d.DoctorId == dto.DoctorId);

        if (doctor is null)
            return (null, false);

        if (!isAdmin)
        {
            var isOwner = isDoctor
                ? doctor.UserId == userId
                : patient.UserId == userId;

            if (!isOwner)
                return (null, true);
        }

        if (string.IsNullOrEmpty(patient.UserId) ||
            string.IsNullOrEmpty(doctor.UserId))
            return (null, false);

        // Start transaction
        await using var transaction =
            await _context.Database.BeginTransactionAsync();

        try
        {
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

            // Create notification for the patient
            var patientNotification = new Notification
            {
                UserId = patient.UserId,
                AppointmentId = appointment.AppointmentId,
                Message = "A new appointment has been scheduled for you",
                CreatedAt = DateTime.UtcNow,
                IsRead = false
            };

            // Create notification for the doctor
            var doctorNotification = new Notification
            {
                UserId = doctor.UserId,
                AppointmentId = appointment.AppointmentId,
                Message = "A new appointment has been scheduled with you",
                CreatedAt = DateTime.UtcNow,
                IsRead = false
            };

            _context.Notifications.AddRange(
                patientNotification,
                doctorNotification);

            await _context.SaveChangesAsync();


            // Commit all changes together
            await transaction.CommitAsync();

            return (
                await BuildResponseAsync(appointment.AppointmentId),
                false);
        }
        catch
        {
            // Roll back if any operation fails
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<(AppointmentResponseDto? Appointment, bool NotOwner)> UpdateAsync(
        int id,
        UpdateAppointmentDto dto,
        string userId,
        bool isAdmin,
        bool isDoctor)
    {
        var appointment = await _context.Appointments
            .Include(a => a.Doctor)
            .Include(a => a.Patient)
            .FirstOrDefaultAsync(a => a.AppointmentId == id);

        if (appointment is null)
            return (null, false);

        if (!isAdmin)
        {
            var isOwner = isDoctor
                ? appointment.Doctor.UserId == userId
                : appointment.Patient.UserId == userId;

            if (!isOwner)
                return (null, true);
        }

        var doctorExists = await _context.Doctors
            .AnyAsync(d => d.DoctorId == dto.DoctorId);

        if (!doctorExists)
            return (null, false);

        appointment.DoctorId = dto.DoctorId;
        appointment.AppointmentDate = dto.AppointmentDate;
        appointment.AppointmentType = dto.AppointmentType;
        appointment.Status = dto.Status;
        appointment.Reason = dto.Reason;
        appointment.Notes = dto.Notes;

        await _context.SaveChangesAsync();

        return (await BuildResponseAsync(id), false);
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

    private async Task<AppointmentResponseDto?> BuildResponseAsync(int id)
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
}