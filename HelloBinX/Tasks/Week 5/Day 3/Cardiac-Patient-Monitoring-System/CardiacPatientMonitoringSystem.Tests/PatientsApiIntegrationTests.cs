using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CardiacPatientMonitoringSystem.Tests;

public class PatientsApiIntegrationTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public PatientsApiIntegrationTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetPatient_ReturnsNotFound_WhenPatientDoesNotExist()
    {
        // Arrange
        await AuthenticatePatientAsync();

        // Act
        var response = await _client.GetAsync("/api/Patients/99999");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetPatient_ReturnsOk_WhenPatientExists()
    {
        // Arrange
        await AuthenticatePatientAsync();

        // Act
        var response = await _client.GetAsync("/api/Patients/10");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var body = await response.Content.ReadAsStringAsync();

        using var json = JsonDocument.Parse(body);

        var root = json.RootElement;

        Assert.Equal(10, root.GetProperty("patientId").GetInt32());
        Assert.Equal(
            "patient17@example.com",
            root.GetProperty("email").GetString());
    }

    private async Task AuthenticatePatientAsync()
    {
        var loginResponse = await _client.PostAsJsonAsync(
            "/api/Auth/login",
            new
            {
                email = "patient17@example.com",
                password = "Patient@123"
            });

        loginResponse.EnsureSuccessStatusCode();

        var loginResult =
            await loginResponse.Content.ReadFromJsonAsync<LoginResponse>();

        Assert.NotNull(loginResult);
        Assert.False(string.IsNullOrWhiteSpace(loginResult.Token));

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                loginResult.Token);
    }

    private class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
    }
}