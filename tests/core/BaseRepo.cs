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
            new User { Id = 1, Username = "Mock User", Password = "password" },
            new User { Id = 2, Username = "Mock User 2", Password = "password" }
        });

        var users = await mockUserRepository.Object.GetAll();

        Assert.IsNotNull(users);
        Assert.AreEqual(2, users.Count());
    }
}
