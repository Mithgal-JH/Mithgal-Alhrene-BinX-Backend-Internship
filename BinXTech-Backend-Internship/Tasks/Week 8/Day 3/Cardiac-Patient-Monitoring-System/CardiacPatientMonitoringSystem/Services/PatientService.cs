using System.Text.Json;
using CardiacPatientMonitoringSystem.DTOs.Patients;
using CardiacPatientMonitoringSystem.Models;
using CardiacPatientMonitoringSystem.Repositories.Interfaces;
using CardiacPatientMonitoringSystem.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace CardiacPatientMonitoringSystem.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _repository;
    private readonly IDistributedCache _cache;
    private readonly ICacheVersionService _cacheVersionService;
    public PatientService(
    IPatientRepository repository,
    IDistributedCache cache,
    ICacheVersionService cacheVersionService)
    {
        _repository = repository;
        _cache = cache;
        _cacheVersionService = cacheVersionService;
    }

    public async Task<PaginatedResponseDto<PatientResponseDto>> GetAllAsync(
        int page,
        int pageSize,
        string? search,
        string? gender,
        string? sort)
    {
        var version = await _cacheVersionService.GetVersionAsync("patients");

        var normalizedSearch = search?.Trim().ToLowerInvariant();
        var normalizedGender = gender?.Trim().ToLowerInvariant();
        var normalizedSort = sort?.Trim().ToLowerInvariant();

        var cacheKey =
            $"patients:v{version}:{page}:{pageSize}:{normalizedSearch}:{normalizedGender}:{normalizedSort}";

        var cached = await _cache.GetStringAsync(cacheKey);

        if (cached is not null)
        {
            return JsonSerializer.Deserialize<PaginatedResponseDto<PatientResponseDto>>(cached)!;
        }

        var patients = await _repository.GetAllAsync(
            page,
            pageSize,
            search,
            gender,
            sort);

        await _cache.SetStringAsync(
            cacheKey,
            JsonSerializer.Serialize(patients),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            });

        return patients;
    }

    public async Task<PaginatedResponseDto<PatientResponseDto>> GetMyPatientsAsync(
        string userId,
        int page,
        int pageSize,
        string? search,
        string? gender,
        string? sort)
    {
        var patients = await _repository.GetMyPatientsAsync(
            userId,
            page,
            pageSize,
            search,
            gender,
            sort);

        return patients;
    }

    public async Task<(PatientResponseDto? Patient, bool NotOwner)> GetMyPatientAsync(
        string userId)
    {
        var patient = await _repository.GetMyPatientAsync(userId);

        if (patient is null)
            return (null, false);

        return (BuildResponse(patient), false);
    }

    public async Task<(PatientResponseDto? Patient, bool NotOwner)> GetByIdAsync(
        int id,
        string userId,
        bool isAdmin,
        bool isDoctor)
    {
        var patient = await _repository.GetByIdAsync(id);

        if (patient is null)
            return (null, false);

        if (!isAdmin)
        {
            var hasAccess = isDoctor
                ? await _repository.HasDoctorAccessAsync(id, userId)
                : patient.UserId == userId;

            if (!hasAccess)
                return (null, true);
        }

        return (BuildResponse(patient), false);
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

        await _repository.AddAsync(patient);

        // Invalidate patients cache
        await _cacheVersionService.InvalidateAsync("patients");

        return BuildResponse(patient);
    }

    public async Task<(PatientResponseDto? Patient, bool NotOwner)> UpdateAsync(
        int id,
        UpdatePatientDto dto,
        string userId,
        bool isAdmin,
        bool isDoctor)
    {
        var patient = await _repository.GetByIdAsync(id);

        if (patient is null)
            return (null, false);

        var hasAccess = isAdmin
            || (isDoctor
                ? await _repository.HasDoctorAccessAsync(id, userId)
                : patient.UserId == userId);

        if (!hasAccess)
            return (null, true);

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

        await _repository.UpdateAsync(patient);

        // Invalidate patients cache
        await _cacheVersionService.InvalidateAsync("patients");

        return (BuildResponse(patient), false);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var patient = await _repository.GetByIdAsync(id);

        if (patient is null)
            return false;

        await _repository.DeleteAsync(patient);

        // Invalidate patients cache
        await _cacheVersionService.InvalidateAsync("patients");

        return true;
    }

    private static PatientResponseDto BuildResponse(Patient patient)
    {
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
}