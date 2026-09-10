using System.Security.Claims;
using CardiacPatientMonitoringSystem.DTOs.Doctors;
using CardiacPatientMonitoringSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CardiacPatientMonitoringSystem.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DoctorsController : ControllerBase
{
    private readonly IDoctorService _doctorService;

    public DoctorsController(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DoctorResponseDto>>> GetAll()
    {
        var doctors = await _doctorService.GetAllAsync();

        return Ok(doctors);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<DoctorResponseDto>> GetById(int id)
    {
        var doctor = await _doctorService.GetByIdAsync(id);

        if (doctor is null)
            return NotFound();

        return Ok(doctor);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<DoctorResponseDto>> Create(
        CreateDoctorDto dto)
    {
        var result = await _doctorService.CreateAsync(dto);

        if (result.LicenseExists)
            return Conflict(new
            {
                message = "A doctor with this license number already exists."
            });

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Doctor!.DoctorId },
            result.Doctor);
    }


    // Allows a doctor to update only their own doctor profile
    [Authorize(Roles = "Doctor")]
    [HttpPut("{id:int}")]
    public async Task<ActionResult<DoctorResponseDto>> Update(
        int id,
        UpdateDoctorDto dto)
    {
        // Get the authenticated user's ID from the JWT "sub" claim
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        // Make sure the JWT contains a user ID
        if (userId is null)
            return Unauthorized();

        // Update the doctor and verify ownership inside the service
        var result = await _doctorService.UpdateAsync(
            id,
            dto,
            userId);

        // The doctor exists, but belongs to another user
        if (result.NotOwner)
            return Forbid();

        // Another doctor already uses this license number
        if (result.LicenseExists)
            return Conflict(new
            {
                message = "A doctor with this license number already exists."
            });

        // The requested doctor does not exist
        if (result.Doctor is null)
            return NotFound();

        return Ok(result.Doctor);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _doctorService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}