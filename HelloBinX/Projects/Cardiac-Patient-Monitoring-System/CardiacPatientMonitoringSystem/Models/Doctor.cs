namespace CardiacPatientMonitoringSystem.Models;

public class Doctor
{
    public int DoctorId { get; set; }

    public string? UserId { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string Specialization { get; set; } = string.Empty;

    public string LicenseNumber { get; set; } = string.Empty;

    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}