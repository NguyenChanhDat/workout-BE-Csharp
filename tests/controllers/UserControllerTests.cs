using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using Moq;
using FirstNETWebApp.presentation.restful.UserControllers;

namespace FirstNETWebApp.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests
    {
        private Mock<IUseCase<CreateUserRequest, CreateUserResponse>> _mockUseCase;
        private UserController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockUseCase = new Mock<IUseCase<CreateUserRequest, CreateUserResponse>>();
            _controller = new UserController(_mockUseCase.Object);
        }

        [TestMethod]
        public async Task PostUser_WithValidRequest_ReturnsCreatedUser()
        {
            // Arrange
            var request = new CreateUserRequest("testuser", "test@example.com", "password123", MembershipTierEnum.Basic);
            var expectedResponse = new CreateUserResponse(1, "testuser", "test@example.com", MembershipTierEnum.Basic);

            _mockUseCase.Setup(u => u.ExecuteAsync(request))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.PostUser(request);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(expectedResponse.Id, result.Value.Id);
            Assert.AreEqual(expectedResponse.Username, result.Value.Username);
            Assert.AreEqual(expectedResponse.Email, result.Value.Email);
            Assert.AreEqual(expectedResponse.MembershipTier, result.Value.MembershipTier);

            _mockUseCase.Verify(u => u.ExecuteAsync(request), Times.Once);
        }

        [TestMethod]
        public async Task PostUser_CallsUseCaseWithCorrectRequest()
        {
            // Arrange
            var request = new CreateUserRequest("john_doe", "john@example.com", "securepass", MembershipTierEnum.Advance);
            var response = new CreateUserResponse(5, "john_doe", "john@example.com", MembershipTierEnum.Advance);

            _mockUseCase.Setup(u => u.ExecuteAsync(It.IsAny<CreateUserRequest>()))
                .ReturnsAsync(response);

            // Act
            await _controller.PostUser(request);

            // Assert
            _mockUseCase.Verify(u => u.ExecuteAsync(It.Is<CreateUserRequest>(r =>
                r.Username == "john_doe" &&
                r.Email == "john@example.com" &&
                r.Password == "securepass" &&
                r.MembershipTier == MembershipTierEnum.Advance
            )), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task PostUser_WhenUseCaseFails_PropagatesException()
        {
            // Arrange
            var request = new CreateUserRequest("testuser", "test@example.com", "password123", MembershipTierEnum.Basic);

            _mockUseCase.Setup(u => u.ExecuteAsync(request))
                .ThrowsAsync(new Exception("Database connection failed"));

            // Act & Assert
            await _controller.PostUser(request);
        }

        [TestMethod]
        public async Task PostUser_WithNullMembershipTier_HandlesCorrectly()
        {
            // Arrange
            var request = new CreateUserRequest("testuser", "test@example.com", "password123", null);
            var response = new CreateUserResponse(1, "testuser", "test@example.com", MembershipTierEnum.Basic);

            _mockUseCase.Setup(u => u.ExecuteAsync(request))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.PostUser(request);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(MembershipTierEnum.Basic, result.Value.MembershipTier);
        }

        [TestMethod]
        public async Task PostUser_WithHighTierMembership_ReturnsCorrectResponse()
        {
            // Arrange
            var request = new CreateUserRequest("vipuser", "vip@example.com", "password123", MembershipTierEnum.High);
            var response = new CreateUserResponse(10, "vipuser", "vip@example.com", MembershipTierEnum.High);

            _mockUseCase.Setup(u => u.ExecuteAsync(request))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.PostUser(request);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(MembershipTierEnum.High, result.Value.MembershipTier);
        }
    }
}