using CardiacPatientMonitoringSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CardiacPatientMonitoringSystem.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<VitalSign> VitalSigns => Set<VitalSign>();
    public DbSet<Medication> Medications => Set<Medication>();
    public DbSet<PatientMedication> PatientMedications => Set<PatientMedication>();
    public DbSet<Appointment> Appointments => Set<Appointment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(p => p.PatientId);

            entity.Property(p => p.MedicalRecordNumber)
                .HasMaxLength(50)
                .IsRequired();

            entity.HasIndex(p => p.MedicalRecordNumber)
                .IsUnique();

            entity.Property(p => p.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(p => p.LastName)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(p => p.Gender)
                .HasMaxLength(20)
                .IsRequired();

            entity.Property(p => p.Phone)
                .HasMaxLength(20)
                .IsRequired();

            entity.Property(p => p.Email)
                .HasMaxLength(255)
                .IsRequired();

            entity.Property(p => p.Address)
                .HasMaxLength(300)
                .IsRequired();

            entity.Property(p => p.EmergencyContactName)
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(p => p.EmergencyContactPhone)
                .HasMaxLength(20)
                .IsRequired();

            entity.Property(p => p.MedicalNotes)
                .HasMaxLength(1000);
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(d => d.DoctorId);

            entity.Property(d => d.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(d => d.LastName)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(d => d.Email)
                .HasMaxLength(255)
                .IsRequired();

            entity.Property(d => d.Phone)
                .HasMaxLength(20)
                .IsRequired();

            entity.Property(d => d.Specialization)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(d => d.LicenseNumber)
                .HasMaxLength(50)
                .IsRequired();

            entity.HasIndex(d => d.LicenseNumber)
                .IsUnique();
        });

        modelBuilder.Entity<VitalSign>(entity =>
        {
            entity.HasKey(v => v.VitalSignId);

            entity.Property(v => v.Temperature)
                .HasPrecision(5, 2);

            entity.Property(v => v.OxygenSaturation)
                .HasPrecision(5, 2);

            entity.Property(v => v.Weight)
                .HasPrecision(6, 2);

            entity.Property(v => v.Notes)
                .HasMaxLength(1000);

            entity.HasOne(v => v.Patient)
                .WithMany(p => p.VitalSigns)
                .HasForeignKey(v => v.PatientId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Medication>(entity =>
        {
            entity.HasKey(m => m.MedicationId);

            entity.Property(m => m.Name)
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(m => m.GenericName)
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(m => m.Description)
                .HasMaxLength(1000);

            entity.Property(m => m.Strength)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(m => m.DosageForm)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(m => m.Manufacturer)
                .HasMaxLength(200)
                .IsRequired();
        });

        modelBuilder.Entity<PatientMedication>(entity =>
        {
            entity.HasKey(pm => pm.PatientMedicationId);

            entity.Property(pm => pm.Dosage)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(pm => pm.Frequency)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(pm => pm.Route)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(pm => pm.Status)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(pm => pm.Notes)
                .HasMaxLength(1000);

            entity.HasOne(pm => pm.Patient)
                .WithMany(p => p.PatientMedications)
                .HasForeignKey(pm => pm.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(pm => pm.Medication)
                .WithMany(m => m.PatientMedications)
                .HasForeignKey(pm => pm.MedicationId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(a => a.AppointmentId);

            entity.Property(a => a.AppointmentType)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(a => a.Status)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(a => a.Reason)
                .HasMaxLength(500)
                .IsRequired();

            entity.Property(a => a.Notes)
                .HasMaxLength(1000);

            entity.HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}