using CardiacPatientMonitoringSystem.DTOs.Patients;
using FluentValidation;

namespace CardiacPatientMonitoringSystem.Validators.Patients;

public class UpdatePatientValidator : AbstractValidator<UpdatePatientDto>
{
    public UpdatePatientValidator()
    {
        RuleFor(x => x.MedicalRecordNumber)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(100);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(100);

        RuleFor(x => x.DateOfBirth)
            .Must(date => CalculateAge(date) >= 18)
            .WithMessage("Patient must be at least 18 years old");

        RuleFor(x => x.Gender)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(x => x.Phone)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(255);

        RuleFor(x => x.Address)
            .NotEmpty()
            .MaximumLength(300);

        RuleFor(x => x.EmergencyContactName)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.EmergencyContactPhone)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(x => x.MedicalNotes)
            .MaximumLength(1000);
    }

    private static int CalculateAge(DateOnly dateOfBirth)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var age = today.Year - dateOfBirth.Year;

        if (dateOfBirth > today.AddYears(-age))
            age--;

        return age;
    }
}