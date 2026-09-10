namespace CardiacPatientMonitoringSystem.DTOs.PatientMedications;

public class UpdatePatientMedicationDto
{
    public string Dosage { get; set; } = string.Empty;
    public string Frequency { get; set; } = string.Empty;
    public string Route { get; set; } = string.Empty;
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Notes { get; set; }
}