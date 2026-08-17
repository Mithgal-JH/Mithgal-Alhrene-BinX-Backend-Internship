using CardiacPatientMonitoringSystem.Data;
using CardiacPatientMonitoringSystem.DTOs.VitalSigns;
using CardiacPatientMonitoringSystem.Models;
using CardiacPatientMonitoringSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CardiacPatientMonitoringSystem.Services;

public class VitalSignService : IVitalSignService
{
    private readonly ApplicationDbContext _context;

    public VitalSignService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<VitalSignResponseDto>> GetAllAsync(
        string userId,
        bool isAdmin,
        bool isDoctor)
    {
        var query = _context.VitalSigns
            .AsNoTracking()
            .AsQueryable();

        if (!isAdmin)
        {
            if (isDoctor)
            {
                query = query.Where(v =>
                    v.Patient.Appointments
                        .Any(a => a.Doctor.UserId == userId));
            }
            else
            {
                query = query.Where(v =>
                    v.Patient.UserId == userId);
            }
        }

        return await query
            .Select(v => new VitalSignResponseDto
            {
                VitalSignId = v.VitalSignId,
                PatientId = v.PatientId,
                HeartRate = v.HeartRate,
                SystolicBloodPressure = v.SystolicBloodPressure,
                DiastolicBloodPressure = v.DiastolicBloodPressure,
                RespiratoryRate = v.RespiratoryRate,
                Temperature = v.Temperature,
                OxygenSaturation = v.OxygenSaturation,
                Weight = v.Weight,
                RecordedAt = v.RecordedAt,
                Notes = v.Notes
            })
            .ToListAsync();
    }

    public async Task<(VitalSignResponseDto? VitalSign, bool NotOwner)>
        GetByIdAsync(
            int id,
            string userId,
            bool isAdmin,
            bool isDoctor)
    {
        var vitalSign = await _context.VitalSigns
            .Include(v => v.Patient)
            .FirstOrDefaultAsync(v => v.VitalSignId == id);

        if (vitalSign is null)
            return (null, false);

        if (!isAdmin)
        {
            var hasAccess = isDoctor
                ? await HasDoctorAccessAsync(
                    vitalSign.PatientId,
                    userId)
                : vitalSign.Patient.UserId == userId;

            if (!hasAccess)
                return (null, true);
        }

        return (await BuildResponseAsync(id), false);
    }

    public async Task<(VitalSignResponseDto? VitalSign, bool NotOwner)>
        CreateAsync(
            CreateVitalSignDto dto,
            string userId,
            bool isAdmin,
            bool isDoctor)
    {
        var patient = await _context.Patients
            .FirstOrDefaultAsync(p => p.PatientId == dto.PatientId);

        if (patient is null)
            return (null, false);

        if (!isAdmin)
        {
            var hasAccess = isDoctor
                ? await HasDoctorAccessAsync(
                    dto.PatientId,
                    userId)
                : patient.UserId == userId;

            if (!hasAccess)
                return (null, true);
        }

        var vitalSign = new VitalSign
        {
            PatientId = dto.PatientId,
            HeartRate = dto.HeartRate,
            SystolicBloodPressure = dto.SystolicBloodPressure,
            DiastolicBloodPressure = dto.DiastolicBloodPressure,
            RespiratoryRate = dto.RespiratoryRate,
            Temperature = dto.Temperature,
            OxygenSaturation = dto.OxygenSaturation,
            Weight = dto.Weight,
            RecordedAt = dto.RecordedAt,
            Notes = dto.Notes
        };

        _context.VitalSigns.Add(vitalSign);
        await _context.SaveChangesAsync();

        return (
            await BuildResponseAsync(vitalSign.VitalSignId),
            false);
    }

    public async Task<(VitalSignResponseDto? VitalSign, bool NotOwner)>
        UpdateAsync(
            int id,
            UpdateVitalSignDto dto,
            string userId,
            bool isAdmin)
    {
        var vitalSign = await _context.VitalSigns
            .Include(v => v.Patient)
            .FirstOrDefaultAsync(v => v.VitalSignId == id);

        if (vitalSign is null)
            return (null, false);

        if (!isAdmin)
        {
            var hasAccess =await HasDoctorAccessAsync(
                    vitalSign.PatientId,
                    userId);

            if (!hasAccess)
                return (null, true);
        }

        vitalSign.HeartRate = dto.HeartRate;
        vitalSign.SystolicBloodPressure = dto.SystolicBloodPressure;
        vitalSign.DiastolicBloodPressure = dto.DiastolicBloodPressure;
        vitalSign.RespiratoryRate = dto.RespiratoryRate;
        vitalSign.Temperature = dto.Temperature;
        vitalSign.OxygenSaturation = dto.OxygenSaturation;
        vitalSign.Weight = dto.Weight;
        vitalSign.RecordedAt = dto.RecordedAt;
        vitalSign.Notes = dto.Notes;

        await _context.SaveChangesAsync();

        return (
            await BuildResponseAsync(id),
            false);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var vitalSign = await _context.VitalSigns
            .FirstOrDefaultAsync(v => v.VitalSignId == id);

        if (vitalSign is null)
            return false;

        _context.VitalSigns.Remove(vitalSign);
        await _context.SaveChangesAsync();

        return true;
    }

    private async Task<bool> HasDoctorAccessAsync(
        int patientId,
        string userId)
    {
        return await _context.Appointments
            .AnyAsync(a =>
                a.PatientId == patientId &&
                a.Doctor.UserId == userId);
    }

    private async Task<VitalSignResponseDto?> BuildResponseAsync(int id)
    {
        return await _context.VitalSigns
            .AsNoTracking()
            .Where(v => v.VitalSignId == id)
            .Select(v => new VitalSignResponseDto
            {
                VitalSignId = v.VitalSignId,
                PatientId = v.PatientId,
                HeartRate = v.HeartRate,
                SystolicBloodPressure = v.SystolicBloodPressure,
                DiastolicBloodPressure = v.DiastolicBloodPressure,
                RespiratoryRate = v.RespiratoryRate,
                Temperature = v.Temperature,
                OxygenSaturation = v.OxygenSaturation,
                Weight = v.Weight,
                RecordedAt = v.RecordedAt,
                Notes = v.Notes
            })
            .FirstOrDefaultAsync();
    }
}