using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FirstNETWebApp.Tests.Entities
{
    [TestClass]
    public class UserEntityTests
    {
        [TestMethod]
        public void User_DefaultConstructor_InitializesCollections()
        {
            // Arrange & Act
            var user = new User();

            // Assert
            Assert.IsNotNull(user.BodyTracks);
            Assert.IsNotNull(user.Plans);
            Assert.AreEqual(0, user.BodyTracks.Count);
            Assert.AreEqual(0, user.Plans.Count);
        }

        [TestMethod]
        public void User_SetProperties_PropertiesAreSet()
        {
            // Arrange
            var user = new User();

            // Act
            user.Id = 1;
            user.Username = "testuser";
            user.Email = "test@example.com";
            user.Password = "hashedpassword";
            user.MembershipTier = MembershipTierEnum.Advance;

            // Assert
            Assert.AreEqual(1, user.Id);
            Assert.AreEqual("testuser", user.Username);
            Assert.AreEqual("test@example.com", user.Email);
            Assert.AreEqual("hashedpassword", user.Password);
            Assert.AreEqual(MembershipTierEnum.Advance, user.MembershipTier);
        }

        [TestMethod]
        public void User_DefaultMembershipTier_IsBasic()
        {
            // Arrange & Act
            var user = new User();

            // Assert
            Assert.AreEqual(MembershipTierEnum.Basic, user.MembershipTier);
        }

        [TestMethod]
        public void User_AddBodyTrack_CollectionContainsItem()
        {
            // Arrange
            var user = new User { Id = 1 };
            var bodyTrack = new BodyTrack { Id = 1, UserId = 1, Weight = 75, Height = 180, Date = DateOnly.FromDateTime(DateTime.Now) };

            // Act
            user.BodyTracks.Add(bodyTrack);

            // Assert
            Assert.AreEqual(1, user.BodyTracks.Count);
            Assert.IsTrue(user.BodyTracks.Contains(bodyTrack));
        }

        [TestMethod]
        public void User_AddPlan_CollectionContainsItem()
        {
            // Arrange
            var user = new User { Id = 1 };
            var plan = new Plan { Id = 1, UserId = 1, Name = "Beginner Plan", MembershipTier = "Basic" };

            // Act
            user.Plans.Add(plan);

            // Assert
            Assert.AreEqual(1, user.Plans.Count);
            Assert.IsTrue(user.Plans.Contains(plan));
        }
    }

    [TestClass]
    public class ExerciseEntityTests
    {
        [TestMethod]
        public void Exercise_DefaultConstructor_InitializesCollections()
        {
            // Arrange & Act
            var exercise = new Exercise();

            // Assert
            Assert.IsNotNull(exercise.Sets);
            Assert.AreEqual(0, exercise.Sets.Count);
        }

        [TestMethod]
        public void Exercise_SetProperties_PropertiesAreSet()
        {
            // Arrange
            var exercise = new Exercise();

            // Act
            exercise.Id = 1;
            exercise.Name = "Bench Press";
            exercise.ImageUrl = "https://example.com/bench-press.jpg";
            exercise.TargetMuscle1 = MuscleEnum.Chest;
            exercise.TargetMuscle2 = MuscleEnum.Triceps;
            exercise.TargetMuscle3 = MuscleEnum.Shoulders;

            // Assert
            Assert.AreEqual(1, exercise.Id);
            Assert.AreEqual("Bench Press", exercise.Name);
            Assert.AreEqual("https://example.com/bench-press.jpg", exercise.ImageUrl);
            Assert.AreEqual(MuscleEnum.Chest, exercise.TargetMuscle1);
            Assert.AreEqual(MuscleEnum.Triceps, exercise.TargetMuscle2);
            Assert.AreEqual(MuscleEnum.Shoulders, exercise.TargetMuscle3);
        }

        [TestMethod]
        public void Exercise_WithNullableTargetMuscles_AllowsNullValues()
        {
            // Arrange & Act
            var exercise = new Exercise
            {
                Id = 1,
                Name = "Cardio",
                TargetMuscle1 = null,
                TargetMuscle2 = null,
                TargetMuscle3 = null
            };

            // Assert
            Assert.IsNull(exercise.TargetMuscle1);
            Assert.IsNull(exercise.TargetMuscle2);
            Assert.IsNull(exercise.TargetMuscle3);
        }

        [TestMethod]
        public void Exercise_AddSet_CollectionContainsItem()
        {
            // Arrange
            var exercise = new Exercise { Id = 1, Name = "Squat" };
            var set = new Set { Id = 1, ExerciseId = 1, SessionId = 1, Weight = 100, Reps = 10, RestTime = 60 };

            // Act
            exercise.Sets.Add(set);

            // Assert
            Assert.AreEqual(1, exercise.Sets.Count);
            Assert.IsTrue(exercise.Sets.Contains(set));
        }
    }

    [TestClass]
    public class BodyTrackEntityTests
    {
        [TestMethod]
        public void BodyTrack_SetProperties_PropertiesAreSet()
        {
            // Arrange
            var bodyTrack = new BodyTrack();
            var date = DateOnly.FromDateTime(DateTime.Now);

            // Act
            bodyTrack.Id = 1;
            bodyTrack.Weight = 75;
            bodyTrack.Height = 180;
            bodyTrack.Date = date;
            bodyTrack.UserId = 10;

            // Assert
            Assert.AreEqual(1, bodyTrack.Id);
            Assert.AreEqual(75, bodyTrack.Weight);
            Assert.AreEqual(180, bodyTrack.Height);
            Assert.AreEqual(date, bodyTrack.Date);
            Assert.AreEqual(10, bodyTrack.UserId);
        }

        [TestMethod]
        public void BodyTrack_WithZeroWeight_AllowsZeroValue()
        {
            // Arrange & Act
            var bodyTrack = new BodyTrack { Weight = 0 };

            // Assert
            Assert.AreEqual(0, bodyTrack.Weight);
        }

        [TestMethod]
        public void BodyTrack_WithZeroHeight_AllowsZeroValue()
        {
            // Arrange & Act
            var bodyTrack = new BodyTrack { Height = 0 };

            // Assert
            Assert.AreEqual(0, bodyTrack.Height);
        }
    }

    [TestClass]
    public class PlanEntityTests
    {
        [TestMethod]
        public void Plan_DefaultConstructor_InitializesCollections()
        {
            // Arrange & Act
            var plan = new Plan();

            // Assert
            Assert.IsNotNull(plan.Sessions);
            Assert.AreEqual(0, plan.Sessions.Count);
        }

        [TestMethod]
        public void Plan_SetProperties_PropertiesAreSet()
        {
            // Arrange
            var plan = new Plan();

            // Act
            plan.Id = 1;
            plan.UserId = 5;
            plan.Name = "Advanced Strength Training";
            plan.MembershipTier = "High";

            // Assert
            Assert.AreEqual(1, plan.Id);
            Assert.AreEqual(5, plan.UserId);
            Assert.AreEqual("Advanced Strength Training", plan.Name);
            Assert.AreEqual("High", plan.MembershipTier);
        }

        [TestMethod]
        public void Plan_AddSession_CollectionContainsItem()
        {
            // Arrange
            var plan = new Plan { Id = 1 };
            var session = new Session { Id = 1, PlanId = 1, Date = DateOnly.FromDateTime(DateTime.Now) };

            // Act
            plan.Sessions.Add(session);

            // Assert
            Assert.AreEqual(1, plan.Sessions.Count);
            Assert.IsTrue(plan.Sessions.Contains(session));
        }
    }

    [TestClass]
    public class SessionEntityTests
    {
        [TestMethod]
        public void Session_DefaultConstructor_InitializesCollections()
        {
            // Arrange & Act
            var session = new Session();

            // Assert
            Assert.IsNotNull(session.Sets);
            Assert.AreEqual(0, session.Sets.Count);
        }

        [TestMethod]
        public void Session_SetProperties_PropertiesAreSet()
        {
            // Arrange
            var session = new Session();
            var date = DateOnly.FromDateTime(DateTime.Now);

            // Act
            session.Id = 1;
            session.Date = date;
            session.PlanId = 5;

            // Assert
            Assert.AreEqual(1, session.Id);
            Assert.AreEqual(date, session.Date);
            Assert.AreEqual(5, session.PlanId);
        }

        [TestMethod]
        public void Session_WithNullPlanId_AllowsNullValue()
        {
            // Arrange & Act
            var session = new Session
            {
                Id = 1,
                Date = DateOnly.FromDateTime(DateTime.Now),
                PlanId = null
            };

            // Assert
            Assert.IsNull(session.PlanId);
            Assert.IsNull(session.Plan);
        }

        [TestMethod]
        public void Session_AddSet_CollectionContainsItem()
        {
            // Arrange
            var session = new Session { Id = 1 };
            var set = new Set { Id = 1, SessionId = 1, ExerciseId = 1, Weight = 50, Reps = 12, RestTime = 90 };

            // Act
            session.Sets.Add(set);

            // Assert
            Assert.AreEqual(1, session.Sets.Count);
            Assert.IsTrue(session.Sets.Contains(set));
        }
    }

    [TestClass]
    public class SetEntityTests
    {
        [TestMethod]
        public void Set_SetProperties_PropertiesAreSet()
        {
            // Arrange
            var set = new Set();

            // Act
            set.Id = 1;
            set.SessionId = 10;
            set.ExerciseId = 5;
            set.Weight = 100;
            set.Reps = 8;
            set.RestTime = 120;

            // Assert
            Assert.AreEqual(1, set.Id);
            Assert.AreEqual(10, set.SessionId);
            Assert.AreEqual(5, set.ExerciseId);
            Assert.AreEqual(100, set.Weight);
            Assert.AreEqual(8, set.Reps);
            Assert.AreEqual(120, set.RestTime);
        }

        [TestMethod]
        public void Set_WithZeroWeight_AllowsZeroValue()
        {
            // Arrange & Act
            var set = new Set { Weight = 0 };

            // Assert
            Assert.AreEqual(0, set.Weight);
        }

        [TestMethod]
        public void Set_WithZeroReps_AllowsZeroValue()
        {
            // Arrange & Act
            var set = new Set { Reps = 0 };

            // Assert
            Assert.AreEqual(0, set.Reps);
        }

        [TestMethod]
        public void Set_WithZeroRestTime_AllowsZeroValue()
        {
            // Arrange & Act
            var set = new Set { RestTime = 0 };

            // Assert
            Assert.AreEqual(0, set.RestTime);
        }
    }
}