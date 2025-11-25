using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FirstNETWebApp.Tests.Dtos
{
    [TestClass]
    public class CreateUserRequestTests
    {
        [TestMethod]
        public void CreateUserRequest_WithAllProperties_CreatesInstance()
        {
            // Arrange & Act
            var request = new CreateUserRequest(
                "testuser",
                "test@example.com",
                "password123",
                MembershipTierEnum.Advance
            );

            // Assert
            Assert.AreEqual("testuser", request.Username);
            Assert.AreEqual("test@example.com", request.Email);
            Assert.AreEqual("password123", request.Password);
            Assert.AreEqual(MembershipTierEnum.Advance, request.MembershipTier);
        }

        [TestMethod]
        public void CreateUserRequest_WithNullMembershipTier_AllowsNull()
        {
            // Arrange & Act
            var request = new CreateUserRequest(
                "testuser",
                "test@example.com",
                "password123",
                null
            );

            // Assert
            Assert.IsNull(request.MembershipTier);
        }

        [TestMethod]
        public void CreateUserRequest_RecordEquality_WorksCorrectly()
        {
            // Arrange
            var request1 = new CreateUserRequest("user", "user@test.com", "pass", MembershipTierEnum.Basic);
            var request2 = new CreateUserRequest("user", "user@test.com", "pass", MembershipTierEnum.Basic);
            var request3 = new CreateUserRequest("user2", "user@test.com", "pass", MembershipTierEnum.Basic);

            // Act & Assert
            Assert.AreEqual(request1, request2);
            Assert.AreNotEqual(request1, request3);
        }

        [TestMethod]
        public void CreateUserRequest_Deconstruction_WorksCorrectly()
        {
            // Arrange
            var request = new CreateUserRequest("testuser", "test@example.com", "password123", MembershipTierEnum.High);

            // Act
            var (username, email, password, membershipTier) = request;

            // Assert
            Assert.AreEqual("testuser", username);
            Assert.AreEqual("test@example.com", email);
            Assert.AreEqual("password123", password);
            Assert.AreEqual(MembershipTierEnum.High, membershipTier);
        }
    }

    [TestClass]
    public class CreateUserResponseTests
    {
        [TestMethod]
        public void CreateUserResponse_WithAllProperties_CreatesInstance()
        {
            // Arrange & Act
            var response = new CreateUserResponse(
                1,
                "testuser",
                "test@example.com",
                MembershipTierEnum.Advance
            );

            // Assert
            Assert.AreEqual(1, response.Id);
            Assert.AreEqual("testuser", response.Username);
            Assert.AreEqual("test@example.com", response.Email);
            Assert.AreEqual(MembershipTierEnum.Advance, response.MembershipTier);
        }

        [TestMethod]
        public void CreateUserResponse_RecordEquality_WorksCorrectly()
        {
            // Arrange
            var response1 = new CreateUserResponse(1, "user", "user@test.com", MembershipTierEnum.Basic);
            var response2 = new CreateUserResponse(1, "user", "user@test.com", MembershipTierEnum.Basic);
            var response3 = new CreateUserResponse(2, "user", "user@test.com", MembershipTierEnum.Basic);

            // Act & Assert
            Assert.AreEqual(response1, response2);
            Assert.AreNotEqual(response1, response3);
        }

        [TestMethod]
        public void CreateUserResponse_Deconstruction_WorksCorrectly()
        {
            // Arrange
            var response = new CreateUserResponse(42, "john_doe", "john@example.com", MembershipTierEnum.High);

            // Act
            var (id, username, email, membershipTier) = response;

            // Assert
            Assert.AreEqual(42, id);
            Assert.AreEqual("john_doe", username);
            Assert.AreEqual("john@example.com", email);
            Assert.AreEqual(MembershipTierEnum.High, membershipTier);
        }

        [TestMethod]
        public void CreateUserResponse_WithDifferentMembershipTiers_CreatesCorrectInstances()
        {
            // Arrange & Act
            var basicResponse = new CreateUserResponse(1, "basic", "basic@test.com", MembershipTierEnum.Basic);
            var advanceResponse = new CreateUserResponse(2, "advance", "advance@test.com", MembershipTierEnum.Advance);
            var highResponse = new CreateUserResponse(3, "high", "high@test.com", MembershipTierEnum.High);

            // Assert
            Assert.AreEqual(MembershipTierEnum.Basic, basicResponse.MembershipTier);
            Assert.AreEqual(MembershipTierEnum.Advance, advanceResponse.MembershipTier);
            Assert.AreEqual(MembershipTierEnum.High, highResponse.MembershipTier);
        }
    }
}