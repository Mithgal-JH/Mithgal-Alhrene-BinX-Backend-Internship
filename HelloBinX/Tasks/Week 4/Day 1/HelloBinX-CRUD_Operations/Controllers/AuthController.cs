using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[Controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;

    public AuthController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest registerRequest)
    {
        var user = new IdentityUser
        {
            UserName = registerRequest.Email,
            Email = registerRequest.Email
        };

        var result = await _userManager.CreateAsync(user, registerRequest.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        return Ok(new
        {
            message = "User registered successfully"
        });
    }


}