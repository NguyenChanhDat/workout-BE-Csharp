using FirstNETWebApp.Core.Repository;
using FirstNETWebApp.UseCase.GetUsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq.Expressions;

namespace FirstNETWebApp.Tests.UseCase.GetUsers;

[TestClass]
public class GetUsersServiceTest
{
    [TestMethod]
    public async Task GetUsersService_ShouldReturnUserSummaries_WhenUsersExist()
    {
        // Arrange
        var mockUserRepository = new Mock<IUserRepository>();
        var expectedUsers = new List<UserSummary>
        {
            new(1, "user1", "user1@example.com", MembershipTierEnum.Basic),
            new(2, "user2", "user2@example.com", MembershipTierEnum.Advance)
        };

        mockUserRepository
            .Setup(r => r.GetAll(It.IsAny<Expression<Func<User, UserSummary>>>()))
            .ReturnsAsync(expectedUsers);

        var service = new GetUsersService(mockUserRepository.Object);
        var request = new GetUsersRequest();

        // Act
        var result = await service.HandleAsync(request);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.Count);
        Assert.AreEqual(expectedUsers[0].Id, result[0].Id);
        Assert.AreEqual(expectedUsers[0].Username, result[0].Username);
        Assert.AreEqual(expectedUsers[0].Email, result[0].Email);
        Assert.AreEqual(expectedUsers[0].MembershipTier, result[0].MembershipTier);
        mockUserRepository.Verify(r => r.GetAll(It.IsAny<Expression<Func<User, UserSummary>>>()), Times.Once());
    }

    [TestMethod]
    public async Task GetUsersService_ShouldReturnEmptyList_WhenNoUsersExist()
    {
        // Arrange
        var mockUserRepository = new Mock<IUserRepository>();
        var expectedUsers = new List<UserSummary>();

        mockUserRepository
            .Setup(r => r.GetAll(It.IsAny<Expression<Func<User, UserSummary>>>()))
            .ReturnsAsync(expectedUsers);

        var service = new GetUsersService(mockUserRepository.Object);
        var request = new GetUsersRequest();

        // Act
        var result = await service.HandleAsync(request);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Count);
        mockUserRepository.Verify(r => r.GetAll(It.IsAny<Expression<Func<User, UserSummary>>>()), Times.Once());
    }
}
