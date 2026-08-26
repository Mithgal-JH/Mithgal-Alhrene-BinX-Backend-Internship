using CardiacPatientMonitoringSystem.DTOs.VitalSigns;
using FluentValidation;

namespace CardiacPatientMonitoringSystem.Validation.VitalSigns;

public class CreateVitalSignValidator : AbstractValidator<CreateVitalSignDto>
{
    public CreateVitalSignValidator()
    {
        RuleFor(x => x.PatientId)
            .GreaterThan(0);

        RuleFor(x => x.HeartRate)
            .InclusiveBetween(30, 220);

        RuleFor(x => x.SystolicBloodPressure)
            .InclusiveBetween(50, 250);

        RuleFor(x => x.DiastolicBloodPressure)
            .InclusiveBetween(30, 150);

        RuleFor(x => x.RespiratoryRate)
            .InclusiveBetween(5, 60);

        RuleFor(x => x.Temperature)
            .InclusiveBetween(30, 45);

        RuleFor(x => x.OxygenSaturation)
            .InclusiveBetween(0, 100);

        RuleFor(x => x.Weight)
            .GreaterThan(0);

        RuleFor(x => x.RecordedAt)
            .NotEmpty();

        RuleFor(x => x.Notes)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.Notes));
    }
}