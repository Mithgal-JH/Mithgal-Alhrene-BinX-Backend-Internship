using CardiacPatientMonitoringSystem.DTOs.Appointments;
using CardiacPatientMonitoringSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CardiacPatientMonitoringSystem.Controllers;

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
        var appointments = await _appointmentService.GetAllAsync();

        return Ok(appointments);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AppointmentResponseDto>> GetById(int id)
    {
        var appointment = await _appointmentService.GetByIdAsync(id);

        if (appointment is null)
            return NotFound();

        return Ok(appointment);
    }

    [HttpPost]
    public async Task<ActionResult<AppointmentResponseDto>> Create(
        CreateAppointmentDto dto)
    {
        var appointment = await _appointmentService.CreateAsync(dto);

        if (appointment is null)
            return BadRequest(new
            {
                message = "Patient or doctor does not exist."
            });

        return CreatedAtAction(
            nameof(GetById),
            new { id = appointment.AppointmentId },
            appointment);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<AppointmentResponseDto>> Update(
        int id,
        UpdateAppointmentDto dto)
    {
        var appointment = await _appointmentService.UpdateAsync(id, dto);

        if (appointment is null)
            return NotFound();

        return Ok(appointment);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _appointmentService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}