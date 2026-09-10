using CardiacPatientMonitoringSystem.Data;
using CardiacPatientMonitoringSystem.Models;
using CardiacPatientMonitoringSystem.Services;
using Microsoft.EntityFrameworkCore;

namespace CardiacPatientMonitoringSystem.Tests;

public class AppointmentServiceTests
{
    private static ApplicationDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task GetByIdAsync_WhenDoctorOwnsAppointment_ReturnsAppointment()
    {
        await using var context = CreateContext();

        var doctor = new Doctor
        {
            DoctorId = 1,
            UserId = "doctor-user-1",
            FirstName = "John",
            LastName = "Smith",
            Email = "doctor@test.com",
            Phone = "0590000000",
            Specialization = "Cardiology",
            LicenseNumber = "LIC-001"
        };

        var patient = new Patient
        {
            PatientId = 1,
            UserId = "patient-user-1",
            MedicalRecordNumber = "MRN-001",
            FirstName = "Jane",
            LastName = "Doe",
            DateOfBirth = new DateOnly(1990, 1, 1),
            Gender = "Female",
            Phone = "0591111111",
            Email = "patient@test.com",
            Address = "Test Address",
            EmergencyContactName = "Emergency Contact",
            EmergencyContactPhone = "0592222222"
        };

        var appointment = new Appointment
        {
            AppointmentId = 1,
            PatientId = patient.PatientId,
            DoctorId = doctor.DoctorId,
            AppointmentDate = new DateTime(2026, 8, 20, 10, 0, 0),
            AppointmentType = "Follow-up",
            Status = "Scheduled",
            Reason = "Cardiac follow-up",
            Notes = "Test appointment",
            Patient = patient,
            Doctor = doctor
        };

        context.Doctors.Add(doctor);
        context.Patients.Add(patient);
        context.Appointments.Add(appointment);
        await context.SaveChangesAsync();

        var service = new AppointmentService(context);

        var result = await service.GetByIdAsync(
            appointment.AppointmentId,
            "doctor-user-1",
            isAdmin: false,
            isDoctor: true);

        Assert.NotNull(result.Appointment);
        Assert.False(result.NotOwner);
        Assert.Equal(appointment.AppointmentId, result.Appointment!.AppointmentId);
    }

    [Fact]
    public async Task GetByIdAsync_WhenAppointmentDoesNotExist_ReturnsNull()
    {
        await using var context = CreateContext();

        var service = new AppointmentService(context);

        var result = await service.GetByIdAsync(
            999,
            "doctor-user-1",
            isAdmin: false,
            isDoctor: true);

        Assert.Null(result.Appointment);
        Assert.False(result.NotOwner);
    }

    [Fact]
    public async Task GetByIdAsync_WhenUserIsNotOwner_ReturnsNotOwner()
    {
        await using var context = CreateContext();

        var doctor = new Doctor
        {
            DoctorId = 1,
            UserId = "doctor-user-1",
            FirstName = "John",
            LastName = "Smith",
            Email = "doctor@test.com",
            Phone = "0590000000",
            Specialization = "Cardiology",
            LicenseNumber = "LIC-001"
        };

        var patient = new Patient
        {
            PatientId = 1,
            UserId = "patient-user-1",
            MedicalRecordNumber = "MRN-001",
            FirstName = "Jane",
            LastName = "Doe",
            DateOfBirth = new DateOnly(1990, 1, 1),
            Gender = "Female",
            Phone = "0591111111",
            Email = "patient@test.com",
            Address = "Test Address",
            EmergencyContactName = "Emergency Contact",
            EmergencyContactPhone = "0592222222"
        };

        var appointment = new Appointment
        {
            AppointmentId = 1,
            PatientId = patient.PatientId,
            DoctorId = doctor.DoctorId,
            AppointmentDate = new DateTime(2026, 8, 20, 10, 0, 0),
            AppointmentType = "Follow-up",
            Status = "Scheduled",
            Reason = "Cardiac follow-up",
            Patient = patient,
            Doctor = doctor
        };

        context.Doctors.Add(doctor);
        context.Patients.Add(patient);
        context.Appointments.Add(appointment);
        await context.SaveChangesAsync();

        var service = new AppointmentService(context);

        var result = await service.GetByIdAsync(
            appointment.AppointmentId,
            "another-user",
            isAdmin: false,
            isDoctor: true);

        Assert.Null(result.Appointment);
        Assert.True(result.NotOwner);
    }
}