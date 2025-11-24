using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

[TestClass]
public class BaseRepoTest
{
    [TestMethod]
    public async Task TestMethod()
    {
        var mockUserRepository = new Mock<IBaseRepository<User>>();
        mockUserRepository.Setup(r => r.GetAll()).ReturnsAsync(new List<User>
        {
            new() { Id = 1, Username = "Mock User", Password = "password" },
            new() { Id = 2, Username = "Mock User 2", Password = "password" }
        });

        var users = await mockUserRepository.Object.GetAll();

        Assert.IsNotNull(users);
        Assert.AreEqual(2, users.Count());
    }

    [TestMethod]
    public async Task GetAll_ReturnsEmptyList_WhenNoUsers()
    {
        var mockUserRepository = new Mock<IBaseRepository<User>>();
        mockUserRepository.Setup(r => r.GetAll()).ReturnsAsync(new List<User>());

        var users = await mockUserRepository.Object.GetAll();

        Assert.IsNotNull(users);
        Assert.AreEqual(0, users.Count());
    }

    [TestMethod]
    public async Task GetOneById_ReturnsUser_WhenIdExists()
    {
        var mockUserRepository = new Mock<IBaseRepository<User>>();
        var expectedUser = new User { Id = 1, Username = "Test User", Email = "test@example.com", Password = "hashed" };
        mockUserRepository.Setup(r => r.GetOneById(1)).ReturnsAsync(expectedUser);

        var user = await mockUserRepository.Object.GetOneById(1);

        Assert.IsNotNull(user);
        Assert.AreEqual(1, user.Id);
        Assert.AreEqual("Test User", user.Username);
    }

    [TestMethod]
    public async Task GetOneById_ReturnsNull_WhenIdDoesNotExist()
    {
        var mockUserRepository = new Mock<IBaseRepository<User>>();
        mockUserRepository.Setup(r => r.GetOneById(999)).ReturnsAsync((User?)null);

        var user = await mockUserRepository.Object.GetOneById(999);

        Assert.IsNull(user);
    }

    [TestMethod]
    public async Task Create_ReturnsCreatedUser()
    {
        var mockUserRepository = new Mock<IBaseRepository<User>>();
        var newUser = new User { Username = "NewUser", Email = "new@example.com", Password = "hashed" };
        var createdUser = new User { Id = 1, Username = "NewUser", Email = "new@example.com", Password = "hashed" };
        
        mockUserRepository.Setup(r => r.Create(newUser)).ReturnsAsync(createdUser);

        var result = await mockUserRepository.Object.Create(newUser);

        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Id);
        Assert.AreEqual("NewUser", result.Username);
    }

    [TestMethod]
    public async Task Update_CallsRepositoryUpdate()
    {
        var mockUserRepository = new Mock<IBaseRepository<User>>();
        var user = new User { Id = 1, Username = "UpdatedUser", Email = "updated@example.com", Password = "hashed" };
        
        mockUserRepository.Setup(r => r.Update(user)).Returns(Task.CompletedTask);

        await mockUserRepository.Object.Update(user);

        mockUserRepository.Verify(r => r.Update(user), Times.Once);
    }

    [TestMethod]
    public async Task Delete_ReturnsDeletedUser_WhenIdExists()
    {
        var mockUserRepository = new Mock<IBaseRepository<User>>();
        var deletedUser = new User { Id = 1, Username = "Deleted User", Email = "deleted@example.com", Password = "hashed" };
        mockUserRepository.Setup(r => r.Delete(1)).ReturnsAsync(deletedUser);

        var result = await mockUserRepository.Object.Delete(1);

        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Id);
        Assert.AreEqual("Deleted User", result.Username);
    }

    [TestMethod]
    public async Task Delete_ReturnsNull_WhenIdDoesNotExist()
    {
        var mockUserRepository = new Mock<IBaseRepository<User>>();
        mockUserRepository.Setup(r => r.Delete(999)).ReturnsAsync((User?)null);

        var result = await mockUserRepository.Object.Delete(999);

        Assert.IsNull(result);
    }
}
