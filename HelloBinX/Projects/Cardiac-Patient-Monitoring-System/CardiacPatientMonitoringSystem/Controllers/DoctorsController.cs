using CardiacPatientMonitoringSystem.DTOs.Doctors;
using CardiacPatientMonitoringSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CardiacPatientMonitoringSystem.Controllers;

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

    [HttpPut("{id:int}")]
    public async Task<ActionResult<DoctorResponseDto>> Update(
        int id,
        UpdateDoctorDto dto)
    {
        var result = await _doctorService.UpdateAsync(id, dto);

        if (result.LicenseExists)
            return Conflict(new
            {
                message = "A doctor with this license number already exists."
            });

        if (result.Doctor is null)
            return NotFound();

        return Ok(result.Doctor);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _doctorService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}