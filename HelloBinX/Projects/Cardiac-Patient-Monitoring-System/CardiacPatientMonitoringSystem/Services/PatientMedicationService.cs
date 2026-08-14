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

    public async Task<IEnumerable<PatientMedicationResponseDto>> GetAllAsync(
        string userId,
        bool isAdmin,
        bool isDoctor)
    {
        var query = _context.PatientMedications
            .AsNoTracking()
            .AsQueryable();

        if (!isAdmin)
        {
            if (isDoctor)
            {
                query = query.Where(pm =>
                    pm.Patient.Appointments
                        .Any(a => a.Doctor.UserId == userId));
            }
            else
            {
                query = query.Where(pm =>
                    pm.Patient.UserId == userId);
            }
        }

        return await query
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

    public async Task<(PatientMedicationResponseDto? PatientMedication, bool NotOwner)>
        GetByIdAsync(
            int id,
            string userId,
            bool isAdmin,
            bool isDoctor)
    {
        var patientMedication = await _context.PatientMedications
            .Include(pm => pm.Patient)
            .FirstOrDefaultAsync(pm =>
                pm.PatientMedicationId == id);

        if (patientMedication is null)
            return (null, false);

        if (!isAdmin)
        {
            var hasAccess = isDoctor
                ? await HasDoctorAccessAsync(
                    patientMedication.PatientId,
                    userId)
                : patientMedication.Patient.UserId == userId;

            if (!hasAccess)
                return (null, true);
        }

        return (await BuildResponseAsync(id), false);
    }

    public async Task<(PatientMedicationResponseDto? PatientMedication, bool NotOwner)>
        CreateAsync(
            CreatePatientMedicationDto dto,
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

        var medicationExists = await _context.Medications
            .AnyAsync(m => m.MedicationId == dto.MedicationId);

        if (!medicationExists)
            return (null, false);

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

        return (
            await BuildResponseAsync(patientMedication.PatientMedicationId),
            false);
    }

    public async Task<(PatientMedicationResponseDto? PatientMedication, bool NotOwner)>
        UpdateAsync(
            int id,
            UpdatePatientMedicationDto dto,
            string userId,
            bool isAdmin,
            bool isDoctor)
    {
        var patientMedication = await _context.PatientMedications
            .Include(pm => pm.Patient)
            .FirstOrDefaultAsync(pm =>
                pm.PatientMedicationId == id);

        if (patientMedication is null)
            return (null, false);

        if (!isAdmin)
        {
            var hasAccess = isDoctor
                ? await HasDoctorAccessAsync(
                    patientMedication.PatientId,
                    userId)
                : false;

            // Patients are not allowed to update medication records
            if (!hasAccess)
                return (null, true);
        }

        patientMedication.Dosage = dto.Dosage;
        patientMedication.Frequency = dto.Frequency;
        patientMedication.Route = dto.Route;
        patientMedication.StartDate = dto.StartDate;
        patientMedication.EndDate = dto.EndDate;
        patientMedication.Status = dto.Status;
        patientMedication.Notes = dto.Notes;

        await _context.SaveChangesAsync();

        return (
            await BuildResponseAsync(id),
            false);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var patientMedication = await _context.PatientMedications
            .FirstOrDefaultAsync(pm =>
                pm.PatientMedicationId == id);

        if (patientMedication is null)
            return false;

        _context.PatientMedications.Remove(patientMedication);
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

    private async Task<PatientMedicationResponseDto?> BuildResponseAsync(
        int id)
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
}