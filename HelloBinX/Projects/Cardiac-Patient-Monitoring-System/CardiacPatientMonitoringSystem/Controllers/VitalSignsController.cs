using CardiacPatientMonitoringSystem.DTOs.VitalSigns;
using CardiacPatientMonitoringSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CardiacPatientMonitoringSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VitalSignsController : ControllerBase
{
    private readonly IVitalSignService _vitalSignService;

    public VitalSignsController(IVitalSignService vitalSignService)
    {
        _vitalSignService = vitalSignService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VitalSignResponseDto>>> GetAll()
    {
        var vitalSigns = await _vitalSignService.GetAllAsync();

        return Ok(vitalSigns);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<VitalSignResponseDto>> GetById(int id)
    {
        var vitalSign = await _vitalSignService.GetByIdAsync(id);

        if (vitalSign is null)
            return NotFound();

        return Ok(vitalSign);
    }

    [HttpPost]
    public async Task<ActionResult<VitalSignResponseDto>> Create(
        CreateVitalSignDto dto)
    {
        var vitalSign = await _vitalSignService.CreateAsync(dto);

        if (vitalSign is null)
            return BadRequest(new
            {
                message = "Patient does not exist."
            });

        return CreatedAtAction(
            nameof(GetById),
            new { id = vitalSign.VitalSignId },
            vitalSign);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _vitalSignService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}