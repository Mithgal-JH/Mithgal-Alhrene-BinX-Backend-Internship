using CardiacPatientMonitoringSystem.DTOs.Patients;
using CardiacPatientMonitoringSystem.Models;

namespace CardiacPatientMonitoringSystem.Repositories.Interfaces;

public interface IPatientRepository
{
    Task<PaginatedResponseDto<PatientResponseDto>> GetAllAsync(
        int page,
        int pageSize,
        string? search,
        string? gender,
        string? sort);

    Task<PaginatedResponseDto<PatientResponseDto>> GetMyPatientsAsync(
        string userId,
        int page,
        int pageSize,
        string? search,
        string? gender,
        string? sort);

    Task<Patient?> GetByIdAsync(int id);

    Task<Patient?> GetMyPatientAsync(string userId);

    Task<bool> HasDoctorAccessAsync(
        int patientId,
        string userId);

    Task AddAsync(Patient patient);

    Task UpdateAsync(Patient patient);

    Task DeleteAsync(Patient patient);
}