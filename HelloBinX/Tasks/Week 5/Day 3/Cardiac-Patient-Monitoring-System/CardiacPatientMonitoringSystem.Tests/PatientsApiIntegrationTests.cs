using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CardiacPatientMonitoringSystem.Tests;

public class PatientsApiIntegrationTests
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public PatientsApiIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetPatient_ReturnsNotFound_WhenPatientDoesNotExist()
    {
        // Arrange
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

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                loginResult!.Token);

        // Act
        var response = await _client.GetAsync("/api/Patients/99999");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    private class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
    }
}