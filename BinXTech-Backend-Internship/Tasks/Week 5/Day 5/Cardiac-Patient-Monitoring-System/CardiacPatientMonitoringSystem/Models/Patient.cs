namespace CardiacPatientMonitoringSystem.Models;

public class Patient
{
    public int PatientId { get; set; }

    public string? UserId { get; set; }

    public string MedicalRecordNumber { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public DateOnly DateOfBirth { get; set; }

    public string Gender { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string EmergencyContactName { get; set; } = string.Empty;

    public string EmergencyContactPhone { get; set; } = string.Empty;

    public string? MedicalNotes { get; set; }

    public ICollection<VitalSign> VitalSigns { get; set; } = new List<VitalSign>();

    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public ICollection<PatientMedication> PatientMedications { get; set; } = new List<PatientMedication>();
}