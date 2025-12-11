using FirstNETWebApp.UseCase.Base.Interfaces;
using FirstNETWebApp.UseCase.CreateUser.Dtos;
using FirstNETWebApp.UseCase.Decorators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
namespace FirstNETWebApp.Tests.UseCase.Base.Decorators;

[TestClass]
public class UnitOfWorkDecorator
{

    [TestMethod]
    public async Task TestMethod()
    {
        // Arrange
        var validator = new Mock<IValidator<CreateUserRequest>>();
        var unitOfWork = new Mock<IUnitOfWork>();
        var innerMutationUseCase = new Mock<IMutationUseCase<CreateUserRequest, CreateUserResponse>>();

        var request = new CreateUserRequest("a", "a@a.com", "123", null);
        var expectedResponse = new CreateUserResponse(1, "a", "a@a.com", MembershipTierEnum.Basic);

        // Arrange (sequence-based setups)
        var sequence = new MockSequence();

        validator.InSequence(sequence)
                 .Setup(v => v.CheaplyValidateAsync(request))
                 .Returns(Task.CompletedTask);

        bool uowCalled = false;
        unitOfWork.InSequence(sequence)
                 .Setup(u => u.ExecuteAsync(It.IsAny<Func<Task<CreateUserResponse>>>()))
                 .Callback<Func<Task<CreateUserResponse>>>(async (func) =>
                 {
                     uowCalled = true;
                     await func(); // execute innerMutationUseCase use case
                 })
                 .Returns(Task.FromResult(expectedResponse));

        innerMutationUseCase.InSequence(sequence)
             .Setup(i => i.ExecuteAsync(request))
             .ReturnsAsync(expectedResponse);

        var decorator = new UnitOfWorkDecorator<CreateUserRequest, CreateUserResponse>(
            innerMutationUseCase.Object,
            unitOfWork.Object,
            validator.Object
        );

        // Act
        var result = await decorator.ExecuteAsync(request);

        // Assert
        Assert.IsTrue(uowCalled);

        // Verify each expected call was invoked once
        validator.Verify(v => v.CheaplyValidateAsync(request), Times.Once());
        unitOfWork.Verify(u => u.ExecuteAsync(It.IsAny<Func<Task<CreateUserResponse>>>()), Times.Once());
        innerMutationUseCase.Verify(i => i.ExecuteAsync(request), Times.Once());

    }

}