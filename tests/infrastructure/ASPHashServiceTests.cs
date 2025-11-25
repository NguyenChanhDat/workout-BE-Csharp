using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FirstNETWebApp.Tests.Infrastructure
{
    [TestClass]
    public class ASPHashServiceTests
    {
        private ASPHashService _hashService;

        [TestInitialize]
        public void Setup()
        {
            _hashService = new ASPHashService();
        }

        [TestMethod]
        public async Task HashPassword_WithValidPassword_ReturnsHashedString()
        {
            // Arrange
            var password = "MySecurePassword123!";

            // Act
            var hashedPassword = await _hashService.HashPassword(password);

            // Assert
            Assert.IsNotNull(hashedPassword);
            Assert.IsFalse(string.IsNullOrWhiteSpace(hashedPassword));
            Assert.AreNotEqual(password, hashedPassword);
        }

        [TestMethod]
        public async Task HashPassword_SamePasswordTwice_ProducesDifferentHashes()
        {
            // Arrange
            var password = "MySecurePassword123!";

            // Act
            var hash1 = await _hashService.HashPassword(password);
            var hash2 = await _hashService.HashPassword(password);

            // Assert
            Assert.AreNotEqual(hash1, hash2, "Same password should produce different hashes due to salt");
        }

        [TestMethod]
        public async Task VerifyPassword_WithCorrectPassword_ReturnsTrue()
        {
            // Arrange
            var password = "MySecurePassword123!";
            var hashedPassword = await _hashService.HashPassword(password);

            // Act
            var result = await _hashService.VerifyPassword(hashedPassword, password);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task VerifyPassword_WithIncorrectPassword_ReturnsFalse()
        {
            // Arrange
            var correctPassword = "MySecurePassword123!";
            var incorrectPassword = "WrongPassword";
            var hashedPassword = await _hashService.HashPassword(correctPassword);

            // Act
            var result = await _hashService.VerifyPassword(hashedPassword, incorrectPassword);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task VerifyPassword_WithEmptyPassword_ReturnsFalse()
        {
            // Arrange
            var password = "MySecurePassword123!";
            var hashedPassword = await _hashService.HashPassword(password);

            // Act
            var result = await _hashService.VerifyPassword(hashedPassword, string.Empty);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task HashPassword_WithEmptyPassword_ReturnsHash()
        {
            // Arrange
            var password = string.Empty;

            // Act
            var hashedPassword = await _hashService.HashPassword(password);

            // Assert
            Assert.IsNotNull(hashedPassword);
            Assert.IsFalse(string.IsNullOrWhiteSpace(hashedPassword));
        }

        [TestMethod]
        public async Task HashPassword_WithSpecialCharacters_ReturnsHash()
        {
            // Arrange
            var password = "P@ssw0rd!#$%^&*()";

            // Act
            var hashedPassword = await _hashService.HashPassword(password);

            // Assert
            Assert.IsNotNull(hashedPassword);
            var isValid = await _hashService.VerifyPassword(hashedPassword, password);
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public async Task HashPassword_WithUnicodeCharacters_ReturnsHash()
        {
            // Arrange
            var password = "密码测试🔐";

            // Act
            var hashedPassword = await _hashService.HashPassword(password);

            // Assert
            Assert.IsNotNull(hashedPassword);
            var isValid = await _hashService.VerifyPassword(hashedPassword, password);
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public async Task VerifyPassword_WithSlightlyDifferentPassword_ReturnsFalse()
        {
            // Arrange
            var password = "Password123";
            var similarPassword = "Password124"; // One character different
            var hashedPassword = await _hashService.HashPassword(password);

            // Act
            var result = await _hashService.VerifyPassword(hashedPassword, similarPassword);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task HashPassword_WithLongPassword_ReturnsHash()
        {
            // Arrange
            var password = new string('a', 1000);

            // Act
            var hashedPassword = await _hashService.HashPassword(password);

            // Assert
            Assert.IsNotNull(hashedPassword);
            var isValid = await _hashService.VerifyPassword(hashedPassword, password);
            Assert.IsTrue(isValid);
        }
    }
}