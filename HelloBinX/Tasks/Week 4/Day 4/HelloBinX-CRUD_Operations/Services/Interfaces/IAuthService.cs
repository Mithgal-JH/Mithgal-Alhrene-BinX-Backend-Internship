using Microsoft.AspNetCore.Identity;

public interface IAuthService
{
    Task<IdentityResult> RegisterAsync(string email, string password);
    Task<string?> LoginAsync(string email, string password);
}