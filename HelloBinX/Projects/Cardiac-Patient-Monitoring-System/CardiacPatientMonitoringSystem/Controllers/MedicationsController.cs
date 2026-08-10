using CardiacPatientMonitoringSystem.DTOs.Medications;
using CardiacPatientMonitoringSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CardiacPatientMonitoringSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MedicationsController : ControllerBase
{
    private readonly IMedicationService _medicationService;

    public MedicationsController(IMedicationService medicationService)
    {
        _medicationService = medicationService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MedicationResponseDto>>> GetAll()
    {
        var medications = await _medicationService.GetAllAsync();

        return Ok(medications);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<MedicationResponseDto>> GetById(int id)
    {
        var medication = await _medicationService.GetByIdAsync(id);

        if (medication is null)
            return NotFound();

        return Ok(medication);
    }

    [HttpPost]
    public async Task<ActionResult<MedicationResponseDto>> Create(
        CreateMedicationDto dto)
    {
        var medication = await _medicationService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = medication.MedicationId },
            medication);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<MedicationResponseDto>> Update(
        int id,
        UpdateMedicationDto dto)
    {
        var medication = await _medicationService.UpdateAsync(id, dto);

        if (medication is null)
            return NotFound();

        return Ok(medication);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _medicationService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}