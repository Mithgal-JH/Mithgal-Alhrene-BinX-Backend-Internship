using CardiacPatientMonitoringSystem.DTOs.Patients;
using FluentValidation;

namespace CardiacPatientMonitoringSystem.Validators.Patients;

public class CreatePatientValidator : AbstractValidator<CreatePatientDto>
{
    public CreatePatientValidator()
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
            .GreaterThanOrEqualTo(new DateOnly(1900, 1, 1))
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("Date of birth must be between 1850 and today");

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
}