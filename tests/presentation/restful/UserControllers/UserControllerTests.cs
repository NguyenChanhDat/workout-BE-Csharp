using FirstNETWebApp.presentation.restful.UserControllers;
using FirstNETWebApp.UseCase.Base.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FirstNETWebApp.Tests.Presentation.Restful.UserControllers;

[TestClass]
public class UserControllerTests
{
    private Mock<IMutationUseCase<CreateUserRequest, CreateUserResponse>> _mockCreateUserUseCase = null!;
    private UserController _controller = null!;

    [TestInitialize]
    public void Setup()
    {
        _mockCreateUserUseCase = new Mock<IMutationUseCase<CreateUserRequest, CreateUserResponse>>();
        _controller = new UserController(_mockCreateUserUseCase.Object);
    }

    [TestCleanup]
    public void Cleanup()
    {
        _mockCreateUserUseCase.Reset();
    }

    [TestMethod]
    public async Task PostUser_WithValidRequest_ReturnsCreateUserResponse()
    {
        // Arrange
        var request = new CreateUserRequest("testuser", "test@example.com", "password123", null);
        var expectedResponse = new CreateUserResponse(1, "testuser", "test@example.com", MembershipTierEnum.Basic);

        _mockCreateUserUseCase
            .Setup(u => u.ExecuteAsync(request))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.PostUser(request);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result.Value, typeof(CreateUserResponse));
        Assert.AreEqual(expectedResponse.Id, result.Value!.Id);
        Assert.AreEqual(expectedResponse.Username, result.Value.Username);
        Assert.AreEqual(expectedResponse.Email, result.Value.Email);
        Assert.AreEqual(expectedResponse.MembershipTier, result.Value.MembershipTier);
        _mockCreateUserUseCase.Verify(u => u.ExecuteAsync(request), Times.Once);
    }

    [TestMethod]
    public async Task PostUser_WithBasicMembershipTier_ReturnsBasicTier()
    {
        // Arrange
        var request = new CreateUserRequest("basicuser", "basic@example.com", "password123", MembershipTierEnum.Basic);
        var expectedResponse = new CreateUserResponse(2, "basicuser", "basic@example.com", MembershipTierEnum.Basic);

        _mockCreateUserUseCase
            .Setup(u => u.ExecuteAsync(request))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.PostUser(request);

        // Assert
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(MembershipTierEnum.Basic, result.Value.MembershipTier);
    }

    [TestMethod]
    public async Task PostUser_WithAdvanceMembershipTier_ReturnsAdvanceTier()
    {
        // Arrange
        var request = new CreateUserRequest("advanceuser", "advance@example.com", "password123", MembershipTierEnum.Advance);
        var expectedResponse = new CreateUserResponse(3, "advanceuser", "advance@example.com", MembershipTierEnum.Advance);

        _mockCreateUserUseCase
            .Setup(u => u.ExecuteAsync(request))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.PostUser(request);

        // Assert
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(MembershipTierEnum.Advance, result.Value.MembershipTier);
    }

    [TestMethod]
    public async Task PostUser_WithHighMembershipTier_ReturnsHighTier()
    {
        // Arrange
        var request = new CreateUserRequest("highuser", "high@example.com", "password123", MembershipTierEnum.High);
        var expectedResponse = new CreateUserResponse(4, "highuser", "high@example.com", MembershipTierEnum.High);

        _mockCreateUserUseCase
            .Setup(u => u.ExecuteAsync(request))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.PostUser(request);

        // Assert
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(MembershipTierEnum.High, result.Value.MembershipTier);
    }

    [TestMethod]
    public async Task PostUser_WithNullMembershipTier_ReturnsDefaultBasicTier()
    {
        // Arrange
        var request = new CreateUserRequest("defaultuser", "default@example.com", "password123", null);
        var expectedResponse = new CreateUserResponse(5, "defaultuser", "default@example.com", MembershipTierEnum.Basic);

        _mockCreateUserUseCase
            .Setup(u => u.ExecuteAsync(request))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.PostUser(request);

        // Assert
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(MembershipTierEnum.Basic, result.Value.MembershipTier);
    }

    [TestMethod]
    public async Task PostUser_WithEmptyUsername_StillCallsUseCase()
    {
        // Arrange
        var request = new CreateUserRequest("", "test@example.com", "password123", null);
        var expectedResponse = new CreateUserResponse(6, "", "test@example.com", MembershipTierEnum.Basic);

        _mockCreateUserUseCase
            .Setup(u => u.ExecuteAsync(request))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.PostUser(request);

        // Assert
        Assert.IsNotNull(result.Value);
        _mockCreateUserUseCase.Verify(u => u.ExecuteAsync(request), Times.Once);
    }

    [TestMethod]
    public async Task PostUser_WithEmptyEmail_StillCallsUseCase()
    {
        // Arrange
        var request = new CreateUserRequest("testuser", "", "password123", null);
        var expectedResponse = new CreateUserResponse(7, "testuser", "", MembershipTierEnum.Basic);

        _mockCreateUserUseCase
            .Setup(u => u.ExecuteAsync(request))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.PostUser(request);

        // Assert
        Assert.IsNotNull(result.Value);
        _mockCreateUserUseCase.Verify(u => u.ExecuteAsync(request), Times.Once);
    }

    [TestMethod]
    public async Task PostUser_WithEmptyPassword_StillCallsUseCase()
    {
        // Arrange
        var request = new CreateUserRequest("testuser", "test@example.com", "", null);
        var expectedResponse = new CreateUserResponse(8, "testuser", "test@example.com", MembershipTierEnum.Basic);

        _mockCreateUserUseCase
            .Setup(u => u.ExecuteAsync(request))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.PostUser(request);

        // Assert
        Assert.IsNotNull(result.Value);
        _mockCreateUserUseCase.Verify(u => u.ExecuteAsync(request), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task PostUser_WhenUseCaseThrowsException_PropagatesException()
    {
        // Arrange
        var request = new CreateUserRequest("testuser", "test@example.com", "password123", null);

        _mockCreateUserUseCase
            .Setup(u => u.ExecuteAsync(request))
            .ThrowsAsync(new Exception("Database connection failed"));

        // Act
        await _controller.PostUser(request);

        // Assert is handled by ExpectedException
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task PostUser_WhenUseCaseThrowsArgumentException_PropagatesException()
    {
        // Arrange
        var request = new CreateUserRequest("", "test@example.com", "password123", null);

        _mockCreateUserUseCase
            .Setup(u => u.ExecuteAsync(request))
            .ThrowsAsync(new ArgumentException("Username is required"));

        // Act
        await _controller.PostUser(request);

        // Assert is handled by ExpectedException
    }

    [TestMethod]
    public async Task PostUser_WithSpecialCharactersInUsername_HandlesCorrectly()
    {
        // Arrange
        var request = new CreateUserRequest("user@#$%", "test@example.com", "password123", null);
        var expectedResponse = new CreateUserResponse(9, "user@#$%", "test@example.com", MembershipTierEnum.Basic);

        _mockCreateUserUseCase
            .Setup(u => u.ExecuteAsync(request))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.PostUser(request);

        // Assert
        Assert.IsNotNull(result.Value);
        Assert.AreEqual("user@#$%", result.Value.Username);
    }

    [TestMethod]
    public async Task PostUser_WithLongUsername_HandlesCorrectly()
    {
        // Arrange
        var longUsername = new string('a', 1000);
        var request = new CreateUserRequest(longUsername, "test@example.com", "password123", null);
        var expectedResponse = new CreateUserResponse(10, longUsername, "test@example.com", MembershipTierEnum.Basic);

        _mockCreateUserUseCase
            .Setup(u => u.ExecuteAsync(request))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.PostUser(request);

        // Assert
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(longUsername, result.Value.Username);
    }

    [TestMethod]
    public async Task PostUser_WithUnicodeCharacters_HandlesCorrectly()
    {
        // Arrange
        var request = new CreateUserRequest("用户名", "test@example.com", "password123", null);
        var expectedResponse = new CreateUserResponse(11, "用户名", "test@example.com", MembershipTierEnum.Basic);

        _mockCreateUserUseCase
            .Setup(u => u.ExecuteAsync(request))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.PostUser(request);

        // Assert
        Assert.IsNotNull(result.Value);
        Assert.AreEqual("用户名", result.Value.Username);
    }

    [TestMethod]
    public async Task PostUser_CallsUseCaseExactlyOnce()
    {
        // Arrange
        var request = new CreateUserRequest("testuser", "test@example.com", "password123", null);
        var expectedResponse = new CreateUserResponse(12, "testuser", "test@example.com", MembershipTierEnum.Basic);

        _mockCreateUserUseCase
            .Setup(u => u.ExecuteAsync(request))
            .ReturnsAsync(expectedResponse);

        // Act
        await _controller.PostUser(request);

        // Assert
        _mockCreateUserUseCase.Verify(u => u.ExecuteAsync(It.IsAny<CreateUserRequest>()), Times.Once);
    }

    [TestMethod]
    public async Task PostUser_ReturnsActionResultWithCorrectType()
    {
        // Arrange
        var request = new CreateUserRequest("testuser", "test@example.com", "password123", null);
        var expectedResponse = new CreateUserResponse(13, "testuser", "test@example.com", MembershipTierEnum.Basic);

        _mockCreateUserUseCase
            .Setup(u => u.ExecuteAsync(request))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.PostUser(request);

        // Assert
        Assert.IsInstanceOfType(result, typeof(ActionResult<CreateUserResponse>));
    }
}