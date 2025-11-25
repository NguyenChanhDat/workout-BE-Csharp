using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FirstNETWebApp.Tests.UseCase
{
    [TestClass]
    public class CreateUserServiceTests
    {
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<IUserRepository> _mockUserRepository;
        private Mock<IHashService> _mockHashService;
        private CreateUserService _service;

        [TestInitialize]
        public void Setup()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockHashService = new Mock<IHashService>();
            _service = new CreateUserService(
                _mockUnitOfWork.Object,
                _mockUserRepository.Object,
                _mockHashService.Object
            );
        }

        [TestMethod]
        public async Task CreateUserAsync_WithValidRequest_ReturnsUserResponse()
        {
            // Arrange
            var request = new CreateUserRequest(
                "testuser",
                "test@example.com",
                "password123",
                MembershipTierEnum.Basic
            );

            var hashedPassword = "hashed_password_123";
            var createdUser = new User
            {
                Id = 1,
                Username = request.Username,
                Email = request.Email,
                Password = hashedPassword,
                MembershipTier = MembershipTierEnum.Basic
            };

            _mockHashService.Setup(h => h.HashPassword(request.Password))
                .ReturnsAsync(hashedPassword);

            _mockUnitOfWork.Setup(u => u.ExecuteAsync(It.IsAny<Func<Task>>()))
                .Callback<Func<Task>>(async action => await action())
                .Returns(Task.CompletedTask);

            _mockUserRepository.Setup(r => r.Create(It.IsAny<User>()))
                .ReturnsAsync(createdUser);

            // Act
            var result = await _service.CreateUserAsync(request);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(createdUser.Id, result.Id);
            Assert.AreEqual(createdUser.Username, result.Username);
            Assert.AreEqual(createdUser.Email, result.Email);
            Assert.AreEqual(createdUser.MembershipTier, result.MembershipTier);

            _mockHashService.Verify(h => h.HashPassword(request.Password), Times.Once);
            _mockUnitOfWork.Verify(u => u.ExecuteAsync(It.IsAny<Func<Task>>()), Times.Once);
        }

        [TestMethod]
        public async Task CreateUserAsync_WithNullMembershipTier_DefaultsToBasic()
        {
            // Arrange
            var request = new CreateUserRequest(
                "testuser",
                "test@example.com",
                "password123",
                null
            );

            var hashedPassword = "hashed_password_123";
            User? capturedUser = null;

            _mockHashService.Setup(h => h.HashPassword(request.Password))
                .ReturnsAsync(hashedPassword);

            _mockUnitOfWork.Setup(u => u.ExecuteAsync(It.IsAny<Func<Task>>()))
                .Callback<Func<Task>>(async action => await action())
                .Returns(Task.CompletedTask);

            _mockUserRepository.Setup(r => r.Create(It.IsAny<User>()))
                .Callback<User>(u => capturedUser = u)
                .ReturnsAsync((User u) => u);

            // Act
            var result = await _service.CreateUserAsync(request);

            // Assert
            Assert.IsNotNull(capturedUser);
            Assert.AreEqual(MembershipTierEnum.Basic, capturedUser.MembershipTier);
        }

        [TestMethod]
        public async Task CreateUserAsync_HashesPassword_BeforeCreatingUser()
        {
            // Arrange
            var request = new CreateUserRequest(
                "testuser",
                "test@example.com",
                "plainPassword",
                MembershipTierEnum.Advance
            );

            var hashedPassword = "very_secure_hashed_password";
            User? capturedUser = null;

            _mockHashService.Setup(h => h.HashPassword(request.Password))
                .ReturnsAsync(hashedPassword);

            _mockUnitOfWork.Setup(u => u.ExecuteAsync(It.IsAny<Func<Task>>()))
                .Callback<Func<Task>>(async action => await action())
                .Returns(Task.CompletedTask);

            _mockUserRepository.Setup(r => r.Create(It.IsAny<User>()))
                .Callback<User>(u => capturedUser = u)
                .ReturnsAsync((User u) => u);

            // Act
            await _service.CreateUserAsync(request);

            // Assert
            Assert.IsNotNull(capturedUser);
            Assert.AreEqual(hashedPassword, capturedUser.Password);
            Assert.AreNotEqual(request.Password, capturedUser.Password);
        }

        [TestMethod]
        public async Task CreateUserAsync_UsesUnitOfWork_ForTransactionManagement()
        {
            // Arrange
            var request = new CreateUserRequest(
                "testuser",
                "test@example.com",
                "password123",
                MembershipTierEnum.High
            );

            var hashedPassword = "hashed_password";
            var createdUser = new User { Id = 1, Username = request.Username, Email = request.Email, Password = hashedPassword, MembershipTier = MembershipTierEnum.High };

            _mockHashService.Setup(h => h.HashPassword(It.IsAny<string>()))
                .ReturnsAsync(hashedPassword);

            _mockUnitOfWork.Setup(u => u.ExecuteAsync(It.IsAny<Func<Task>>()))
                .Callback<Func<Task>>(async action => await action())
                .Returns(Task.CompletedTask);

            _mockUserRepository.Setup(r => r.Create(It.IsAny<User>()))
                .ReturnsAsync(createdUser);

            // Act
            await _service.CreateUserAsync(request);

            // Assert
            _mockUnitOfWork.Verify(u => u.ExecuteAsync(It.IsAny<Func<Task>>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task CreateUserAsync_WhenUserIsNull_ThrowsException()
        {
            // Arrange
            var request = new CreateUserRequest(
                "testuser",
                "test@example.com",
                "password123",
                MembershipTierEnum.Basic
            );

            _mockHashService.Setup(h => h.HashPassword(It.IsAny<string>()))
                .ReturnsAsync("hashed_password");

            _mockUnitOfWork.Setup(u => u.ExecuteAsync(It.IsAny<Func<Task>>()))
                .Callback<Func<Task>>(async action => await action())
                .Returns(Task.CompletedTask);

            _mockUserRepository.Setup(r => r.Create(It.IsAny<User>()))
                .ReturnsAsync((User?)null);

            // Act & Assert
            await _service.CreateUserAsync(request);
        }

        [TestMethod]
        public async Task CreateUserAsync_WithAdvanceMembershipTier_CreatesUserWithCorrectTier()
        {
            // Arrange
            var request = new CreateUserRequest(
                "premiumuser",
                "premium@example.com",
                "password123",
                MembershipTierEnum.Advance
            );

            var hashedPassword = "hashed_password";
            var createdUser = new User
            {
                Id = 2,
                Username = request.Username,
                Email = request.Email,
                Password = hashedPassword,
                MembershipTier = MembershipTierEnum.Advance
            };

            _mockHashService.Setup(h => h.HashPassword(request.Password))
                .ReturnsAsync(hashedPassword);

            _mockUnitOfWork.Setup(u => u.ExecuteAsync(It.IsAny<Func<Task>>()))
                .Callback<Func<Task>>(async action => await action())
                .Returns(Task.CompletedTask);

            _mockUserRepository.Setup(r => r.Create(It.IsAny<User>()))
                .ReturnsAsync(createdUser);

            // Act
            var result = await _service.CreateUserAsync(request);

            // Assert
            Assert.AreEqual(MembershipTierEnum.Advance, result.MembershipTier);
        }

        [TestMethod]
        public async Task CreateUserAsync_PreservesUsernameAndEmail()
        {
            // Arrange
            var request = new CreateUserRequest(
                "john_doe",
                "john.doe@example.com",
                "password123",
                MembershipTierEnum.Basic
            );

            var hashedPassword = "hashed_password";
            var createdUser = new User
            {
                Id = 3,
                Username = request.Username,
                Email = request.Email,
                Password = hashedPassword,
                MembershipTier = MembershipTierEnum.Basic
            };

            _mockHashService.Setup(h => h.HashPassword(request.Password))
                .ReturnsAsync(hashedPassword);

            _mockUnitOfWork.Setup(u => u.ExecuteAsync(It.IsAny<Func<Task>>()))
                .Callback<Func<Task>>(async action => await action())
                .Returns(Task.CompletedTask);

            _mockUserRepository.Setup(r => r.Create(It.IsAny<User>()))
                .ReturnsAsync(createdUser);

            // Act
            var result = await _service.CreateUserAsync(request);

            // Assert
            Assert.AreEqual("john_doe", result.Username);
            Assert.AreEqual("john.doe@example.com", result.Email);
        }
    }

    [TestClass]
    public class CreateUserUseCaseTests
    {
        [TestMethod]
        public async Task ExecuteAsync_CallsCreateUserService()
        {
            // Arrange
            var request = new CreateUserRequest(
                "testuser",
                "test@example.com",
                "password123",
                MembershipTierEnum.Basic
            );

            var expectedResponse = new CreateUserResponse(
                1,
                request.Username,
                request.Email,
                MembershipTierEnum.Basic
            );

            var mockService = new Mock<ICreateUserService>();
            mockService.Setup(s => s.CreateUserAsync(request))
                .ReturnsAsync(expectedResponse);

            var useCase = new CreateUserUseCase(mockService.Object);

            // Act
            var result = await useCase.ExecuteAsync(request);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResponse.Id, result.Id);
            Assert.AreEqual(expectedResponse.Username, result.Username);
            Assert.AreEqual(expectedResponse.Email, result.Email);
            Assert.AreEqual(expectedResponse.MembershipTier, result.MembershipTier);

            mockService.Verify(s => s.CreateUserAsync(request), Times.Once);
        }

        [TestMethod]
        public async Task ExecuteAsync_PropagatesExceptions()
        {
            // Arrange
            var request = new CreateUserRequest(
                "testuser",
                "test@example.com",
                "password123",
                MembershipTierEnum.Basic
            );

            var mockService = new Mock<ICreateUserService>();
            mockService.Setup(s => s.CreateUserAsync(request))
                .ThrowsAsync(new Exception("Database error"));

            var useCase = new CreateUserUseCase(mockService.Object);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () =>
            {
                await useCase.ExecuteAsync(request);
            });
        }
    }
}