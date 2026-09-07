using CardiacPatientMonitoringSystem.DTOs.Medications;
using FluentValidation;

namespace CardiacPatientMonitoringSystem.Validation.Medications;

public class UpdateMedicationValidator : AbstractValidator<UpdateMedicationDto>
{
    public UpdateMedicationValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.GenericName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.Description));

        RuleFor(x => x.Strength)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.DosageForm)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Manufacturer)
            .NotEmpty()
            .MaximumLength(100);
    }
}