using CardiacPatientMonitoringSystem.DTOs.Patients;
using CardiacPatientMonitoringSystem.Models;
using CardiacPatientMonitoringSystem.Repositories.Interfaces;
using CardiacPatientMonitoringSystem.Services.Interfaces;

namespace CardiacPatientMonitoringSystem.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _repository;

    public PatientService(IPatientRepository repository)
    {
        _repository = repository;
    }

    public async Task<PaginatedResponseDto<PatientResponseDto>> GetAllAsync(
        int page,
        int pageSize,
        string? search,
        string? gender,
        string? sort)
    {
        var patients = await _repository.GetAllAsync(
            page,
            pageSize,
            search,
            gender,
            sort);

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

    public async Task<PatientResponseDto> CreateAsync(
        CreatePatientDto dto)
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

        return BuildResponse(patient);
    }

    public async Task<(PatientResponseDto? Patient, bool NotOwner)> UpdateAsync(
        int id,
        UpdatePatientDto dto,
        string userId,
        bool isDoctor)
    {
        var patient = await _repository.GetByIdAsync(id);

        if (patient is null)
            return (null, false);

        var hasAccess = isDoctor
            ? await _repository.HasDoctorAccessAsync(id, userId)
            : patient.UserId == userId;

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

        return (BuildResponse(patient), false);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var patient = await _repository.GetByIdAsync(id);

        if (patient is null)
            return false;

        await _repository.DeleteAsync(patient);

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