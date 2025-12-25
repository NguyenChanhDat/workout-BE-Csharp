using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FirstNETWebApp.Tests.Validation;

/// <summary>
/// Comprehensive validation tests for project structure and critical files.
/// These tests ensure the project maintains its expected structure and
/// all critical files are present for proper operation.
/// </summary>
[TestClass]
public class ProjectStructureValidationTests
{
    [TestMethod]
    [TestCategory("Structure")]
    [Description("Validates that src directory exists")]
    public void SrcDirectory_ShouldExist()
    {
        // Act
        var directoryExists = Directory.Exists("src");

        // Assert
        Assert.IsTrue(directoryExists, "src/ directory should exist containing the main application code");
    }

    [TestMethod]
    [TestCategory("Structure")]
    [Description("Validates that tests directory exists")]
    public void TestsDirectory_ShouldExist()
    {
        // Act
        var directoryExists = Directory.Exists("tests");

        // Assert
        Assert.IsTrue(directoryExists, "tests/ directory should exist containing unit tests");
    }

    [TestMethod]
    [TestCategory("Structure")]
    [Description("Validates that integration-test directory exists")]
    public void IntegrationTestDirectory_ShouldExist()
    {
        // Act
        var directoryExists = Directory.Exists("integration-test");

        // Assert
        Assert.IsTrue(directoryExists, "integration-test/ directory should exist for API integration tests");
    }

    [TestMethod]
    [TestCategory("Structure")]
    [Description("Validates that doc directory exists")]
    public void DocDirectory_ShouldExist()
    {
        // Act
        var directoryExists = Directory.Exists("doc");

        // Assert
        Assert.IsTrue(directoryExists, "doc/ directory should exist for project documentation");
    }

    [TestMethod]
    [TestCategory("Structure")]
    [Description("Validates that .gitignore file exists")]
    public void GitIgnoreFile_ShouldExist()
    {
        // Act
        var fileExists = File.Exists(".gitignore");

        // Assert
        Assert.IsTrue(fileExists, ".gitignore file should exist to exclude unnecessary files from version control");
    }

    [TestMethod]
    [TestCategory("Structure")]
    [Description("Validates that solution file exists")]
    public void SolutionFile_ShouldExist()
    {
        // Arrange
        var solutionFiles = Directory.GetFiles(".", "*.sln", SearchOption.TopDirectoryOnly);

        // Assert
        Assert.IsTrue(solutionFiles.Length > 0, "A .sln solution file should exist in the project root");
    }

    [TestMethod]
    [TestCategory("Structure")]
    [Description("Validates that main project file exists in src directory")]
    public void MainProjectFile_ShouldExistInSrc()
    {
        // Arrange
        var projectFiles = Directory.GetFiles("src", "*.csproj", SearchOption.TopDirectoryOnly);

        // Assert
        Assert.IsTrue(projectFiles.Length > 0, "A .csproj file should exist in the src/ directory");
    }

    [TestMethod]
    [TestCategory("Structure")]
    [Description("Validates that test project file exists in tests directory")]
    public void TestProjectFile_ShouldExistInTests()
    {
        // Arrange
        var projectFiles = Directory.GetFiles("tests", "*.csproj", SearchOption.TopDirectoryOnly);

        // Assert
        Assert.IsTrue(projectFiles.Length > 0, "A .csproj file should exist in the tests/ directory");
    }

    [TestMethod]
    [TestCategory("Structure")]
    [Description("Validates that Program.cs exists in src directory")]
    public void ProgramFile_ShouldExist()
    {
        // Act
        var fileExists = File.Exists("src/Program.cs");

        // Assert
        Assert.IsTrue(fileExists, "src/Program.cs should exist as the application entry point");
    }

    [TestMethod]
    [TestCategory("Structure")]
    [Description("Validates that core directory exists in src")]
    public void CoreDirectory_ShouldExistInSrc()
    {
        // Act
        var directoryExists = Directory.Exists("src/core");

        // Assert
        Assert.IsTrue(directoryExists, "src/core/ directory should exist following Clean Architecture");
    }

    [TestMethod]
    [TestCategory("Structure")]
    [Description("Validates that entities directory exists in core")]
    public void EntitiesDirectory_ShouldExistInCore()
    {
        // Act
        var directoryExists = Directory.Exists("src/core/entities");

        // Assert
        Assert.IsTrue(directoryExists, "src/core/entities/ directory should exist for domain entities");
    }

    [TestMethod]
    [TestCategory("Structure")]
    [Description("Validates that repository directory exists in core")]
    public void RepositoryDirectory_ShouldExistInCore()
    {
        // Act
        var directoryExists = Directory.Exists("src/core/repository");

        // Assert
        Assert.IsTrue(directoryExists, "src/core/repository/ directory should exist for repository interfaces");
    }

    [TestMethod]
    [TestCategory("Structure")]
    [Description("Validates that infrastructure directory exists in src")]
    public void InfrastructureDirectory_ShouldExistInSrc()
    {
        // Act
        var directoryExists = Directory.Exists("src/infrastructure");

        // Assert
        Assert.IsTrue(directoryExists, "src/infrastructure/ directory should exist for infrastructure implementations");
    }

    [TestMethod]
    [TestCategory("Structure")]
    [Description("Validates that .gitignore contains common excludes")]
    public void GitIgnoreFile_ShouldContainCommonExcludes()
    {
        // Arrange
        var content = File.ReadAllText(".gitignore");
        var expectedExcludes = new[] { "bin/", "obj/", ".vs/" };

        // Act & Assert
        foreach (var exclude in expectedExcludes)
        {
            Assert.IsTrue(content.Contains(exclude, StringComparison.OrdinalIgnoreCase),
                $".gitignore should exclude {exclude}");
        }
    }

    [TestMethod]
    [TestCategory("Structure")]
    [Description("Validates that Dockerfile exists for database")]
    public void DockerfileDb_ShouldExist()
    {
        // Act
        var fileExists = File.Exists("Dockerfile.db");

        // Assert
        Assert.IsTrue(fileExists, "Dockerfile.db should exist for database containerization");
    }

    [TestMethod]
    [TestCategory("Structure")]
    [Description("Validates critical directories are not empty")]
    public void CriticalDirectories_ShouldNotBeEmpty()
    {
        // Arrange
        var criticalDirectories = new[] { "src/core/entities", "src/core/repository", "tests/core" };

        // Act & Assert
        foreach (var directory in criticalDirectories)
        {
            if (Directory.Exists(directory))
            {
                var files = Directory.GetFiles(directory, "*.cs", SearchOption.AllDirectories);
                Assert.IsTrue(files.Length > 0, 
                    $"Critical directory {directory} should contain at least one C# file");
            }
        }
    }

    [TestMethod]
    [TestCategory("Structure")]
    [Description("Validates that enums directory exists in core")]
    public void EnumsDirectory_ShouldExistInCore()
    {
        // Act
        var directoryExists = Directory.Exists("src/core/enums");

        // Assert
        Assert.IsTrue(directoryExists, "src/core/enums/ directory should exist for domain enumerations");
    }

    [TestMethod]
    [TestCategory("Structure")]
    [Description("Validates that unit of work pattern is implemented")]
    public void UnitOfWorkDirectory_ShouldExistInCore()
    {
        // Act
        var directoryExists = Directory.Exists("src/core/unitOfWork");

        // Assert
        Assert.IsTrue(directoryExists, "src/core/unitOfWork/ directory should exist for unit of work pattern");
    }

    [TestMethod]
    [TestCategory("Structure")]
    [Description("Validates that database implementations exist")]
    public void DatabaseDirectory_ShouldExistInInfrastructure()
    {
        // Act
        var directoryExists = Directory.Exists("src/infrastructure/database");

        // Assert
        Assert.IsTrue(directoryExists, 
            "src/infrastructure/database/ directory should exist for database implementations");
    }

    [TestMethod]
    [TestCategory("Structure")]
    [Description("Validates that Entity Framework directory exists")]
    public void EntityFrameworkDirectory_ShouldExist()
    {
        // Act
        var directoryExists = Directory.Exists("src/infrastructure/database/entityFramework");

        // Assert
        Assert.IsTrue(directoryExists,
            "src/infrastructure/database/entityFramework/ directory should exist for EF Core implementations");
    }

    [TestMethod]
    [TestCategory("Structure")]
    [Description("Validates that commands directory exists with helper scripts")]
    public void CommandsDirectory_ShouldExist()
    {
        // Act
        var directoryExists = Directory.Exists("commands");

        // Assert
        Assert.IsTrue(directoryExists, "commands/ directory should exist for helper scripts");
    }

    [TestMethod]
    [TestCategory("Structure")]
    [Description("Validates that no environment files are committed")]
    public void EnvironmentFiles_ShouldNotBeCommitted()
    {
        // Arrange
        var environmentFiles = new[] { ".env", ".env.local", ".env.production" };

        // Act & Assert
        foreach (var envFile in environmentFiles)
        {
            Assert.IsFalse(File.Exists(envFile),
                $"Environment file {envFile} should not be committed to version control");
        }
    }

    [TestMethod]
    [TestCategory("Structure")]
    [Description("Validates that Properties directory exists for launch settings")]
    public void PropertiesDirectory_ShouldExist()
    {
        // Act
        var directoryExists = Directory.Exists("Properties");

        // Assert
        Assert.IsTrue(directoryExists, "Properties/ directory should exist for project properties");
    }
}