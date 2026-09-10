using CardiacPatientMonitoringSystem.Data;
using CardiacPatientMonitoringSystem.Models;
using CardiacPatientMonitoringSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CardiacPatientMonitoringSystem.Repositories;

public class PatientRepository : IPatientRepository
{
    private readonly ApplicationDbContext _context;

    public PatientRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Patient>> GetAllAsync()
    {
        return await _context.Patients
            .AsNoTracking()
            .ToListAsync();
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