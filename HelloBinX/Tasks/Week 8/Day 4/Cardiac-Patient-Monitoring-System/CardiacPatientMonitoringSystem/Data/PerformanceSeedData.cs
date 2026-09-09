using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CardiacPatientMonitoringSystem.Models;

namespace CardiacPatientMonitoringSystem.Data;

public static class PerformanceSeedData
{
    public static async Task Seed1000PatientsAsync(
        IServiceProvider services)
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

        // Don't duplicate the performance data
        if (await context.Patients.CountAsync() >= 1000)
            return;

        var random = new Random(42);

        var genders = new[] { "Male", "Female" };

        for (int i = 1; i <= 1000; i++)
        {
            var email = $"performance.patient{i}@test.com";

            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(
                    user,
                    "Test1234!"
                );

                if (!result.Succeeded)
                {
                    var errors = string.Join(
                        ", ",
                        result.Errors.Select(e => e.Description));

                    throw new Exception(
                        $"Failed to create user {email}: {errors}");
                }
            }

            var patientExists = await context.Patients
                .AnyAsync(p => p.UserId == user.Id);

            if (patientExists)
                continue;

            var gender = genders[random.Next(genders.Length)];

            var patient = new Patient
            {
                UserId = user.Id,

                MedicalRecordNumber =
                    $"PERF-{i:D5}",

                FirstName =
                    $"Patient{i}",

                LastName =
                    $"Test{i}",

                DateOfBirth =
                    DateOnly.FromDateTime(DateTime.UtcNow)
                        .AddYears(-random.Next(18, 80))
                        .AddDays(-random.Next(0, 365)),

                Gender = gender,

                Phone =
                    $"0599{random.Next(100000, 999999)}",

                Email = email,

                Address =
                    $"Test Address {i}",

                EmergencyContactName =
                    $"Emergency Contact {i}",

                EmergencyContactPhone =
                    $"0598{random.Next(100000, 999999)}",

                MedicalNotes =
                    "Synthetic performance test patient."
            };

            context.Patients.Add(patient);

            // Save in batches
            if (i % 100 == 0)
            {
                await context.SaveChangesAsync();

                Console.WriteLine(
                    $"Seeded {i} performance patients...");
            }
        }

        await context.SaveChangesAsync();

        Console.WriteLine(
            "1000 performance patients seeded successfully.");
    }
}