namespace CardiacPatientMonitoringSystem.DTOs.VitalSigns;

public class UpdateVitalSignDto
{
    public int HeartRate { get; set; }

    public int SystolicBloodPressure { get; set; }

    public int DiastolicBloodPressure { get; set; }

    public int RespiratoryRate { get; set; }

    public decimal Temperature { get; set; }

    public decimal OxygenSaturation { get; set; }

    public decimal Weight { get; set; }

    public DateTime RecordedAt { get; set; }

    public string? Notes { get; set; }
}