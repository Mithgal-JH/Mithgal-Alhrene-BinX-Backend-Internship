using System.Security.Claims;
using CardiacPatientMonitoringSystem.DTOs.VitalSigns;
using CardiacPatientMonitoringSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CardiacPatientMonitoringSystem.Controllers;

[Authorize]
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
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
            return Unauthorized();

        var vitalSigns = await _vitalSignService.GetAllAsync(
            userId,
            User.IsInRole("Admin"),
            User.IsInRole("Doctor"));

        return Ok(vitalSigns);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<VitalSignResponseDto>> GetById(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
            return Unauthorized();

        var result = await _vitalSignService.GetByIdAsync(
            id,
            userId,
            User.IsInRole("Admin"),
            User.IsInRole("Doctor"));

        if (result.NotOwner)
            return Forbid();

        if (result.VitalSign is null)
            return NotFound();

        return Ok(result.VitalSign);
    }

    [Authorize(Roles = "Admin,Doctor,Patient")]
    [HttpPost]
    public async Task<ActionResult<VitalSignResponseDto>> Create(
        CreateVitalSignDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
            return Unauthorized();

        var result = await _vitalSignService.CreateAsync(
            dto,
            userId,
            User.IsInRole("Admin"),
            User.IsInRole("Doctor"));

        if (result.NotOwner)
            return Forbid();

        if (result.VitalSign is null)
        {
            return BadRequest(new
            {
                message = "Patient does not exist."
            });
        }

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.VitalSign.VitalSignId },
            result.VitalSign);
    }

    [Authorize(Roles = "Admin,Doctor")]
    [HttpPut("{id:int}")]
    public async Task<ActionResult<VitalSignResponseDto>> Update(
        int id,
        UpdateVitalSignDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
            return Unauthorized();

        var result = await _vitalSignService.UpdateAsync(
            id,
            dto,
            userId,
            User.IsInRole("Admin"),
            User.IsInRole("Doctor"));

        if (result.NotOwner)
            return Forbid();

        if (result.VitalSign is null)
            return NotFound();

        return Ok(result.VitalSign);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _vitalSignService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}