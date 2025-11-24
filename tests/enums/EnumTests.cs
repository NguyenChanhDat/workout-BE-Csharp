using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FirstNETWebApp.Tests.Enums
{
    [TestClass]
    public class MembershipTierEnumTests
    {
        [TestMethod]
        public void MembershipTierEnum_HasBasicValue()
        {
            // Arrange & Act
            var tier = MembershipTierEnum.Basic;

            // Assert
            Assert.AreEqual("Basic", tier.ToString());
        }

        [TestMethod]
        public void MembershipTierEnum_HasAdvanceValue()
        {
            // Arrange & Act
            var tier = MembershipTierEnum.Advance;

            // Assert
            Assert.AreEqual("Advance", tier.ToString());
        }

        [TestMethod]
        public void MembershipTierEnum_HasHighValue()
        {
            // Arrange & Act
            var tier = MembershipTierEnum.High;

            // Assert
            Assert.AreEqual("High", tier.ToString());
        }

        [TestMethod]
        public void MembershipTierEnum_ParseFromString_ReturnsCorrectValue()
        {
            // Arrange & Act
            var basic = Enum.Parse<MembershipTierEnum>("Basic");
            var advance = Enum.Parse<MembershipTierEnum>("Advance");
            var high = Enum.Parse<MembershipTierEnum>("High");

            // Assert
            Assert.AreEqual(MembershipTierEnum.Basic, basic);
            Assert.AreEqual(MembershipTierEnum.Advance, advance);
            Assert.AreEqual(MembershipTierEnum.High, high);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MembershipTierEnum_ParseInvalidString_ThrowsException()
        {
            // Arrange & Act & Assert
            Enum.Parse<MembershipTierEnum>("InvalidTier");
        }
    }

    [TestClass]
    public class MuscleEnumTests
    {
        [TestMethod]
        public void MuscleEnum_HasAllExpectedValues()
        {
            // Arrange
            var expectedValues = new[]
            {
                MuscleEnum.Quads,
                MuscleEnum.Hamstring,
                MuscleEnum.Calves,
                MuscleEnum.Glutes,
                MuscleEnum.Back,
                MuscleEnum.Chest,
                MuscleEnum.Shoulders,
                MuscleEnum.Triceps,
                MuscleEnum.Biceps,
                MuscleEnum.Traps
            };

            // Act
            var allValues = Enum.GetValues<MuscleEnum>();

            // Assert
            Assert.AreEqual(expectedValues.Length, allValues.Length);
            foreach (var expected in expectedValues)
            {
                Assert.IsTrue(allValues.Contains(expected));
            }
        }

        [TestMethod]
        public void MuscleEnum_ParseFromString_ReturnsCorrectValue()
        {
            // Arrange & Act
            var chest = Enum.Parse<MuscleEnum>("Chest");
            var back = Enum.Parse<MuscleEnum>("Back");
            var biceps = Enum.Parse<MuscleEnum>("Biceps");

            // Assert
            Assert.AreEqual(MuscleEnum.Chest, chest);
            Assert.AreEqual(MuscleEnum.Back, back);
            Assert.AreEqual(MuscleEnum.Biceps, biceps);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MuscleEnum_ParseInvalidString_ThrowsException()
        {
            // Arrange & Act & Assert
            Enum.Parse<MuscleEnum>("InvalidMuscle");
        }
    }

    [TestClass]
    public class RoleEnumTests
    {
        [TestMethod]
        public void RoleEnum_HasAdminValue()
        {
            // Arrange & Act
            var role = RoleEnum.Admin;

            // Assert
            Assert.AreEqual("Admin", role.ToString());
        }

        [TestMethod]
        public void RoleEnum_HasUserValue()
        {
            // Arrange & Act
            var role = RoleEnum.User;

            // Assert
            Assert.AreEqual("User", role.ToString());
        }

        [TestMethod]
        public void RoleEnum_HasGuestValue()
        {
            // Arrange & Act
            var role = RoleEnum.Guest;

            // Assert
            Assert.AreEqual("Guest", role.ToString());
        }

        [TestMethod]
        public void RoleEnum_ParseFromString_ReturnsCorrectValue()
        {
            // Arrange & Act
            var admin = Enum.Parse<RoleEnum>("Admin");
            var user = Enum.Parse<RoleEnum>("User");
            var guest = Enum.Parse<RoleEnum>("Guest");

            // Assert
            Assert.AreEqual(RoleEnum.Admin, admin);
            Assert.AreEqual(RoleEnum.User, user);
            Assert.AreEqual(RoleEnum.Guest, guest);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RoleEnum_ParseInvalidString_ThrowsException()
        {
            // Arrange & Act & Assert
            Enum.Parse<RoleEnum>("InvalidRole");
        }
    }
}