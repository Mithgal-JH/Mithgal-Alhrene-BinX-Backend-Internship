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

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var result = await _authService.RegisterAsync(dto);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok(new
        {
            message = "User registered successfully."
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
}