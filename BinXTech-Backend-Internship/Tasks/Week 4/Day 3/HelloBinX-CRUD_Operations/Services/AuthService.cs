using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public AuthService(
    UserManager<IdentityUser> userManager,
    SignInManager<IdentityUser> signInManager,
    RoleManager<IdentityRole> roleManager,
    IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    public async Task<IdentityResult> RegisterAsync(string email, string password)
    {
        var user = new IdentityUser
        {
            UserName = email,
            Email = email
        };

        return await _userManager.CreateAsync(user, password);
    }

    public async Task<string?> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
        {
            return null;
        }

        var result = await _signInManager.CheckPasswordSignInAsync(
            user,
            password,
            false);

        if (!result.Succeeded)
        {
            return null;
        }

        // Get user roles
        var roles = await _userManager.GetRolesAsync(user);

        // Add user information to the token
        var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
        new Claim(ClaimTypes.Email, user.Email!)
    };

        // Give Admin permission
        if (roles.Contains("Admin"))
        {
            claims.Add(new Claim("Permission", "ManageBooks"));
        }

        // Add roles to the token
        claims.AddRange(
            roles.Select(role => new Claim(ClaimTypes.Role, role))
        );

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        // Create the JWT token
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(15),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}