using CardiacPatientMonitoringSystem.Data;
using CardiacPatientMonitoringSystem.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CardiacPatientMonitoringSystem.Tests;

public class CustomWebApplicationFactory
    : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureAppConfiguration((context, config) =>
        {
            config.AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["Jwt:Key"] =
                        "ThisIsATestJwtKeyForIntegrationTestsOnly123456789",

                ["Jwt:Issuer"] =
                        "CardiacPatientMonitoringSystem",

                ["Jwt:Audience"] =
                        "CardiacPatientMonitoringSystem",

                ["Jwt:ExpirationInMinutes"] =
                        "60"
            });
        });
        builder.ConfigureServices(services =>
        {
            using var scope =
                services.BuildServiceProvider().CreateScope();

            var serviceProvider = scope.ServiceProvider;

            var context =
                serviceProvider
                    .GetRequiredService<ApplicationDbContext>();

            context.Database.EnsureCreated();

            SeedTestData(serviceProvider, context);
        });
    }

    private static void SeedTestData(
        IServiceProvider services,
        ApplicationDbContext context)
    {
        var userManager =
            services.GetRequiredService<UserManager<IdentityUser>>();

        var roleManager =
            services.GetRequiredService<RoleManager<IdentityRole>>();

        const string email = "patient17@example.com";
        const string password = "Patient@123";

        if (!roleManager.RoleExistsAsync("Patient")
            .GetAwaiter()
            .GetResult())
        {
            roleManager
                .CreateAsync(new IdentityRole("Patient"))
                .GetAwaiter()
                .GetResult();
        }

        var user =
            userManager.FindByEmailAsync(email)
                .GetAwaiter()
                .GetResult();

        if (user is null)
        {
            user = new IdentityUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };

            var result =
                userManager
                    .CreateAsync(user, password)
                    .GetAwaiter()
                    .GetResult();

            if (!result.Succeeded)
            {
                throw new InvalidOperationException(
                    string.Join(
                        ", ",
                        result.Errors.Select(e => e.Description)));
            }

            userManager
                .AddToRoleAsync(user, "Patient")
                .GetAwaiter()
                .GetResult();
        }

        if (!context.Patients.Any(p => p.PatientId == 10))
        {
            context.Patients.Add(new Patient
            {
                PatientId = 10,
                UserId = user.Id,
                MedicalRecordNumber = "TEST-MRN-010",
                FirstName = "Test",
                LastName = "Patient",
                DateOfBirth = new DateOnly(2000, 1, 1),
                Gender = "Male",
                Phone = "0599999999",
                Email = email,
                Address = "Test Address",
                EmergencyContactName = "Emergency Contact",
                EmergencyContactPhone = "0598888888",
                MedicalNotes = "Integration test patient"
            });

            context.SaveChanges();
        }
    }
}