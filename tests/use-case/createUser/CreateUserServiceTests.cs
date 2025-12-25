using FirstNETWebApp.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FirstNETWebApp.Tests.UseCase.CreateUser;

[TestClass]
public class CreateUserServiceTests
{
    private Mock<IUserRepository> _mockUserRepository = null!;
    private Mock<IHashService> _mockHashService = null!;
    private CreateUserService _service = null!;

    [TestInitialize]
    public void Setup()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _mockHashService = new Mock<IHashService>();
        _service = new CreateUserService(_mockUserRepository.Object, _mockHashService.Object);
    }

    [TestCleanup]
    public void Cleanup()
    {
        _mockUserRepository.Reset();
        _mockHashService.Reset();
    }

    [TestMethod]
    public async Task HandleAsync_WithValidRequest_CreatesUserSuccessfully()
    {
        // Arrange
        var request = new CreateUserRequest("testuser", "test@example.com", "plainpassword", null);
        var hashedPassword = "hashed_password_123";
        var createdUser = new User
        {
            Id = 1,
            Username = "testuser",
            Email = "test@example.com",
            Password = hashedPassword,
            MembershipTier = MembershipTierEnum.Basic
        };

        _mockHashService
            .Setup(h => h.HashPassword("plainpassword"))
            .ReturnsAsync(hashedPassword);

        _mockUserRepository
            .Setup(r => r.Create(It.IsAny<User>()))
            .ReturnsAsync(createdUser);

        // Act
        var result = await _service.HandleAsync(request);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Id);
        Assert.AreEqual("testuser", result.Username);
        Assert.AreEqual("test@example.com", result.Email);
        Assert.AreEqual(MembershipTierEnum.Basic, result.MembershipTier);
        _mockHashService.Verify(h => h.HashPassword("plainpassword"), Times.Once);
        _mockUserRepository.Verify(r => r.Create(It.Is<User>(u =>
            u.Username == "testuser" &&
            u.Email == "test@example.com" &&
            u.Password == hashedPassword &&
            u.MembershipTier == MembershipTierEnum.Basic
        )), Times.Once);
    }

    [TestMethod]
    public async Task HandleAsync_WithNullMembershipTier_DefaultsToBasic()
    {
        // Arrange
        var request = new CreateUserRequest("testuser", "test@example.com", "password", null);
        var hashedPassword = "hashed";
        var createdUser = new User
        {
            Id = 2,
            Username = "testuser",
            Email = "test@example.com",
            Password = hashedPassword,
            MembershipTier = MembershipTierEnum.Basic
        };

        _mockHashService.Setup(h => h.HashPassword(It.IsAny<string>())).ReturnsAsync(hashedPassword);
        _mockUserRepository.Setup(r => r.Create(It.IsAny<User>())).ReturnsAsync(createdUser);

        // Act
        var result = await _service.HandleAsync(request);

        // Assert
        Assert.AreEqual(MembershipTierEnum.Basic, result.MembershipTier);
        _mockUserRepository.Verify(r => r.Create(It.Is<User>(u => u.MembershipTier == MembershipTierEnum.Basic)), Times.Once);
    }

    [TestMethod]
    public async Task HandleAsync_WithBasicMembershipTier_CreatesBasicUser()
    {
        // Arrange
        var request = new CreateUserRequest("basicuser", "basic@example.com", "password", MembershipTierEnum.Basic);
        var hashedPassword = "hashed";
        var createdUser = new User
        {
            Id = 3,
            Username = "basicuser",
            Email = "basic@example.com",
            Password = hashedPassword,
            MembershipTier = MembershipTierEnum.Basic
        };

        _mockHashService.Setup(h => h.HashPassword(It.IsAny<string>())).ReturnsAsync(hashedPassword);
        _mockUserRepository.Setup(r => r.Create(It.IsAny<User>())).ReturnsAsync(createdUser);

        // Act
        var result = await _service.HandleAsync(request);

        // Assert
        Assert.AreEqual(MembershipTierEnum.Basic, result.MembershipTier);
    }

    [TestMethod]
    public async Task HandleAsync_WithAdvanceMembershipTier_CreatesAdvanceUser()
    {
        // Arrange
        var request = new CreateUserRequest("advanceuser", "advance@example.com", "password", MembershipTierEnum.Advance);
        var hashedPassword = "hashed";
        var createdUser = new User
        {
            Id = 4,
            Username = "advanceuser",
            Email = "advance@example.com",
            Password = hashedPassword,
            MembershipTier = MembershipTierEnum.Advance
        };

        _mockHashService.Setup(h => h.HashPassword(It.IsAny<string>())).ReturnsAsync(hashedPassword);
        _mockUserRepository.Setup(r => r.Create(It.IsAny<User>())).ReturnsAsync(createdUser);

        // Act
        var result = await _service.HandleAsync(request);

        // Assert
        Assert.AreEqual(MembershipTierEnum.Advance, result.MembershipTier);
        _mockUserRepository.Verify(r => r.Create(It.Is<User>(u => u.MembershipTier == MembershipTierEnum.Advance)), Times.Once);
    }

    [TestMethod]
    public async Task HandleAsync_WithHighMembershipTier_CreatesHighUser()
    {
        // Arrange
        var request = new CreateUserRequest("highuser", "high@example.com", "password", MembershipTierEnum.High);
        var hashedPassword = "hashed";
        var createdUser = new User
        {
            Id = 5,
            Username = "highuser",
            Email = "high@example.com",
            Password = hashedPassword,
            MembershipTier = MembershipTierEnum.High
        };

        _mockHashService.Setup(h => h.HashPassword(It.IsAny<string>())).ReturnsAsync(hashedPassword);
        _mockUserRepository.Setup(r => r.Create(It.IsAny<User>())).ReturnsAsync(createdUser);

        // Act
        var result = await _service.HandleAsync(request);

        // Assert
        Assert.AreEqual(MembershipTierEnum.High, result.MembershipTier);
        _mockUserRepository.Verify(r => r.Create(It.Is<User>(u => u.MembershipTier == MembershipTierEnum.High)), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task HandleAsync_WhenRepositoryReturnsNull_ThrowsException()
    {
        // Arrange
        var request = new CreateUserRequest("testuser", "test@example.com", "password", null);
        var hashedPassword = "hashed";

        _mockHashService.Setup(h => h.HashPassword(It.IsAny<string>())).ReturnsAsync(hashedPassword);
        _mockUserRepository.Setup(r => r.Create(It.IsAny<User>())).ReturnsAsync((User?)null);

        // Act
        await _service.HandleAsync(request);

        // Assert is handled by ExpectedException
    }

    [TestMethod]
    public async Task HandleAsync_HashesPasswordBeforeCreatingUser()
    {
        // Arrange
        var request = new CreateUserRequest("testuser", "test@example.com", "plainpassword", null);
        var hashedPassword = "secure_hashed_password";
        var createdUser = new User
        {
            Id = 6,
            Username = "testuser",
            Email = "test@example.com",
            Password = hashedPassword,
            MembershipTier = MembershipTierEnum.Basic
        };

        var sequence = new MockSequence();
        _mockHashService.InSequence(sequence)
            .Setup(h => h.HashPassword("plainpassword"))
            .ReturnsAsync(hashedPassword);

        _mockUserRepository.InSequence(sequence)
            .Setup(r => r.Create(It.Is<User>(u => u.Password == hashedPassword)))
            .ReturnsAsync(createdUser);

        // Act
        var result = await _service.HandleAsync(request);

        // Assert
        Assert.IsNotNull(result);
        _mockHashService.Verify(h => h.HashPassword("plainpassword"), Times.Once);
        _mockUserRepository.Verify(r => r.Create(It.Is<User>(u => u.Password == hashedPassword)), Times.Once);
    }

    [TestMethod]
    public async Task HandleAsync_WithEmptyPassword_StillHashesIt()
    {
        // Arrange
        var request = new CreateUserRequest("testuser", "test@example.com", "", null);
        var hashedPassword = "hashed_empty";
        var createdUser = new User
        {
            Id = 7,
            Username = "testuser",
            Email = "test@example.com",
            Password = hashedPassword,
            MembershipTier = MembershipTierEnum.Basic
        };

        _mockHashService.Setup(h => h.HashPassword("")).ReturnsAsync(hashedPassword);
        _mockUserRepository.Setup(r => r.Create(It.IsAny<User>())).ReturnsAsync(createdUser);

        // Act
        var result = await _service.HandleAsync(request);

        // Assert
        _mockHashService.Verify(h => h.HashPassword(""), Times.Once);
    }

    [TestMethod]
    public async Task HandleAsync_WithLongPassword_HashesCorrectly()
    {
        // Arrange
        var longPassword = new string('a', 1000);
        var request = new CreateUserRequest("testuser", "test@example.com", longPassword, null);
        var hashedPassword = "hashed_long_password";
        var createdUser = new User
        {
            Id = 8,
            Username = "testuser",
            Email = "test@example.com",
            Password = hashedPassword,
            MembershipTier = MembershipTierEnum.Basic
        };

        _mockHashService.Setup(h => h.HashPassword(longPassword)).ReturnsAsync(hashedPassword);
        _mockUserRepository.Setup(r => r.Create(It.IsAny<User>())).ReturnsAsync(createdUser);

        // Act
        var result = await _service.HandleAsync(request);

        // Assert
        _mockHashService.Verify(h => h.HashPassword(longPassword), Times.Once);
    }

    [TestMethod]
    public async Task HandleAsync_PreservesUsernameInResponse()
    {
        // Arrange
        var request = new CreateUserRequest("unique_username", "test@example.com", "password", null);
        var hashedPassword = "hashed";
        var createdUser = new User
        {
            Id = 9,
            Username = "unique_username",
            Email = "test@example.com",
            Password = hashedPassword,
            MembershipTier = MembershipTierEnum.Basic
        };

        _mockHashService.Setup(h => h.HashPassword(It.IsAny<string>())).ReturnsAsync(hashedPassword);
        _mockUserRepository.Setup(r => r.Create(It.IsAny<User>())).ReturnsAsync(createdUser);

        // Act
        var result = await _service.HandleAsync(request);

        // Assert
        Assert.AreEqual("unique_username", result.Username);
    }

    [TestMethod]
    public async Task HandleAsync_PreservesEmailInResponse()
    {
        // Arrange
        var request = new CreateUserRequest("testuser", "unique@test.com", "password", null);
        var hashedPassword = "hashed";
        var createdUser = new User
        {
            Id = 10,
            Username = "testuser",
            Email = "unique@test.com",
            Password = hashedPassword,
            MembershipTier = MembershipTierEnum.Basic
        };

        _mockHashService.Setup(h => h.HashPassword(It.IsAny<string>())).ReturnsAsync(hashedPassword);
        _mockUserRepository.Setup(r => r.Create(It.IsAny<User>())).ReturnsAsync(createdUser);

        // Act
        var result = await _service.HandleAsync(request);

        // Assert
        Assert.AreEqual("unique@test.com", result.Email);
    }

    [TestMethod]
    public async Task HandleAsync_ReturnsGeneratedIdFromRepository()
    {
        // Arrange
        var request = new CreateUserRequest("testuser", "test@example.com", "password", null);
        var hashedPassword = "hashed";
        var createdUser = new User
        {
            Id = 999,
            Username = "testuser",
            Email = "test@example.com",
            Password = hashedPassword,
            MembershipTier = MembershipTierEnum.Basic
        };

        _mockHashService.Setup(h => h.HashPassword(It.IsAny<string>())).ReturnsAsync(hashedPassword);
        _mockUserRepository.Setup(r => r.Create(It.IsAny<User>())).ReturnsAsync(createdUser);

        // Act
        var result = await _service.HandleAsync(request);

        // Assert
        Assert.AreEqual(999, result.Id);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task HandleAsync_WhenHashServiceThrowsException_PropagatesException()
    {
        // Arrange
        var request = new CreateUserRequest("testuser", "test@example.com", "password", null);

        _mockHashService
            .Setup(h => h.HashPassword(It.IsAny<string>()))
            .ThrowsAsync(new Exception("Hashing failed"));

        // Act
        await _service.HandleAsync(request);

        // Assert is handled by ExpectedException
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task HandleAsync_WhenRepositoryThrowsException_PropagatesException()
    {
        // Arrange
        var request = new CreateUserRequest("testuser", "test@example.com", "password", null);
        var hashedPassword = "hashed";

        _mockHashService.Setup(h => h.HashPassword(It.IsAny<string>())).ReturnsAsync(hashedPassword);
        _mockUserRepository
            .Setup(r => r.Create(It.IsAny<User>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act
        await _service.HandleAsync(request);

        // Assert is handled by ExpectedException
    }

    [TestMethod]
    public async Task HandleAsync_WithSpecialCharactersInUsername_HandlesCorrectly()
    {
        // Arrange
        var request = new CreateUserRequest("user@#$", "test@example.com", "password", null);
        var hashedPassword = "hashed";
        var createdUser = new User
        {
            Id = 11,
            Username = "user@#$",
            Email = "test@example.com",
            Password = hashedPassword,
            MembershipTier = MembershipTierEnum.Basic
        };

        _mockHashService.Setup(h => h.HashPassword(It.IsAny<string>())).ReturnsAsync(hashedPassword);
        _mockUserRepository.Setup(r => r.Create(It.IsAny<User>())).ReturnsAsync(createdUser);

        // Act
        var result = await _service.HandleAsync(request);

        // Assert
        Assert.AreEqual("user@#$", result.Username);
    }

    [TestMethod]
    public async Task HandleAsync_CallsRepositoryCreateOnlyOnce()
    {
        // Arrange
        var request = new CreateUserRequest("testuser", "test@example.com", "password", null);
        var hashedPassword = "hashed";
        var createdUser = new User
        {
            Id = 12,
            Username = "testuser",
            Email = "test@example.com",
            Password = hashedPassword,
            MembershipTier = MembershipTierEnum.Basic
        };

        _mockHashService.Setup(h => h.HashPassword(It.IsAny<string>())).ReturnsAsync(hashedPassword);
        _mockUserRepository.Setup(r => r.Create(It.IsAny<User>())).ReturnsAsync(createdUser);

        // Act
        await _service.HandleAsync(request);

        // Assert
        _mockUserRepository.Verify(r => r.Create(It.IsAny<User>()), Times.Once);
    }
}