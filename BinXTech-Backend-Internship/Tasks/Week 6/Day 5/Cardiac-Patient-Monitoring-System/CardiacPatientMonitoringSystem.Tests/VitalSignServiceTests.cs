using CardiacPatientMonitoringSystem.Services;

namespace CardiacPatientMonitoringSystem.Tests;

public class VitalSignServiceTests
{
    private readonly VitalSignService _service;

    public VitalSignServiceTests()
    {
        _service = new VitalSignService(null!);
    }

    [Fact]
    public void GetVitalSignStatus_ReturnsNormal_WhenVitalsAreNormal()
    {
        // Act
        var result = _service.GetVitalSignStatus(80, 98, 120);

        // Assert
        Assert.Equal("Normal", result);
    }

    [Fact]
    public void GetVitalSignStatus_ReturnsWarning_WhenHeartRateIsHigh()
    {
        // Act
        var result = _service.GetVitalSignStatus(110, 98, 120);

        // Assert
        Assert.Equal("Warning", result);
    }

    [Fact]
    public void GetVitalSignStatus_ReturnsCritical_WhenOxygenIsLow()
    {
        // Act
        var result = _service.GetVitalSignStatus(80, 88, 120);

        // Assert
        Assert.Equal("Critical", result);
    }

    [Theory]
    [InlineData(80, 98, 120, "Normal")]
    [InlineData(110, 98, 120, "Warning")]
    [InlineData(145, 88, 170, "Critical")]
    public void GetVitalSignStatus_ReturnsExpectedStatus(
        int heartRate,
        int oxygenSaturation,
        int systolicBloodPressure,
        string expected)
    {
        // Act
        var result = _service.GetVitalSignStatus(
            heartRate,
            oxygenSaturation,
            systolicBloodPressure);

        // Assert
        Assert.Equal(expected, result);
    }
}