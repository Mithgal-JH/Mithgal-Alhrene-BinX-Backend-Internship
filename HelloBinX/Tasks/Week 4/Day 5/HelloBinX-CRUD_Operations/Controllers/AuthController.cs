using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

[ApiController]
[Route("api/[Controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(
    IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest registerRequest)
    {
        var result = await _authService.RegisterAsync(
            registerRequest.Email,
            registerRequest.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok(new
        {
            message = "User registered successfully"
        });
    }


    [HttpPost("login")]
    [EnableRateLimiting("Fixed")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        var token = await _authService.LoginAsync(
            loginRequest.Email,
            loginRequest.Password);

        if (token == null)
        {
            return Unauthorized(new
            {
                message = "Invalid email or password"
            });
        }

        return Ok(new
        {
            token
        });
    }


    //temporary endpoint
    ////[HttpPost("seed-users")]
    ////public async Task<IActionResult> SeedUsers()
    //// {
    ////     await IdentitySeeder.SeedAsync(_userManager, _roleManager);

    ////     return Ok(new
    // //    {
    ////         message = "Roles and test users seeded successfully."
    ////     });
    //// }
}