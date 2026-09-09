using CardiacPatientMonitoringSystem.Data;
using CardiacPatientMonitoringSystem.DTOs.Medications;
using CardiacPatientMonitoringSystem.Models;
using CardiacPatientMonitoringSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CardiacPatientMonitoringSystem.Services;

public class MedicationService : IMedicationService
{
    private readonly ApplicationDbContext _context;

    public MedicationService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MedicationResponseDto>> GetAllAsync()
    {
        return await _context.Medications
            .AsNoTracking()
            .Select(m => new MedicationResponseDto
            {
                MedicationId = m.MedicationId,
                Name = m.Name,
                GenericName = m.GenericName,
                Description = m.Description,
                Strength = m.Strength,
                DosageForm = m.DosageForm,
                Manufacturer = m.Manufacturer
            })
            .ToListAsync();
    }

    public async Task<MedicationResponseDto?> GetByIdAsync(int id)
    {
        return await _context.Medications
            .AsNoTracking()
            .Where(m => m.MedicationId == id)
            .Select(m => new MedicationResponseDto
            {
                MedicationId = m.MedicationId,
                Name = m.Name,
                GenericName = m.GenericName,
                Description = m.Description,
                Strength = m.Strength,
                DosageForm = m.DosageForm,
                Manufacturer = m.Manufacturer
            })
            .FirstOrDefaultAsync();
    }

    public async Task<MedicationResponseDto> CreateAsync(
        CreateMedicationDto dto)
    {
        var medication = new Medication
        {
            Name = dto.Name,
            GenericName = dto.GenericName,
            Description = dto.Description,
            Strength = dto.Strength,
            DosageForm = dto.DosageForm,
            Manufacturer = dto.Manufacturer
        };

        _context.Medications.Add(medication);
        await _context.SaveChangesAsync();

        return new MedicationResponseDto
        {
            MedicationId = medication.MedicationId,
            Name = medication.Name,
            GenericName = medication.GenericName,
            Description = medication.Description,
            Strength = medication.Strength,
            DosageForm = medication.DosageForm,
            Manufacturer = medication.Manufacturer
        };
    }

    public async Task<MedicationResponseDto?> UpdateAsync(
        int id,
        UpdateMedicationDto dto)
    {
        var medication = await _context.Medications
            .FirstOrDefaultAsync(m => m.MedicationId == id);

        if (medication is null)
            return null;

        medication.Name = dto.Name;
        medication.GenericName = dto.GenericName;
        medication.Description = dto.Description;
        medication.Strength = dto.Strength;
        medication.DosageForm = dto.DosageForm;
        medication.Manufacturer = dto.Manufacturer;

        await _context.SaveChangesAsync();

        return await GetByIdAsync(id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var medication = await _context.Medications
            .FirstOrDefaultAsync(m => m.MedicationId == id);

        if (medication is null)
            return false;

        _context.Medications.Remove(medication);
        await _context.SaveChangesAsync();

        return true;
    }
}