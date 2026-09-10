using CardiacPatientMonitoringSystem.DTOs.PatientMedications;
using FluentValidation;

namespace CardiacPatientMonitoringSystem.Validation.PatientMedications;

public class UpdatePatientMedicationValidator
    : AbstractValidator<UpdatePatientMedicationDto>
{
    public UpdatePatientMedicationValidator()
    {
        RuleFor(x => x.Dosage)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Frequency)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Route)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.EndDate)
            .GreaterThanOrEqualTo(x => x.StartDate)
            .When(x => x.EndDate.HasValue);

        RuleFor(x => x.Status)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Notes)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.Notes));
    }
}