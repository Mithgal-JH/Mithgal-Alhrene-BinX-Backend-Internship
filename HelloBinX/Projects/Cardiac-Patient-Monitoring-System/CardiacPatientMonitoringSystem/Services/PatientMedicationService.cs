using CardiacPatientMonitoringSystem.Data;
using CardiacPatientMonitoringSystem.DTOs.PatientMedications;
using CardiacPatientMonitoringSystem.Models;
using CardiacPatientMonitoringSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CardiacPatientMonitoringSystem.Services;

public class PatientMedicationService : IPatientMedicationService
{
    private readonly ApplicationDbContext _context;

    public PatientMedicationService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PatientMedicationResponseDto>> GetAllAsync()
    {
        return await _context.PatientMedications
            .AsNoTracking()
            .Select(pm => new PatientMedicationResponseDto
            {
                PatientMedicationId = pm.PatientMedicationId,
                PatientId = pm.PatientId,
                MedicationId = pm.MedicationId,
                Dosage = pm.Dosage,
                Frequency = pm.Frequency,
                Route = pm.Route,
                StartDate = pm.StartDate,
                EndDate = pm.EndDate,
                Status = pm.Status,
                Notes = pm.Notes
            })
            .ToListAsync();
    }

    public async Task<PatientMedicationResponseDto?> GetByIdAsync(int id)
    {
        return await _context.PatientMedications
            .AsNoTracking()
            .Where(pm => pm.PatientMedicationId == id)
            .Select(pm => new PatientMedicationResponseDto
            {
                PatientMedicationId = pm.PatientMedicationId,
                PatientId = pm.PatientId,
                MedicationId = pm.MedicationId,
                Dosage = pm.Dosage,
                Frequency = pm.Frequency,
                Route = pm.Route,
                StartDate = pm.StartDate,
                EndDate = pm.EndDate,
                Status = pm.Status,
                Notes = pm.Notes
            })
            .FirstOrDefaultAsync();
    }

    public async Task<PatientMedicationResponseDto?> CreateAsync(
        CreatePatientMedicationDto dto)
    {
        var patientExists = await _context.Patients
            .AnyAsync(p => p.PatientId == dto.PatientId);

        if (!patientExists)
            return null;

        var medicationExists = await _context.Medications
            .AnyAsync(m => m.MedicationId == dto.MedicationId);

        if (!medicationExists)
            return null;

        var patientMedication = new PatientMedication
        {
            PatientId = dto.PatientId,
            MedicationId = dto.MedicationId,
            Dosage = dto.Dosage,
            Frequency = dto.Frequency,
            Route = dto.Route,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            Status = dto.Status,
            Notes = dto.Notes
        };

        _context.PatientMedications.Add(patientMedication);
        await _context.SaveChangesAsync();

        return await GetByIdAsync(patientMedication.PatientMedicationId);
    }

    public async Task<PatientMedicationResponseDto?> UpdateAsync(
        int id,
        UpdatePatientMedicationDto dto)
    {
        var patientMedication = await _context.PatientMedications
            .FirstOrDefaultAsync(pm => pm.PatientMedicationId == id);

        if (patientMedication is null)
            return null;

        patientMedication.Dosage = dto.Dosage;
        patientMedication.Frequency = dto.Frequency;
        patientMedication.Route = dto.Route;
        patientMedication.StartDate = dto.StartDate;
        patientMedication.EndDate = dto.EndDate;
        patientMedication.Status = dto.Status;
        patientMedication.Notes = dto.Notes;

        await _context.SaveChangesAsync();

        return await GetByIdAsync(id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var patientMedication = await _context.PatientMedications
            .FirstOrDefaultAsync(pm => pm.PatientMedicationId == id);

        if (patientMedication is null)
            return false;

        _context.PatientMedications.Remove(patientMedication);
        await _context.SaveChangesAsync();

        return true;
    }
}