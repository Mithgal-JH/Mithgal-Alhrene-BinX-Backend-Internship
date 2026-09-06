using CardiacPatientMonitoringSystem.Models;
using CardiacPatientMonitoringSystem.Repositories.Interfaces;
using CardiacPatientMonitoringSystem.Services;
using Moq;

namespace CardiacPatientMonitoringSystem.Tests;

public class PatientServiceTests
{
    [Fact]
    public async Task GetByIdAsync_ReturnsPatient_WhenPatientExistsAndUserIsOwner()
    {
        // Arrange
        var patient = new Patient
        {
            PatientId = 1,
            UserId = "user-1",
            MedicalRecordNumber = "MRN-001",
            FirstName = "John",
            LastName = "Doe"
        };

        var mockRepository = new Mock<IPatientRepository>();

        mockRepository
            .Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(patient);

        var service = new PatientService(mockRepository.Object);

        // Act
        var result = await service.GetByIdAsync(
            1,
            "user-1",
            true,
            false);

        // Assert
        Assert.NotNull(result.Patient);
        Assert.False(result.NotOwner);
        Assert.Equal(1, result.Patient!.PatientId);
        Assert.Equal("John", result.Patient.FirstName);

        mockRepository.Verify(
            r => r.GetByIdAsync(1),
            Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenPatientDoesNotExist()
    {
        // Arrange
        var mockRepository = new Mock<IPatientRepository>();

        mockRepository
            .Setup(r => r.GetByIdAsync(999))
            .ReturnsAsync((Patient?)null);

        var service = new PatientService(mockRepository.Object);

        // Act
        var result = await service.GetByIdAsync(
            999,
            "user-1",
            true,
            false);

        // Assert
        Assert.Null(result.Patient);
        Assert.False(result.NotOwner);

        mockRepository.Verify(
            r => r.GetByIdAsync(999),
            Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNotOwner_WhenPatientBelongsToAnotherUser()
    {
        // Arrange
        var patient = new Patient
        {
            PatientId = 1,
            UserId = "user-2",
            MedicalRecordNumber = "MRN-001",
            FirstName = "John",
            LastName = "Doe"
        };

        var mockRepository = new Mock<IPatientRepository>();

        mockRepository
            .Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(patient);

        var service = new PatientService(mockRepository.Object);

        // Act
        var result = await service.GetByIdAsync(
            1,
            "user-1",
            false,
            false);

        // Assert
        Assert.Null(result.Patient);
        Assert.True(result.NotOwner);

        mockRepository.Verify(
            r => r.GetByIdAsync(1),
            Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_ThrowsException_WhenRepositoryFails()
    {
        // Arrange
        var mockRepository = new Mock<IPatientRepository>();

        mockRepository
            .Setup(r => r.GetByIdAsync(1))
            .ThrowsAsync(new Exception("Database error"));

        var service = new PatientService(mockRepository.Object);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(
            () => service.GetByIdAsync(
                1,
                "user-1",
                true,
                false));

        mockRepository.Verify(
            r => r.GetByIdAsync(1),
            Times.Once);
    }
}