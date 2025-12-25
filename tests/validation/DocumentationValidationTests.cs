using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace FirstNETWebApp.Tests.Validation;

/// <summary>
/// Comprehensive validation tests for documentation files in the repository.
/// These tests ensure documentation is complete, links are valid, and required
/// sections are present for maintainability and developer onboarding.
/// </summary>
[TestClass]
public class DocumentationValidationTests
{
    private const string ReadmePath = "README.md";
    private const string TodoPath = "ToDo.md";
    private const string DocDirectory = "doc";

    [TestMethod]
    [TestCategory("Documentation")]
    [Description("Validates that README.md exists in the project root")]
    public void ReadmeFile_ShouldExist()
    {
        // Act
        var fileExists = File.Exists(ReadmePath);

        // Assert
        Assert.IsTrue(fileExists, "README.md should exist in the project root for documentation purposes");
    }

    [TestMethod]
    [TestCategory("Documentation")]
    [Description("Validates that README.md is not empty")]
    public void ReadmeFile_ShouldNotBeEmpty()
    {
        // Arrange
        var content = File.ReadAllText(ReadmePath);

        // Assert
        Assert.IsFalse(string.IsNullOrWhiteSpace(content), "README.md should contain meaningful content");
        Assert.IsTrue(content.Length > 100, "README.md should contain substantial documentation (> 100 characters)");
    }

    [TestMethod]
    [TestCategory("Documentation")]
    [Description("Validates that README.md contains required sections")]
    public void ReadmeFile_ShouldContainRequiredSections()
    {
        // Arrange
        var content = File.ReadAllText(ReadmePath);
        var requiredSections = new[] { "DEPENDENCY", "DEVELOPING", "TESTING" };

        // Act & Assert
        foreach (var section in requiredSections)
        {
            Assert.IsTrue(content.Contains($"## {section}", StringComparison.OrdinalIgnoreCase),
                $"README.md should contain a '{section}' section");
        }
    }

    [TestMethod]
    [TestCategory("Documentation")]
    [Description("Validates that README.md contains Setup instructions")]
    public void ReadmeFile_ShouldContainSetupInstructions()
    {
        // Arrange
        var content = File.ReadAllText(ReadmePath);

        // Act
        var hasSetup = content.Contains("### Setup", StringComparison.OrdinalIgnoreCase);
        var hasDotnetCommand = content.Contains("dotnet restore", StringComparison.OrdinalIgnoreCase);

        // Assert
        Assert.IsTrue(hasSetup, "README.md should contain Setup instructions");
        Assert.IsTrue(hasDotnetCommand, "Setup section should include dotnet restore command");
    }

    [TestMethod]
    [TestCategory("Documentation")]
    [Description("Validates that README.md contains Testing instructions")]
    public void ReadmeFile_ShouldContainTestingInstructions()
    {
        // Arrange
        var content = File.ReadAllText(ReadmePath);

        // Act
        var hasTestingSection = content.Contains("## TESTING", StringComparison.OrdinalIgnoreCase);
        var hasTestCommand = content.Contains("dotnet test", StringComparison.OrdinalIgnoreCase);

        // Assert
        Assert.IsTrue(hasTestingSection, "README.md should contain a TESTING section");
        Assert.IsTrue(hasTestCommand, "Testing section should include dotnet test command");
    }

    [TestMethod]
    [TestCategory("Documentation")]
    [Description("Validates that README.md markdown links are properly formatted")]
    public void ReadmeFile_MarkdownLinks_ShouldBeProperlyFormatted()
    {
        // Arrange
        var content = File.ReadAllText(ReadmePath);
        var linkPattern = @"\[([^\]]+)\]\(([^\)]+)\)";
        var matches = Regex.Matches(content, linkPattern);

        // Assert
        Assert.IsTrue(matches.Count > 0, "README.md should contain at least one markdown link");

        foreach (Match match in matches)
        {
            var linkText = match.Groups[1].Value;
            var linkUrl = match.Groups[2].Value;

            Assert.IsFalse(string.IsNullOrWhiteSpace(linkText), 
                $"Link text should not be empty for URL: {linkUrl}");
            Assert.IsFalse(string.IsNullOrWhiteSpace(linkUrl),
                $"Link URL should not be empty for text: {linkText}");
        }
    }

    [TestMethod]
    [TestCategory("Documentation")]
    [Description("Validates that README.md external links use HTTPS")]
    public void ReadmeFile_ExternalLinks_ShouldUseHttps()
    {
        // Arrange
        var content = File.ReadAllText(ReadmePath);
        var linkPattern = @"\[([^\]]+)\]\((https?://[^\)]+)\)";
        var matches = Regex.Matches(content, linkPattern);

        // Act & Assert
        foreach (Match match in matches)
        {
            var linkUrl = match.Groups[2].Value;
            Assert.IsTrue(linkUrl.StartsWith("https://", StringComparison.OrdinalIgnoreCase),
                $"External link should use HTTPS: {linkUrl}");
        }
    }

    [TestMethod]
    [TestCategory("Documentation")]
    [Description("Validates that README.md contains author information")]
    public void ReadmeFile_ShouldContainAuthorInformation()
    {
        // Arrange
        var content = File.ReadAllText(ReadmePath);

        // Act
        var hasAuthor = content.Contains("Author:", StringComparison.OrdinalIgnoreCase);

        // Assert
        Assert.IsTrue(hasAuthor, "README.md should contain author information");
    }

    [TestMethod]
    [TestCategory("Documentation")]
    [Description("Validates that README.md contains project description")]
    public void ReadmeFile_ShouldContainProjectDescription()
    {
        // Arrange
        var content = File.ReadAllText(ReadmePath);

        // Act
        var hasDescription = content.Contains("Describe:", StringComparison.OrdinalIgnoreCase);

        // Assert
        Assert.IsTrue(hasDescription, "README.md should contain a project description");
    }

    [TestMethod]
    [TestCategory("Documentation")]
    [Description("Validates that doc directory exists")]
    public void DocDirectory_ShouldExist()
    {
        // Act
        var directoryExists = Directory.Exists(DocDirectory);

        // Assert
        Assert.IsTrue(directoryExists, "doc/ directory should exist for additional documentation");
    }

    [TestMethod]
    [TestCategory("Documentation")]
    [Description("Validates that doc directory contains markdown files")]
    public void DocDirectory_ShouldContainMarkdownFiles()
    {
        // Arrange & Act
        var mdFiles = Directory.GetFiles(DocDirectory, "*.md", SearchOption.TopDirectoryOnly);

        // Assert
        Assert.IsTrue(mdFiles.Length > 0, "doc/ directory should contain at least one markdown file");
    }

    [TestMethod]
    [TestCategory("Documentation")]
    [Description("Validates that ProjectArchitecture.md exists")]
    public void ProjectArchitectureDoc_ShouldExist()
    {
        // Arrange
        var archDocPath = Path.Combine(DocDirectory, "ProjectArchitecture.md");

        // Act
        var fileExists = File.Exists(archDocPath);

        // Assert
        Assert.IsTrue(fileExists, "doc/ProjectArchitecture.md should exist to document project structure");
    }

    [TestMethod]
    [TestCategory("Documentation")]
    [Description("Validates that ProjectArchitecture.md mentions Clean Architecture")]
    public void ProjectArchitectureDoc_ShouldMentionCleanArchitecture()
    {
        // Arrange
        var archDocPath = Path.Combine(DocDirectory, "ProjectArchitecture.md");
        var content = File.ReadAllText(archDocPath);

        // Act
        var mentionsCleanArch = content.Contains("Clean Architecture", StringComparison.OrdinalIgnoreCase);

        // Assert
        Assert.IsTrue(mentionsCleanArch, 
            "ProjectArchitecture.md should mention Clean Architecture pattern");
    }

    [TestMethod]
    [TestCategory("Documentation")]
    [Description("Validates that rule.md exists with coding rules")]
    public void RulesDoc_ShouldExist()
    {
        // Arrange
        var rulesDocPath = Path.Combine(DocDirectory, "rule.md");

        // Act
        var fileExists = File.Exists(rulesDocPath);

        // Assert
        Assert.IsTrue(fileExists, "doc/rule.md should exist to document coding rules");
    }

    [TestMethod]
    [TestCategory("Documentation")]
    [Description("Validates that rule.md is not empty")]
    public void RulesDoc_ShouldNotBeEmpty()
    {
        // Arrange
        var rulesDocPath = Path.Combine(DocDirectory, "rule.md");
        var content = File.ReadAllText(rulesDocPath);

        // Assert
        Assert.IsFalse(string.IsNullOrWhiteSpace(content), 
            "doc/rule.md should contain coding rules and principles");
    }

    [TestMethod]
    [TestCategory("Documentation")]
    [Description("Validates that all markdown files use consistent heading style")]
    public void AllMarkdownFiles_ShouldUseConsistentHeadingStyle()
    {
        // Arrange
        var allMdFiles = new List<string> { ReadmePath };
        allMdFiles.AddRange(Directory.GetFiles(DocDirectory, "*.md", SearchOption.AllDirectories));

        // Act & Assert
        foreach (var mdFile in allMdFiles)
        {
            var content = File.ReadAllText(mdFile);
            var hasAtxHeadings = Regex.IsMatch(content, @"^#{1,6}\s+.+$", RegexOptions.Multiline);

            Assert.IsTrue(hasAtxHeadings || content.Length < 50, 
                $"Markdown file {mdFile} should use ATX-style headings (# heading)");
        }
    }

    [TestMethod]
    [TestCategory("Documentation")]
    [Description("Validates that documentation files have reasonable sizes")]
    public void DocumentationFiles_ShouldHaveReasonableSizes()
    {
        // Arrange
        const long maxFileSize = 500 * 1024; // 500 KB
        var allMdFiles = new List<string> { ReadmePath };
        if (File.Exists(TodoPath))
        {
            allMdFiles.Add(TodoPath);
        }
        allMdFiles.AddRange(Directory.GetFiles(DocDirectory, "*.md", SearchOption.AllDirectories));

        // Act & Assert
        foreach (var mdFile in allMdFiles)
        {
            var fileInfo = new FileInfo(mdFile);
            Assert.IsTrue(fileInfo.Length < maxFileSize,
                $"Documentation file {mdFile} size ({fileInfo.Length} bytes) should be less than {maxFileSize} bytes");
        }
    }

    [TestMethod]
    [TestCategory("Documentation")]
    [Description("Validates that code blocks in README have language identifiers")]
    public void ReadmeFile_CodeBlocks_ShouldHaveLanguageIdentifiers()
    {
        // Arrange
        var content = File.ReadAllText(ReadmePath);
        var codeBlockPattern = @"```(\w*)";
        var matches = Regex.Matches(content, codeBlockPattern);

        // Act & Assert
        var codeBlockCount = matches.Count / 2; // Opening and closing backticks
        Assert.IsTrue(codeBlockCount > 0, "README.md should contain code examples");

        foreach (Match match in matches)
        {
            if (match.Index > 0 && content[match.Index - 1] != '`') // Opening block
            {
                var language = match.Groups[1].Value;
                Assert.IsFalse(string.IsNullOrWhiteSpace(language),
                    "Code blocks should specify a language identifier for syntax highlighting");
            }
        }
    }

    [TestMethod]
    [TestCategory("Documentation")]
    [Description("Validates that documentation doesn't contain TODO markers")]
    public void DocumentationFiles_ShouldNotContainUnresolvedTodos()
    {
        // Arrange
        var allMdFiles = new List<string> { ReadmePath };
        allMdFiles.AddRange(Directory.GetFiles(DocDirectory, "*.md", SearchOption.AllDirectories));
        var todoPatterns = new[] { "TODO:", "FIXME:", "XXX:" };

        // Act & Assert
        foreach (var mdFile in allMdFiles)
        {
            var content = File.ReadAllText(mdFile);
            foreach (var pattern in todoPatterns)
            {
                Assert.IsFalse(content.Contains(pattern, StringComparison.OrdinalIgnoreCase),
                    $"Documentation file {mdFile} should not contain unresolved {pattern} markers");
            }
        }
    }

    [TestMethod]
    [TestCategory("Documentation")]
    [Description("Validates that README contains dependency information")]
    public void ReadmeFile_ShouldListDependencies()
    {
        // Arrange
        var content = File.ReadAllText(ReadmePath);
        var expectedDependencies = new[] { "Docker", "EntityFramework", ".NET" };

        // Act & Assert
        foreach (var dependency in expectedDependencies)
        {
            Assert.IsTrue(content.Contains(dependency, StringComparison.OrdinalIgnoreCase),
                $"README.md should list {dependency} as a dependency");
        }
    }

    [TestMethod]
    [TestCategory("Documentation")]
    [Description("Validates that markdown files use UTF-8 encoding")]
    public void DocumentationFiles_ShouldUseUtf8Encoding()
    {
        // Arrange
        var allMdFiles = new List<string> { ReadmePath };
        allMdFiles.AddRange(Directory.GetFiles(DocDirectory, "*.md", SearchOption.AllDirectories));

        // Act & Assert
        foreach (var mdFile in allMdFiles)
        {
            var content = File.ReadAllText(mdFile);
            Assert.IsNotNull(content, $"Should be able to read {mdFile} as UTF-8");
            Assert.IsFalse(content.StartsWith("\ufeff"), 
                $"Documentation file {mdFile} should not contain UTF-8 BOM");
        }
    }
}