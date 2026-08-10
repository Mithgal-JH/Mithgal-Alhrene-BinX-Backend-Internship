using CardiacPatientMonitoringSystem.Data;
using CardiacPatientMonitoringSystem.DTOs.Patients;
using CardiacPatientMonitoringSystem.Models;
using CardiacPatientMonitoringSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CardiacPatientMonitoringSystem.Services;

public class PatientService : IPatientService
{
    private readonly ApplicationDbContext _context;

    public PatientService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PatientResponseDto>> GetAllAsync()
    {
        return await _context.Patients
            .AsNoTracking()
            .Select(p => new PatientResponseDto
            {
                PatientId = p.PatientId,
                MedicalRecordNumber = p.MedicalRecordNumber,
                FirstName = p.FirstName,
                LastName = p.LastName,
                DateOfBirth = p.DateOfBirth,
                Gender = p.Gender,
                Phone = p.Phone,
                Email = p.Email,
                Address = p.Address,
                EmergencyContactName = p.EmergencyContactName,
                EmergencyContactPhone = p.EmergencyContactPhone,
                MedicalNotes = p.MedicalNotes
            })
            .ToListAsync();
    }

    public async Task<PatientResponseDto?> GetByIdAsync(int id)
    {
        return await _context.Patients
            .AsNoTracking()
            .Where(p => p.PatientId == id)
            .Select(p => new PatientResponseDto
            {
                PatientId = p.PatientId,
                MedicalRecordNumber = p.MedicalRecordNumber,
                FirstName = p.FirstName,
                LastName = p.LastName,
                DateOfBirth = p.DateOfBirth,
                Gender = p.Gender,
                Phone = p.Phone,
                Email = p.Email,
                Address = p.Address,
                EmergencyContactName = p.EmergencyContactName,
                EmergencyContactPhone = p.EmergencyContactPhone,
                MedicalNotes = p.MedicalNotes
            })
            .FirstOrDefaultAsync();
    }

    public async Task<PatientResponseDto> CreateAsync(CreatePatientDto dto)
    {
        var patient = new Patient
        {
            MedicalRecordNumber = dto.MedicalRecordNumber,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            DateOfBirth = dto.DateOfBirth,
            Gender = dto.Gender,
            Phone = dto.Phone,
            Email = dto.Email,
            Address = dto.Address,
            EmergencyContactName = dto.EmergencyContactName,
            EmergencyContactPhone = dto.EmergencyContactPhone,
            MedicalNotes = dto.MedicalNotes
        };

        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();

        return new PatientResponseDto
        {
            PatientId = patient.PatientId,
            MedicalRecordNumber = patient.MedicalRecordNumber,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            DateOfBirth = patient.DateOfBirth,
            Gender = patient.Gender,
            Phone = patient.Phone,
            Email = patient.Email,
            Address = patient.Address,
            EmergencyContactName = patient.EmergencyContactName,
            EmergencyContactPhone = patient.EmergencyContactPhone,
            MedicalNotes = patient.MedicalNotes
        };
    }

    public async Task<PatientResponseDto?> UpdateAsync(
        int id,
        UpdatePatientDto dto)
    {
        var patient = await _context.Patients
            .FirstOrDefaultAsync(p => p.PatientId == id);

        if (patient is null)
            return null;

        patient.MedicalRecordNumber = dto.MedicalRecordNumber;
        patient.FirstName = dto.FirstName;
        patient.LastName = dto.LastName;
        patient.DateOfBirth = dto.DateOfBirth;
        patient.Gender = dto.Gender;
        patient.Phone = dto.Phone;
        patient.Email = dto.Email;
        patient.Address = dto.Address;
        patient.EmergencyContactName = dto.EmergencyContactName;
        patient.EmergencyContactPhone = dto.EmergencyContactPhone;
        patient.MedicalNotes = dto.MedicalNotes;

        await _context.SaveChangesAsync();

        return await GetByIdAsync(id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var patient = await _context.Patients
            .FirstOrDefaultAsync(p => p.PatientId == id);

        if (patient is null)
            return false;

        _context.Patients.Remove(patient);
        await _context.SaveChangesAsync();

        return true;
    }
}