namespace CardiacPatientMonitoringSystem.Services.Interfaces;

public interface ICacheVersionService
{
    Task<string> GetVersionAsync(string resource);
    Task InvalidateAsync(string resource);
}