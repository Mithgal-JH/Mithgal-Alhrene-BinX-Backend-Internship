using CardiacPatientMonitoringSystem.DTOs.Auth;
using FluentValidation;

namespace CardiacPatientMonitoringSystem.Validators.Auth;

public class RegisterDoctorValidator : AbstractValidator<RegisterDoctorDto>
{
    public RegisterDoctorValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8)
            .Matches("[A-Z]")
            .WithMessage("Password must contain at least one uppercase letter")
            .Matches("[a-z]")
            .WithMessage("Password must contain at least one lowercase letter")
            .Matches("[0-9]")
            .WithMessage("Password must contain at least one number")
            .Matches("[^a-zA-Z0-9]")
            .WithMessage("Password must contain at least one special character");

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(100);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(100);

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