using CardiacPatientMonitoringSystem.DTOs.Patients;
using CardiacPatientMonitoringSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CardiacPatientMonitoringSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientsController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PatientResponseDto>>> GetAll()
    {
        var patients = await _patientService.GetAllAsync();

        return Ok(patients);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PatientResponseDto>> GetById(int id)
    {
        var patient = await _patientService.GetByIdAsync(id);

        if (patient is null)
            return NotFound();

        return Ok(patient);
    }

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

    [HttpPut("{id:int}")]
    public async Task<ActionResult<PatientResponseDto>> Update(
        int id,
        UpdatePatientDto dto)
    {
        var patient = await _patientService.UpdateAsync(id, dto);

        if (patient is null)
            return NotFound();

        return Ok(patient);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _patientService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}