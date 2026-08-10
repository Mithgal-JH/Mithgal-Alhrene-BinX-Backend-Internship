using CardiacPatientMonitoringSystem.Services;
using CardiacPatientMonitoringSystem.Services.Interfaces;

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

        return services;
    }
}