using CardiacPatientMonitoringSystem.DTOs.Auth;
using CardiacPatientMonitoringSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CardiacPatientMonitoringSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register/patient")]
    public async Task<IActionResult> RegisterPatient(
    RegisterPatientDto dto)
    {
        var result =
            await _authService.RegisterPatientAsync(dto);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok(new
        {
            message = "Patient registered successfully."
        });
    }

    [HttpPost("register/doctor")]
    public async Task<IActionResult> RegisterDoctor(
        RegisterDoctorDto dto)
    {
        var result =
            await _authService.RegisterDoctorAsync(dto);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok(new
        {
            message = "Doctor registered successfully."
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var token = await _authService.LoginAsync(dto);

        if (token is null)
        {
            return Unauthorized(new
            {
                message = "Invalid email or password."
            });
        }

        return Ok(new
        {
            token
        });
    }



    // // Temporary endpoint used to assign a role to an existing user during development/testing.
    // [HttpPost("assign-role")]
    // public async Task<IActionResult> AssignRole(AssignRoleDto dto)
    // {
    //     // Find the user by email.
    //     var user = await _userManager.FindByEmailAsync(dto.Email);

    //     if (user == null)
    //         return NotFound("User not found.");

    //     // Check that the requested role exists.
    //     if (!await _roleManager.RoleExistsAsync(dto.Role))
    //         return BadRequest("Role does not exist.");

    //     // Check whether the user already has the role.
    //     if (await _userManager.IsInRoleAsync(user, dto.Role))
    //         return BadRequest("User already has this role.");

    //     // Assign the role to the user.
    //     var result = await _userManager.AddToRoleAsync(user, dto.Role);

    //     if (!result.Succeeded)
    //         return BadRequest(result.Errors);

    //     return Ok(new
    //     {
    //         message = $"Role '{dto.Role}' assigned successfully to '{dto.Email}'."
    //     });
    // }

}