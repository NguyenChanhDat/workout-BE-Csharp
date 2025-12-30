using FirstNETWebApp.UseCase.GetUsers;
using FirstNETWebApp.UseCase.GetUsers.Dtos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FirstNETWebApp.Tests.UseCase.GetUsers;

[TestClass]
public class GetUsersUseCaseTest
{
    [TestMethod]
    public async Task GetUsersUseCase_ShouldCallService_AndReturnResult()
    {
        // Arrange
        var mockService = new Mock<IGetUsersService>();
        var expectedUsers = new List<UserSummary>
        {
            new(1, "user1", "user1@example.com", MembershipTierEnum.Basic),
            new(2, "user2", "user2@example.com", MembershipTierEnum.Advance)
        };

        var request = new GetUsersRequest();

        mockService
            .Setup(s => s.HandleAsync(request))
            .ReturnsAsync(expectedUsers);

        var useCase = new GetUsersUseCase(mockService.Object);

        // Act
        var result = await useCase.ExecuteAsync(request);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.Count);
        Assert.AreEqual(expectedUsers[0].Id, result[0].Id);
        Assert.AreEqual(expectedUsers[0].Username, result[0].Username);
        mockService.Verify(s => s.HandleAsync(request), Times.Once());
    }

    [TestMethod]
    public async Task GetUsersUseCase_ShouldReturnEmptyList_WhenServiceReturnsEmpty()
    {
        // Arrange
        var mockService = new Mock<IGetUsersService>();
        var expectedUsers = new List<UserSummary>();
        var request = new GetUsersRequest();

        mockService
            .Setup(s => s.HandleAsync(request))
            .ReturnsAsync(expectedUsers);

        var useCase = new GetUsersUseCase(mockService.Object);

        // Act
        var result = await useCase.ExecuteAsync(request);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Count);
        mockService.Verify(s => s.HandleAsync(request), Times.Once());
    }
}
