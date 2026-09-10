using CardiacPatientMonitoringSystem.Controllers;
using CardiacPatientMonitoringSystem.Data;
using CardiacPatientMonitoringSystem.DTOs.Patients;
using CardiacPatientMonitoringSystem.Models;
using CardiacPatientMonitoringSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace CardiacPatientMonitoringSystem.Repositories;

public class PatientRepository : IPatientRepository
{
    private readonly ApplicationDbContext _context;

    public PatientRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedResponseDto<PatientResponseDto>> GetAllAsync(int page,
                                                                    int pageSize,
                                                                    string? search,
                                                                    string? gender,
                                                                    string? sort)
    {
        IQueryable<Patient> query = _context.Patients
                                            .AsNoTracking();


        // Search (filter)
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(p => p.FirstName.Contains(search) || p.LastName.Contains(search));
        }

        // Gender
        if (!string.IsNullOrWhiteSpace(gender))
        {
            query = query.Where(p => p.Gender == gender);
        }

        //TotalCount after filters
        int totalCount = await query.CountAsync();

        if (sort == "dob_desc")
            query = query.OrderByDescending(p => p.DateOfBirth);
        else
            query = query.OrderBy(p => p.DateOfBirth);

        var patients = await query
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize)
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

        return new PaginatedResponseDto<PatientResponseDto>
        {
            Items = patients,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };


    }

    public async Task<Patient?> GetByIdAsync(int id)
    {
        return await _context.Patients
            .FirstOrDefaultAsync(p => p.PatientId == id);
    }

    public async Task AddAsync(Patient patient)
    {
        await _context.Patients.AddAsync(patient);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Patient patient)
    {
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Patient patient)
    {
        _context.Patients.Remove(patient);
        await _context.SaveChangesAsync();
    }
}