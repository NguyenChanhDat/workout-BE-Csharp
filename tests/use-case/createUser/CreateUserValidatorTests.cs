using FirstNETWebApp.UseCase;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FirstNETWebApp.Tests.UseCase.CreateUser;

[TestClass]
public class CreateUserValidatorTests
{
    private CreateUserValidator _validator = null!;

    [TestInitialize]
    public void Setup()
    {
        _validator = new CreateUserValidator();
    }

    [TestMethod]
    public async Task CheaplyValidateAsync_WithValidRequest_DoesNotThrow()
    {
        // Arrange
        var request = new CreateUserRequest("testuser", "test@example.com", "password123", null);

        // Act & Assert
        await _validator.CheaplyValidateAsync(request);
        // If no exception is thrown, test passes
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task CheaplyValidateAsync_WithNullUsername_ThrowsArgumentException()
    {
        // Arrange
        var request = new CreateUserRequest(null!, "test@example.com", "password123", null);

        // Act
        await _validator.CheaplyValidateAsync(request);

        // Assert is handled by ExpectedException
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task CheaplyValidateAsync_WithEmptyUsername_ThrowsArgumentException()
    {
        // Arrange
        var request = new CreateUserRequest("", "test@example.com", "password123", null);

        // Act
        await _validator.CheaplyValidateAsync(request);

        // Assert is handled by ExpectedException
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task CheaplyValidateAsync_WithWhitespaceUsername_ThrowsArgumentException()
    {
        // Arrange
        var request = new CreateUserRequest("   ", "test@example.com", "password123", null);

        // Act
        await _validator.CheaplyValidateAsync(request);

        // Assert is handled by ExpectedException
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task CheaplyValidateAsync_WithNullPassword_ThrowsArgumentException()
    {
        // Arrange
        var request = new CreateUserRequest("testuser", "test@example.com", null!, null);

        // Act
        await _validator.CheaplyValidateAsync(request);

        // Assert is handled by ExpectedException
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task CheaplyValidateAsync_WithEmptyPassword_ThrowsArgumentException()
    {
        // Arrange
        var request = new CreateUserRequest("testuser", "test@example.com", "", null);

        // Act
        await _validator.CheaplyValidateAsync(request);

        // Assert is handled by ExpectedException
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task CheaplyValidateAsync_WithWhitespacePassword_ThrowsArgumentException()
    {
        // Arrange
        var request = new CreateUserRequest("testuser", "test@example.com", "   ", null);

        // Act
        await _validator.CheaplyValidateAsync(request);

        // Assert is handled by ExpectedException
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task CheaplyValidateAsync_WithNullEmail_ThrowsArgumentException()
    {
        // Arrange
        var request = new CreateUserRequest("testuser", null!, "password123", null);

        // Act
        await _validator.CheaplyValidateAsync(request);

        // Assert is handled by ExpectedException
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task CheaplyValidateAsync_WithEmptyEmail_ThrowsArgumentException()
    {
        // Arrange
        var request = new CreateUserRequest("testuser", "", "password123", null);

        // Act
        await _validator.CheaplyValidateAsync(request);

        // Assert is handled by ExpectedException
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task CheaplyValidateAsync_WithWhitespaceEmail_ThrowsArgumentException()
    {
        // Arrange
        var request = new CreateUserRequest("testuser", "   ", "password123", null);

        // Act
        await _validator.CheaplyValidateAsync(request);

        // Assert is handled by ExpectedException
    }

    [TestMethod]
    public async Task CheaplyValidateAsync_WithValidInputsAndNullMembershipTier_DoesNotThrow()
    {
        // Arrange
        var request = new CreateUserRequest("validuser", "valid@email.com", "validPassword", null);

        // Act & Assert
        await _validator.CheaplyValidateAsync(request);
    }

    [TestMethod]
    public async Task CheaplyValidateAsync_WithValidInputsAndBasicTier_DoesNotThrow()
    {
        // Arrange
        var request = new CreateUserRequest("validuser", "valid@email.com", "validPassword", MembershipTierEnum.Basic);

        // Act & Assert
        await _validator.CheaplyValidateAsync(request);
    }

    [TestMethod]
    public async Task CheaplyValidateAsync_WithValidInputsAndAdvanceTier_DoesNotThrow()
    {
        // Arrange
        var request = new CreateUserRequest("validuser", "valid@email.com", "validPassword", MembershipTierEnum.Advance);

        // Act & Assert
        await _validator.CheaplyValidateAsync(request);
    }

    [TestMethod]
    public async Task CheaplyValidateAsync_WithValidInputsAndHighTier_DoesNotThrow()
    {
        // Arrange
        var request = new CreateUserRequest("validuser", "valid@email.com", "validPassword", MembershipTierEnum.High);

        // Act & Assert
        await _validator.CheaplyValidateAsync(request);
    }

    [TestMethod]
    public async Task CheaplyValidateAsync_WithSpecialCharactersInUsername_DoesNotThrow()
    {
        // Arrange
        var request = new CreateUserRequest("user@#$%", "test@example.com", "password123", null);

        // Act & Assert
        await _validator.CheaplyValidateAsync(request);
    }

    [TestMethod]
    public async Task CheaplyValidateAsync_WithLongUsername_DoesNotThrow()
    {
        // Arrange
        var longUsername = new string('a', 1000);
        var request = new CreateUserRequest(longUsername, "test@example.com", "password123", null);

        // Act & Assert
        await _validator.CheaplyValidateAsync(request);
    }

    [TestMethod]
    public async Task CheaplyValidateAsync_WithLongEmail_DoesNotThrow()
    {
        // Arrange
        var longEmail = new string('a', 1000) + "@example.com";
        var request = new CreateUserRequest("testuser", longEmail, "password123", null);

        // Act & Assert
        await _validator.CheaplyValidateAsync(request);
    }

    [TestMethod]
    public async Task CheaplyValidateAsync_WithLongPassword_DoesNotThrow()
    {
        // Arrange
        var longPassword = new string('a', 1000);
        var request = new CreateUserRequest("testuser", "test@example.com", longPassword, null);

        // Act & Assert
        await _validator.CheaplyValidateAsync(request);
    }

    [TestMethod]
    public async Task CheaplyValidateAsync_WithUnicodeCharacters_DoesNotThrow()
    {
        // Arrange
        var request = new CreateUserRequest("用户名", "测试@example.com", "密码123", null);

        // Act & Assert
        await _validator.CheaplyValidateAsync(request);
    }

    [TestMethod]
    public async Task CheaplyValidateAsync_WithMinimalValidInput_DoesNotThrow()
    {
        // Arrange
        var request = new CreateUserRequest("a", "a@a.a", "p", null);

        // Act & Assert
        await _validator.CheaplyValidateAsync(request);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task CheaplyValidateAsync_WithTabCharactersInUsername_ThrowsArgumentException()
    {
        // Arrange
        var request = new CreateUserRequest("\t\t", "test@example.com", "password123", null);

        // Act
        await _validator.CheaplyValidateAsync(request);

        // Assert is handled by ExpectedException
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task CheaplyValidateAsync_WithNewlineCharactersInPassword_ThrowsArgumentException()
    {
        // Arrange
        var request = new CreateUserRequest("testuser", "test@example.com", "\n\n", null);

        // Act
        await _validator.CheaplyValidateAsync(request);

        // Assert is handled by ExpectedException
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task CheaplyValidateAsync_WithMixedWhitespaceInEmail_ThrowsArgumentException()
    {
        // Arrange
        var request = new CreateUserRequest("testuser", " \t\n ", "password123", null);

        // Act
        await _validator.CheaplyValidateAsync(request);

        // Assert is handled by ExpectedException
    }

    [TestMethod]
    public async Task CheaplyValidateAsync_ThrowsExceptionWithUsernameMessage()
    {
        // Arrange
        var request = new CreateUserRequest("", "test@example.com", "password123", null);

        // Act & Assert
        try
        {
            await _validator.CheaplyValidateAsync(request);
            Assert.Fail("Expected ArgumentException was not thrown");
        }
        catch (ArgumentException ex)
        {
            Assert.IsTrue(ex.Message.Contains("Username"), "Exception message should mention 'Username'");
        }
    }

    [TestMethod]
    public async Task CheaplyValidateAsync_ThrowsExceptionWithPasswordMessage()
    {
        // Arrange
        var request = new CreateUserRequest("testuser", "test@example.com", "", null);

        // Act & Assert
        try
        {
            await _validator.CheaplyValidateAsync(request);
            Assert.Fail("Expected ArgumentException was not thrown");
        }
        catch (ArgumentException ex)
        {
            Assert.IsTrue(ex.Message.Contains("Password"), "Exception message should mention 'Password'");
        }
    }

    [TestMethod]
    public async Task CheaplyValidateAsync_ThrowsExceptionWithEmailMessage()
    {
        // Arrange
        var request = new CreateUserRequest("testuser", "", "password123", null);

        // Act & Assert
        try
        {
            await _validator.CheaplyValidateAsync(request);
            Assert.Fail("Expected ArgumentException was not thrown");
        }
        catch (ArgumentException ex)
        {
            Assert.IsTrue(ex.Message.Contains("Email"), "Exception message should mention 'Email'");
        }
    }
}