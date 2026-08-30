namespace CardiacPatientMonitoringSystem.Models;

public class Notification
{
    public int NotificationId { get; set; }

    public string UserId { get; set; } = string.Empty;

    public int AppointmentId { get; set; }

    public string Message { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public bool IsRead { get; set; }

    public Appointment Appointment { get; set; } = null!;
}