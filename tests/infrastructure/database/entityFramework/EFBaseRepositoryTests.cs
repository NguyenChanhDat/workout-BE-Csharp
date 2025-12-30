using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using FirstNETWebApp.Infrastructure.Database.EntityFramework;

namespace FirstNETWebApp.Tests.Infrastructure.Database.EntityFramework;

[TestClass]
public class EFBaseRepositoryTests
{
    private DatabaseContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new DatabaseContext(options);
    }

    [TestMethod]
    public async Task GetAll_WithSelector_ReturnsProjectedResults()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var repository = new EFBaseRepository<User>(context);

        var user1 = new User
        {
            Id = 1,
            Username = "user1",
            Email = "user1@example.com",
            Password = "password1",
            MembershipTier = MembershipTierEnum.Basic
        };
        var user2 = new User
        {
            Id = 2,
            Username = "user2",
            Email = "user2@example.com",
            Password = "password2",
            MembershipTier = MembershipTierEnum.High
        };

        context.Users.AddRange(user1, user2);
        await context.SaveChangesAsync();

        // Act
        var results = await repository.GetAll(u => new
        {
            u.Id,
            u.Username,
            u.Email
        });

        // Assert
        Assert.IsNotNull(results);
        Assert.AreEqual(2, results.Count);
        Assert.AreEqual(1, results[0].Id);
        Assert.AreEqual("user1", results[0].Username);
        Assert.AreEqual("user1@example.com", results[0].Email);
        Assert.AreEqual(2, results[1].Id);
        Assert.AreEqual("user2", results[1].Username);
        Assert.AreEqual("user2@example.com", results[1].Email);
    }

    [TestMethod]
    public async Task GetAll_WithSelector_ProjectsOnlySpecifiedFields()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var repository = new EFBaseRepository<User>(context);

        var user = new User
        {
            Id = 1,
            Username = "testuser",
            Email = "test@example.com",
            Password = "secretpassword",
            MembershipTier = MembershipTierEnum.Basic
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();

        // Act - Project only username
        var results = await repository.GetAll(u => u.Username);

        // Assert
        Assert.IsNotNull(results);
        Assert.AreEqual(1, results.Count);
        Assert.AreEqual("testuser", results[0]);
    }

    [TestMethod]
    public async Task GetAll_WithSelector_ReturnsEmptyListWhenNoEntities()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var repository = new EFBaseRepository<User>(context);

        // Act
        var results = await repository.GetAll(u => new { u.Id, u.Username });

        // Assert
        Assert.IsNotNull(results);
        Assert.AreEqual(0, results.Count);
    }

    [TestMethod]
    public async Task GetAll_WithSelector_VerifyAsNoTrackingBehavior()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var repository = new EFBaseRepository<User>(context);

        var user = new User
        {
            Id = 1,
            Username = "tracktest",
            Email = "track@example.com",
            Password = "password",
            MembershipTier = MembershipTierEnum.Basic
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();

        // Clear change tracker to start with clean state
        context.ChangeTracker.Clear();

        // Act - Get with projection (uses AsNoTracking)
        var projectedResults = await repository.GetAll(u => new { u.Id, u.Username });

        // Verify no entities are tracked after the query with projection
        var trackedEntitiesAfterProjection = context.ChangeTracker.Entries<User>().Count();

        // Assert
        Assert.AreEqual(1, projectedResults.Count);
        Assert.AreEqual("tracktest", projectedResults[0].Username);
        // AsNoTracking should ensure no entities are tracked
        Assert.AreEqual(0, trackedEntitiesAfterProjection,
            "AsNoTracking should prevent entity tracking when using selector");
    }

    [TestMethod]
    public async Task GetAll_WithComplexSelector_ReturnsTransformedData()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var repository = new EFBaseRepository<User>(context);

        var user = new User
        {
            Id = 1,
            Username = "john",
            Email = "john@example.com",
            Password = "password",
            MembershipTier = MembershipTierEnum.High
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();

        // Act - Use complex transformation in selector
        var results = await repository.GetAll(u => new
        {
            DisplayName = u.Username.ToUpper(),
            EmailDomain = u.Email.Substring(u.Email.IndexOf("@") + 1),
            IsHighTier = u.MembershipTier == MembershipTierEnum.High
        });

        // Assert
        Assert.IsNotNull(results);
        Assert.AreEqual(1, results.Count);
        Assert.AreEqual("JOHN", results[0].DisplayName);
        Assert.AreEqual("example.com", results[0].EmailDomain);
        Assert.IsTrue(results[0].IsHighTier);
    }

    [TestMethod]
    public async Task GetAll_WithSelector_WorksWithMultipleEntities()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var repository = new EFBaseRepository<User>(context);

        // Add 10 users
        for (int i = 1; i <= 10; i++)
        {
            context.Users.Add(new User
            {
                Id = i,
                Username = $"user{i}",
                Email = $"user{i}@example.com",
                Password = $"password{i}",
                MembershipTier = i % 2 == 0 ? MembershipTierEnum.High : MembershipTierEnum.Basic
            });
        }
        await context.SaveChangesAsync();

        // Act
        var results = await repository.GetAll(u => new { u.Id, u.MembershipTier });

        // Assert
        Assert.IsNotNull(results);
        Assert.AreEqual(10, results.Count);

        var highTierCount = results.Count(r => r.MembershipTier == MembershipTierEnum.High);
        var basicCount = results.Count(r => r.MembershipTier == MembershipTierEnum.Basic);

        Assert.AreEqual(5, highTierCount);
        Assert.AreEqual(5, basicCount);
    }
}
