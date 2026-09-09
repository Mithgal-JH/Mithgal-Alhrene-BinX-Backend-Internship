using CardiacPatientMonitoringSystem.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CardiacPatientMonitoringSystem.Authorization;

public class AdultOnlyHandler : AuthorizationHandler<AdultOnlyRequirement>
{
    private readonly ApplicationDbContext _context;

    public AdultOnlyHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        AdultOnlyRequirement requirement)
    {
        // Admins and Doctors are not restricted by the age policy
        if (context.User.IsInRole("Admin") ||
            context.User.IsInRole("Doctor"))
        {
            context.Succeed(requirement);
            return;
        }

        var userId = context.User.FindFirstValue(
            ClaimTypes.NameIdentifier);

        if (userId is null)
            return;

        var patient = await _context.Patients
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.UserId == userId);

        if (patient is null)
            return;

        var today = DateOnly.FromDateTime(DateTime.Today);

        var age = today.Year - patient.DateOfBirth.Year;

        if (patient.DateOfBirth > today.AddYears(-age))
        {
            age--;
        }

        if (age >= 18)
        {
            context.Succeed(requirement);
        }
    }
}