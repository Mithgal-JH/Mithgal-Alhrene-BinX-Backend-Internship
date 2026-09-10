using CardiacPatientMonitoringSystem.Services;
using CardiacPatientMonitoringSystem.Services.Interfaces;
using CardiacPatientMonitoringSystem.Authorization;
using Microsoft.AspNetCore.Authorization;
using CardiacPatientMonitoringSystem.Repositories.Interfaces;
using CardiacPatientMonitoringSystem.Repositories;

namespace CardiacPatientMonitoringSystem.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services.AddScoped<IPatientService, PatientService>();
        services.AddScoped<IDoctorService, DoctorService>();
        services.AddScoped<IMedicationService, MedicationService>();
        services.AddScoped<IPatientMedicationService, PatientMedicationService>();
        services.AddScoped<IAppointmentService, AppointmentService>();
        services.AddScoped<IVitalSignService, VitalSignService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAuthorizationHandler, AdultOnlyHandler>();

        services.AddScoped<IPatientRepository, PatientRepository>();
        return services;
    }
}