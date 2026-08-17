using System.Security.Claims;
using CardiacPatientMonitoringSystem.DTOs.Patients;
using CardiacPatientMonitoringSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CardiacPatientMonitoringSystem.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientsController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [Authorize(Roles = "Admin,Doctor")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PatientResponseDto>>> GetAll()
    {
        var userId = User.FindFirstValue(
            ClaimTypes.NameIdentifier);

        if (userId is null)
            return Unauthorized();

        var patients = await _patientService.GetAllAsync(
            userId,
            User.IsInRole("Admin"),
            User.IsInRole("Doctor"));

        return Ok(patients);
    }

    // Accessible by all authenticated roles
    [Authorize(Roles = "Admin,Doctor,Patient")]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<PatientResponseDto>> GetById(int id)
    {
        var userId = User.FindFirstValue(
            ClaimTypes.NameIdentifier);

        if (userId is null)
            return Unauthorized();

        var result = await _patientService.GetByIdAsync(
            id,
            userId,
            User.IsInRole("Admin"),
            User.IsInRole("Doctor"));

        if (result.NotOwner)
            return Forbid();

        if (result.Patient is null)
            return NotFound();

        return Ok(result.Patient);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<PatientResponseDto>> Create(
        CreatePatientDto dto)
    {
        var patient = await _patientService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = patient.PatientId },
            patient);
    }

    [Authorize(Roles = "Doctor,Patient")]
    [HttpPut("{id:int}")]
    public async Task<ActionResult<PatientResponseDto>> Update(
        int id,
        UpdatePatientDto dto)
    {
        var userId = User.FindFirstValue(
            ClaimTypes.NameIdentifier);

        if (userId is null)
            return Unauthorized();

        var result = await _patientService.UpdateAsync(
            id,
            dto,
            userId,
            User.IsInRole("Doctor"));

        if (result.NotOwner)
            return Forbid();

        if (result.Patient is null)
            return NotFound();

        return Ok(result.Patient);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _patientService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}