using CardiacPatientMonitoringSystem.DTOs.Doctors;
using FluentValidation;

namespace CardiacPatientMonitoringSystem.Validators.Doctors;

public class CreateDoctorValidator : AbstractValidator<CreateDoctorDto>
{
    public CreateDoctorValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(100);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(100);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(255);

        RuleFor(x => x.Phone)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(x => x.Specialization)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.LicenseNumber)
            .NotEmpty()
            .MaximumLength(50);
    }
}