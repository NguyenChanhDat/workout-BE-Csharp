using FirstNETWebApp.Services;
using FirstNETWebApp.UseCase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FirstNETWebApp.Tests.UseCase.CreateUser;

[TestClass]
public class CreateUserUseCaseTests
{
    private Mock<ICreateUserService> _mockCreateUserService = null!;
    private CreateUserUseCase _useCase = null!;

    [TestInitialize]
    public void Setup()
    {
        _mockCreateUserService = new Mock<ICreateUserService>();
        _useCase = new CreateUserUseCase(_mockCreateUserService.Object);
    }

    [TestCleanup]
    public void Cleanup()
    {
        _mockCreateUserService.Reset();
    }

    [TestMethod]
    public async Task ExecuteAsync_WithValidRequest_CallsServiceHandleAsync()
    {
        // Arrange
        var request = new CreateUserRequest("testuser", "test@example.com", "password", null);
        var expectedResponse = new CreateUserResponse(1, "testuser", "test@example.com", MembershipTierEnum.Basic);

        _mockCreateUserService
            .Setup(s => s.HandleAsync(request))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _useCase.ExecuteAsync(request);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(expectedResponse.Id, result.Id);
        Assert.AreEqual(expectedResponse.Username, result.Username);
        Assert.AreEqual(expectedResponse.Email, result.Email);
        Assert.AreEqual(expectedResponse.MembershipTier, result.MembershipTier);
        _mockCreateUserService.Verify(s => s.HandleAsync(request), Times.Once);
    }

    [TestMethod]
    public async Task ExecuteAsync_WithBasicMembershipTier_ReturnsCorrectResponse()
    {
        // Arrange
        var request = new CreateUserRequest("basicuser", "basic@example.com", "password", MembershipTierEnum.Basic);
        var expectedResponse = new CreateUserResponse(2, "basicuser", "basic@example.com", MembershipTierEnum.Basic);

        _mockCreateUserService
            .Setup(s => s.HandleAsync(request))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _useCase.ExecuteAsync(request);

        // Assert
        Assert.AreEqual(MembershipTierEnum.Basic, result.MembershipTier);
    }

    [TestMethod]
    public async Task ExecuteAsync_WithAdvanceMembershipTier_ReturnsCorrectResponse()
    {
        // Arrange
        var request = new CreateUserRequest("advanceuser", "advance@example.com", "password", MembershipTierEnum.Advance);
        var expectedResponse = new CreateUserResponse(3, "advanceuser", "advance@example.com", MembershipTierEnum.Advance);

        _mockCreateUserService
            .Setup(s => s.HandleAsync(request))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _useCase.ExecuteAsync(request);

        // Assert
        Assert.AreEqual(MembershipTierEnum.Advance, result.MembershipTier);
    }

    [TestMethod]
    public async Task ExecuteAsync_WithHighMembershipTier_ReturnsCorrectResponse()
    {
        // Arrange
        var request = new CreateUserRequest("highuser", "high@example.com", "password", MembershipTierEnum.High);
        var expectedResponse = new CreateUserResponse(4, "highuser", "high@example.com", MembershipTierEnum.High);

        _mockCreateUserService
            .Setup(s => s.HandleAsync(request))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _useCase.ExecuteAsync(request);

        // Assert
        Assert.AreEqual(MembershipTierEnum.High, result.MembershipTier);
    }

    [TestMethod]
    public async Task ExecuteAsync_WithNullMembershipTier_ReturnsDefaultTier()
    {
        // Arrange
        var request = new CreateUserRequest("defaultuser", "default@example.com", "password", null);
        var expectedResponse = new CreateUserResponse(5, "defaultuser", "default@example.com", MembershipTierEnum.Basic);

        _mockCreateUserService
            .Setup(s => s.HandleAsync(request))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _useCase.ExecuteAsync(request);

        // Assert
        Assert.AreEqual(MembershipTierEnum.Basic, result.MembershipTier);
    }

    [TestMethod]
    public async Task ExecuteAsync_PassesRequestUnmodifiedToService()
    {
        // Arrange
        var request = new CreateUserRequest("testuser", "test@example.com", "password", MembershipTierEnum.Advance);
        var expectedResponse = new CreateUserResponse(6, "testuser", "test@example.com", MembershipTierEnum.Advance);

        _mockCreateUserService
            .Setup(s => s.HandleAsync(It.Is<CreateUserRequest>(r =>
                r.Username == "testuser" &&
                r.Email == "test@example.com" &&
                r.Password == "password" &&
                r.MembershipTier == MembershipTierEnum.Advance
            )))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _useCase.ExecuteAsync(request);

        // Assert
        _mockCreateUserService.Verify(s => s.HandleAsync(It.Is<CreateUserRequest>(r =>
            r.Username == "testuser" &&
            r.Email == "test@example.com" &&
            r.Password == "password" &&
            r.MembershipTier == MembershipTierEnum.Advance
        )), Times.Once);
    }

    [TestMethod]
    public async Task ExecuteAsync_CallsServiceExactlyOnce()
    {
        // Arrange
        var request = new CreateUserRequest("testuser", "test@example.com", "password", null);
        var expectedResponse = new CreateUserResponse(7, "testuser", "test@example.com", MembershipTierEnum.Basic);

        _mockCreateUserService
            .Setup(s => s.HandleAsync(request))
            .ReturnsAsync(expectedResponse);

        // Act
        await _useCase.ExecuteAsync(request);

        // Assert
        _mockCreateUserService.Verify(s => s.HandleAsync(It.IsAny<CreateUserRequest>()), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task ExecuteAsync_WhenServiceThrowsException_PropagatesException()
    {
        // Arrange
        var request = new CreateUserRequest("testuser", "test@example.com", "password", null);

        _mockCreateUserService
            .Setup(s => s.HandleAsync(request))
            .ThrowsAsync(new Exception("Service error"));

        // Act
        await _useCase.ExecuteAsync(request);

        // Assert is handled by ExpectedException
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task ExecuteAsync_WhenServiceThrowsArgumentException_PropagatesException()
    {
        // Arrange
        var request = new CreateUserRequest("", "test@example.com", "password", null);

        _mockCreateUserService
            .Setup(s => s.HandleAsync(request))
            .ThrowsAsync(new ArgumentException("Invalid argument"));

        // Act
        await _useCase.ExecuteAsync(request);

        // Assert is handled by ExpectedException
    }

    [TestMethod]
    public async Task ExecuteAsync_ReturnsResponseFromService()
    {
        // Arrange
        var request = new CreateUserRequest("testuser", "test@example.com", "password", null);
        var expectedResponse = new CreateUserResponse(8, "testuser", "test@example.com", MembershipTierEnum.Basic);

        _mockCreateUserService
            .Setup(s => s.HandleAsync(request))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _useCase.ExecuteAsync(request);

        // Assert
        Assert.AreSame(expectedResponse, result);
    }

    [TestMethod]
    public async Task ExecuteAsync_WithEmptyUsername_PassesToService()
    {
        // Arrange
        var request = new CreateUserRequest("", "test@example.com", "password", null);
        var expectedResponse = new CreateUserResponse(9, "", "test@example.com", MembershipTierEnum.Basic);

        _mockCreateUserService
            .Setup(s => s.HandleAsync(request))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _useCase.ExecuteAsync(request);

        // Assert
        _mockCreateUserService.Verify(s => s.HandleAsync(It.Is<CreateUserRequest>(r => r.Username == "")), Times.Once);
    }

    [TestMethod]
    public async Task ExecuteAsync_WithEmptyEmail_PassesToService()
    {
        // Arrange
        var request = new CreateUserRequest("testuser", "", "password", null);
        var expectedResponse = new CreateUserResponse(10, "testuser", "", MembershipTierEnum.Basic);

        _mockCreateUserService
            .Setup(s => s.HandleAsync(request))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _useCase.ExecuteAsync(request);

        // Assert
        _mockCreateUserService.Verify(s => s.HandleAsync(It.Is<CreateUserRequest>(r => r.Email == "")), Times.Once);
    }

    [TestMethod]
    public async Task ExecuteAsync_WithEmptyPassword_PassesToService()
    {
        // Arrange
        var request = new CreateUserRequest("testuser", "test@example.com", "", null);
        var expectedResponse = new CreateUserResponse(11, "testuser", "test@example.com", MembershipTierEnum.Basic);

        _mockCreateUserService
            .Setup(s => s.HandleAsync(request))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _useCase.ExecuteAsync(request);

        // Assert
        _mockCreateUserService.Verify(s => s.HandleAsync(It.Is<CreateUserRequest>(r => r.Password == "")), Times.Once);
    }

    [TestMethod]
    public async Task ExecuteAsync_WithSpecialCharacters_HandlesCorrectly()
    {
        // Arrange
        var request = new CreateUserRequest("user@#$", "test@example.com", "p@$$w0rd!", null);
        var expectedResponse = new CreateUserResponse(12, "user@#$", "test@example.com", MembershipTierEnum.Basic);

        _mockCreateUserService
            .Setup(s => s.HandleAsync(request))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _useCase.ExecuteAsync(request);

        // Assert
        Assert.IsNotNull(result);
        _mockCreateUserService.Verify(s => s.HandleAsync(request), Times.Once);
    }

    [TestMethod]
    public async Task ExecuteAsync_WithLongInputs_PassesToService()
    {
        // Arrange
        var longString = new string('a', 1000);
        var request = new CreateUserRequest(longString, $"{longString}@example.com", longString, null);
        var expectedResponse = new CreateUserResponse(13, longString, $"{longString}@example.com", MembershipTierEnum.Basic);

        _mockCreateUserService
            .Setup(s => s.HandleAsync(request))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _useCase.ExecuteAsync(request);

        // Assert
        _mockCreateUserService.Verify(s => s.HandleAsync(request), Times.Once);
    }

    [TestMethod]
    public async Task ExecuteAsync_WithUnicodeCharacters_HandlesCorrectly()
    {
        // Arrange
        var request = new CreateUserRequest("用户", "测试@example.com", "密码", null);
        var expectedResponse = new CreateUserResponse(14, "用户", "测试@example.com", MembershipTierEnum.Basic);

        _mockCreateUserService
            .Setup(s => s.HandleAsync(request))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _useCase.ExecuteAsync(request);

        // Assert
        Assert.IsNotNull(result);
    }
}