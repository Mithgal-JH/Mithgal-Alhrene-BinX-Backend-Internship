using System.Security.Claims;
using CardiacPatientMonitoringSystem.DTOs.PatientMedications;
using CardiacPatientMonitoringSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CardiacPatientMonitoringSystem.Controllers;

[Authorize]
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
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
            return Unauthorized();

        var result = await _patientMedicationService.GetAllAsync(
            userId,
            User.IsInRole("Admin"),
            User.IsInRole("Doctor"));

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PatientMedicationResponseDto>> GetById(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
            return Unauthorized();

        var result = await _patientMedicationService.GetByIdAsync(
            id,
            userId,
            User.IsInRole("Admin"),
            User.IsInRole("Doctor"));

        if (result.NotOwner)
            return Forbid();

        if (result.PatientMedication is null)
            return NotFound();

        return Ok(result.PatientMedication);
    }

    [Authorize(Roles = "Admin,Doctor,Patient")]
    [HttpPost]
    public async Task<ActionResult<PatientMedicationResponseDto>> Create(
        CreatePatientMedicationDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
            return Unauthorized();

        var result = await _patientMedicationService.CreateAsync(
            dto,
            userId,
            User.IsInRole("Admin"),
            User.IsInRole("Doctor"));

        if (result.NotOwner)
            return Forbid();

        if (result.PatientMedication is null)
        {
            return BadRequest(new
            {
                message = "Patient or medication does not exist."
            });
        }

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.PatientMedication.PatientMedicationId },
            result.PatientMedication);
    }

    [Authorize(Roles = "Admin,Doctor")]
    [HttpPut("{id:int}")]
    public async Task<ActionResult<PatientMedicationResponseDto>> Update(
        int id,
        UpdatePatientMedicationDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
            return Unauthorized();

        var result = await _patientMedicationService.UpdateAsync(
            id,
            dto,
            userId,
            User.IsInRole("Admin"),
            User.IsInRole("Doctor"));

        if (result.NotOwner)
            return Forbid();

        if (result.PatientMedication is null)
            return NotFound();

        return Ok(result.PatientMedication);
    }

    [Authorize(Roles = "Admin")]
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