namespace CardiacPatientMonitoringSystem.DTOs.Appointments;

public class AppointmentSummaryDto
{
    public int AppointmentId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string AppointmentType { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string PatientName { get; set; } = string.Empty;
    public string DoctorName { get; set; } = string.Empty;
}