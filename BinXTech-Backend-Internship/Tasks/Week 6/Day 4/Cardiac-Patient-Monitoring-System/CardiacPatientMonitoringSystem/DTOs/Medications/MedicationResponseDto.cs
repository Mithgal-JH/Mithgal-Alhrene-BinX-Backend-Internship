namespace CardiacPatientMonitoringSystem.DTOs.Medications;

public class MedicationResponseDto
{
    public int MedicationId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string GenericName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Strength { get; set; } = string.Empty;
    public string DosageForm { get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;
}