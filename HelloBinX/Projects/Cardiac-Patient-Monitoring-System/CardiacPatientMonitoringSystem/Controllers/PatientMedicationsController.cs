using CardiacPatientMonitoringSystem.DTOs.PatientMedications;
using CardiacPatientMonitoringSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CardiacPatientMonitoringSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientMedicationsController : ControllerBase
{
    private readonly IPatientMedicationService _patientMedicationService;

    public PatientMedicationsController(
        IPatientMedicationService patientMedicationService)
    {
        _patientMedicationService = patientMedicationService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PatientMedicationResponseDto>>> GetAll()
    {
        var patientMedications =
            await _patientMedicationService.GetAllAsync();

        return Ok(patientMedications);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PatientMedicationResponseDto>> GetById(int id)
    {
        var patientMedication =
            await _patientMedicationService.GetByIdAsync(id);

        if (patientMedication is null)
            return NotFound();

        return Ok(patientMedication);
    }

    [HttpPost]
    public async Task<ActionResult<PatientMedicationResponseDto>> Create(
        CreatePatientMedicationDto dto)
    {
        var patientMedication =
            await _patientMedicationService.CreateAsync(dto);

        if (patientMedication is null)
            return BadRequest(new
            {
                message = "Patient or medication does not exist."
            });

        return CreatedAtAction(
            nameof(GetById),
            new { id = patientMedication.PatientMedicationId },
            patientMedication);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<PatientMedicationResponseDto>> Update(
        int id,
        UpdatePatientMedicationDto dto)
    {
        var patientMedication =
            await _patientMedicationService.UpdateAsync(id, dto);

        if (patientMedication is null)
            return NotFound();

        return Ok(patientMedication);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted =
            await _patientMedicationService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}