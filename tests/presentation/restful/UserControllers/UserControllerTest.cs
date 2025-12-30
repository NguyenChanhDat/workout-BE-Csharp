using FirstNETWebApp.presentation.restful.UserControllers;
using FirstNETWebApp.UseCase.Base.Interfaces;
using FirstNETWebApp.UseCase.CreateUser.Dtos;
using FirstNETWebApp.UseCase.GetUsers.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FirstNETWebApp.Tests.Presentation.Restful.UserControllers;

[TestClass]
public class UserControllerTest
{
    [TestMethod]
    public async Task GetUsers_ShouldReturnUserSummaries_WhenUsersExist()
    {
        // Arrange
        var mockCreateUserUseCase = new Mock<IMutationUseCase<CreateUserRequest, User>>();
        var mockGetUsersUseCase = new Mock<IQueryUseCase<GetUsersRequest, List<UserSummary>>>();
        
        var expectedUsers = new List<UserSummary>
        {
            new(1, "user1", "user1@example.com", MembershipTierEnum.Basic),
            new(2, "user2", "user2@example.com", MembershipTierEnum.Advance)
        };

        mockGetUsersUseCase
            .Setup(u => u.ExecuteAsync(It.IsAny<GetUsersRequest>()))
            .ReturnsAsync(expectedUsers);

        var controller = new UserController(
            mockCreateUserUseCase.Object,
            mockGetUsersUseCase.Object
        );

        // Act
        var result = await controller.GetUsers();

        // Assert
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(2, result.Value.Count());
        
        var userList = result.Value.ToList();
        Assert.AreEqual(expectedUsers[0].Id, userList[0].Id);
        Assert.AreEqual(expectedUsers[0].Username, userList[0].Username);
        Assert.AreEqual(expectedUsers[0].Email, userList[0].Email);
        Assert.AreEqual(expectedUsers[0].MembershipTier, userList[0].MembershipTier);
        
        mockGetUsersUseCase.Verify(u => u.ExecuteAsync(It.IsAny<GetUsersRequest>()), Times.Once());
    }

    [TestMethod]
    public async Task GetUsers_ShouldReturnEmptyList_WhenNoUsersExist()
    {
        // Arrange
        var mockCreateUserUseCase = new Mock<IMutationUseCase<CreateUserRequest, User>>();
        var mockGetUsersUseCase = new Mock<IQueryUseCase<GetUsersRequest, List<UserSummary>>>();
        
        var expectedUsers = new List<UserSummary>();

        mockGetUsersUseCase
            .Setup(u => u.ExecuteAsync(It.IsAny<GetUsersRequest>()))
            .ReturnsAsync(expectedUsers);

        var controller = new UserController(
            mockCreateUserUseCase.Object,
            mockGetUsersUseCase.Object
        );

        // Act
        var result = await controller.GetUsers();

        // Assert
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(0, result.Value.Count());
        
        mockGetUsersUseCase.Verify(u => u.ExecuteAsync(It.IsAny<GetUsersRequest>()), Times.Once());
    }
}
