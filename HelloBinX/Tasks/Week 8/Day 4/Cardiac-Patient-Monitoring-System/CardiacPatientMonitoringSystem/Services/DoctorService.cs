using CardiacPatientMonitoringSystem.Data;
using CardiacPatientMonitoringSystem.DTOs.Doctors;
using CardiacPatientMonitoringSystem.Models;
using CardiacPatientMonitoringSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CardiacPatientMonitoringSystem.Services;

public class DoctorService : IDoctorService
{
    private readonly ApplicationDbContext _context;

    public DoctorService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DoctorResponseDto>> GetAllAsync()
    {
        return await _context.Doctors
            .AsNoTracking()
            .Select(d => new DoctorResponseDto
            {
                DoctorId = d.DoctorId,
                FirstName = d.FirstName,
                LastName = d.LastName,
                Email = d.Email,
                Phone = d.Phone,
                Specialization = d.Specialization,
                LicenseNumber = d.LicenseNumber
            })
            .ToListAsync();
    }

    public async Task<DoctorResponseDto?> GetByIdAsync(int id)
    {
        return await _context.Doctors
            .AsNoTracking()
            .Where(d => d.DoctorId == id)
            .Select(d => new DoctorResponseDto
            {
                DoctorId = d.DoctorId,
                FirstName = d.FirstName,
                LastName = d.LastName,
                Email = d.Email,
                Phone = d.Phone,
                Specialization = d.Specialization,
                LicenseNumber = d.LicenseNumber
            })
            .FirstOrDefaultAsync();
    }

    public async Task<(DoctorResponseDto? Doctor, bool LicenseExists)> CreateAsync(
        CreateDoctorDto dto)
    {
        var licenseExists = await _context.Doctors
            .AnyAsync(d => d.LicenseNumber == dto.LicenseNumber);

        if (licenseExists)
            return (null, true);

        var doctor = new Doctor
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Phone = dto.Phone,
            Specialization = dto.Specialization,
            LicenseNumber = dto.LicenseNumber
        };

        _context.Doctors.Add(doctor);
        await _context.SaveChangesAsync();

        return (
            new DoctorResponseDto
            {
                DoctorId = doctor.DoctorId,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Email = doctor.Email,
                Phone = doctor.Phone,
                Specialization = doctor.Specialization,
                LicenseNumber = doctor.LicenseNumber
            },
            false
        );
    }

    public async Task<(DoctorResponseDto? Doctor, bool LicenseExists)> UpdateAsync(
    int id,
    UpdateDoctorDto dto)
    {
        var doctor = await _context.Doctors
            .FirstOrDefaultAsync(d => d.DoctorId == id);

        if (doctor is null)
            return (null, false);

        var licenseExists = await _context.Doctors
            .AnyAsync(d =>
                d.LicenseNumber == dto.LicenseNumber &&
                d.DoctorId != id);

        if (licenseExists)
            return (null, true);

        doctor.FirstName = dto.FirstName;
        doctor.LastName = dto.LastName;
        doctor.Email = dto.Email;
        doctor.Phone = dto.Phone;
        doctor.Specialization = dto.Specialization;
        doctor.LicenseNumber = dto.LicenseNumber;

        await _context.SaveChangesAsync();

        return (await GetByIdAsync(id), false);
    }
    public async Task<bool> DeleteAsync(int id)
    {
        var doctor = await _context.Doctors
            .FirstOrDefaultAsync(d => d.DoctorId == id);

        if (doctor is null)
            return false;

        _context.Doctors.Remove(doctor);
        await _context.SaveChangesAsync();

        return true;
    }
}