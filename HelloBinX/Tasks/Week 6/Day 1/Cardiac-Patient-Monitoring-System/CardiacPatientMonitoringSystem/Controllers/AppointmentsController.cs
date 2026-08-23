using System.Security.Claims;
using CardiacPatientMonitoringSystem.DTOs.Appointments;
using CardiacPatientMonitoringSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CardiacPatientMonitoringSystem.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentsController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppointmentResponseDto>>> GetAll()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
            return Unauthorized();

        var appointments = await _appointmentService.GetAllAsync(
            userId,
            User.IsInRole("Admin"),
            User.IsInRole("Doctor"));

        return Ok(appointments);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AppointmentResponseDto>> GetById(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
            return Unauthorized();

        var result = await _appointmentService.GetByIdAsync(
            id,
            userId,
            User.IsInRole("Admin"),
            User.IsInRole("Doctor"));

        if (result.NotOwner)
            return Forbid();

        if (result.Appointment is null)
            return NotFound();

        return Ok(result.Appointment);
    }

    [HttpPost]
    public async Task<ActionResult<AppointmentResponseDto>> Create(
        CreateAppointmentDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
            return Unauthorized();

        var result = await _appointmentService.CreateAsync(
            dto,
            userId,
            User.IsInRole("Admin"),
            User.IsInRole("Doctor"));

        if (result.NotOwner)
            return Forbid();

        if (result.Appointment is null)
        {
            return BadRequest(new
            {
                message = "Patient or doctor does not exist."
            });
        }

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Appointment.AppointmentId },
            result.Appointment);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<AppointmentResponseDto>> Update(
        int id,
        UpdateAppointmentDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
            return Unauthorized();

        var result = await _appointmentService.UpdateAsync(
            id,
            dto,
            userId,
            User.IsInRole("Admin"),
            User.IsInRole("Doctor"));

        if (result.NotOwner)
            return Forbid();

        if (result.Appointment is null)
            return NotFound();

        return Ok(result.Appointment);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _appointmentService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}