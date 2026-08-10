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

    public async Task<IEnumerable<VitalSignResponseDto>> GetAllAsync()
    {
        return await _context.VitalSigns
            .AsNoTracking()
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

    public async Task<VitalSignResponseDto?> GetByIdAsync(int id)
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

    public async Task<VitalSignResponseDto?> CreateAsync(
        CreateVitalSignDto dto)
    {
        var patientExists = await _context.Patients
            .AnyAsync(p => p.PatientId == dto.PatientId);

        if (!patientExists)
            return null;

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

        return await GetByIdAsync(vitalSign.VitalSignId);
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
}