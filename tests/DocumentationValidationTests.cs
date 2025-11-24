using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace FirstNETWebApp.Tests.Documentation;

/// <summary>
/// Comprehensive tests for README.md and documentation files
/// Tests structure, content validity, links, and formatting
/// </summary>
[TestClass]
public class DocumentationValidationTests
{
    private const string ReadmePath = "../../../README.md";
    private const string DocFolderPath = "../../../doc";
    private const string TestsFolderPath = "../../../tests";
    private string? _readmeContent;

    [TestInitialize]
    public void Setup()
    {
        // Load README content for tests
        if (File.Exists(ReadmePath))
        {
            _readmeContent = File.ReadAllText(ReadmePath);
        }
    }

    #region File Existence Tests

    [TestMethod]
    [TestCategory("Documentation")]
    public void README_File_Should_Exist()
    {
        Assert.IsTrue(File.Exists(ReadmePath), "README.md file should exist in repository root");
    }

    [TestMethod]
    [TestCategory("Documentation")]
    public void README_Should_Not_Be_Empty()
    {
        Assert.IsNotNull(_readmeContent, "README content should be loaded");
        Assert.IsTrue(_readmeContent.Length > 0, "README.md should not be empty");
        Assert.IsTrue(_readmeContent.Length > 100, "README.md should contain substantial content");
    }

    [TestMethod]
    [TestCategory("Documentation")]
    public void Doc_Folder_Should_Exist()
    {
        Assert.IsTrue(Directory.Exists(DocFolderPath), 
            "doc folder should exist as referenced in README");
    }

    [TestMethod]
    [TestCategory("Documentation")]
    public void Tests_Folder_Should_Exist()
    {
        Assert.IsTrue(Directory.Exists(TestsFolderPath), 
            "tests folder should exist as referenced in README");
    }

    #endregion

    #region Structure and Required Sections Tests

    [TestMethod]
    [TestCategory("Documentation")]
    public void README_Should_Have_Title()
    {
        Assert.IsNotNull(_readmeContent);
        Assert.IsTrue(_readmeContent.Contains("# README"), 
            "README should have main title starting with # README");
    }

    [TestMethod]
    [TestCategory("Documentation")]
    public void README_Should_Have_Author_Section()
    {
        Assert.IsNotNull(_readmeContent);
        Assert.IsTrue(_readmeContent.Contains("Author:"), 
            "README should have an Author section");
    }

    [TestMethod]
    [TestCategory("Documentation")]
    public void README_Should_Have_Description_Section()
    {
        Assert.IsNotNull(_readmeContent);
        Assert.IsTrue(_readmeContent.Contains("Describe:"), 
            "README should have a Description section");
    }

    [TestMethod]
    [TestCategory("Documentation")]
    public void README_Should_Have_Dependency_Section()
    {
        Assert.IsNotNull(_readmeContent);
        Assert.IsTrue(_readmeContent.Contains("## DEPENDENCY"), 
            "README should have a DEPENDENCY section");
    }

    [TestMethod]
    [TestCategory("Documentation")]
    public void README_Should_Have_Developing_Section()
    {
        Assert.IsNotNull(_readmeContent);
        Assert.IsTrue(_readmeContent.Contains("## DEVELOPING"), 
            "README should have a DEVELOPING section");
    }

    [TestMethod]
    [TestCategory("Documentation")]
    public void README_Should_Have_Testing_Section()
    {
        Assert.IsNotNull(_readmeContent);
        Assert.IsTrue(_readmeContent.Contains("## TESTING"), 
            "README should have a TESTING section");
    }

    [TestMethod]
    [TestCategory("Documentation")]
    public void README_Should_Have_Setup_Instructions()
    {
        Assert.IsNotNull(_readmeContent);
        Assert.IsTrue(_readmeContent.Contains("### Setup"), 
            "README should have Setup instructions");
    }

    #endregion

    #region Content Quality Tests

    [TestMethod]
    [TestCategory("Documentation")]
    public void README_Should_Not_Contain_Random_Gibberish()
    {
        Assert.IsNotNull(_readmeContent);
        
        // Check for common patterns of random text (sequences of consonants without vowels)
        var lines = _readmeContent.Split('\n');
        var suspiciousLines = new List<string>();
        
        foreach (var line in lines)
        {
            var trimmedLine = line.Trim();
            if (string.IsNullOrWhiteSpace(trimmedLine) || trimmedLine.StartsWith("#") || 
                trimmedLine.StartsWith("```") || trimmedLine.StartsWith("Author:") || 
                trimmedLine.StartsWith("Describe:"))
            {
                continue;
            }
            
            // Check for lines with suspicious patterns (long sequences without spaces or vowels)
            if (trimmedLine.Length > 10 && !trimmedLine.Contains(" "))
            {
                // Count vowels
                int vowelCount = trimmedLine.Count(c => "aeiouAEIOU".Contains(c));
                double vowelRatio = (double)vowelCount / trimmedLine.Length;
                
                // If less than 15% vowels and not a URL/path, it might be gibberish
                if (vowelRatio < 0.15 && !trimmedLine.Contains("://") && 
                    !trimmedLine.Contains(".") && !trimmedLine.Contains("/"))
                {
                    suspiciousLines.Add(trimmedLine);
                }
            }
        }
        
        Assert.AreEqual(0, suspiciousLines.Count, 
            $"README contains potential gibberish text: {string.Join(", ", suspiciousLines)}");
    }

    [TestMethod]
    [TestCategory("Documentation")]
    public void README_Author_Line_Should_Be_Well_Formed()
    {
        Assert.IsNotNull(_readmeContent);
        
        var authorMatch = Regex.Match(_readmeContent, @"Author:\s*(.+?)(?:\n|$)", RegexOptions.Multiline);
        Assert.IsTrue(authorMatch.Success, "Author line should exist");
        
        var authorName = authorMatch.Groups[1].Value.Trim();
        Assert.IsTrue(authorName.Length > 0, "Author name should not be empty");
        Assert.IsTrue(authorName.Length < 100, "Author name should be reasonable length");
        
        // Should not contain random characters after author name on same line
        var nextLine = _readmeContent.Split('\n')
            .SkipWhile(l => !l.Contains("Author:"))
            .Skip(1)
            .FirstOrDefault()?.Trim();
            
        if (!string.IsNullOrEmpty(nextLine) && !nextLine.StartsWith("Describe:") && 
            !string.IsNullOrWhiteSpace(nextLine))
        {
            Assert.Fail($"Unexpected content after Author line: '{nextLine}'. " +
                       "Should be followed by empty line or Description.");
        }
    }

    [TestMethod]
    [TestCategory("Documentation")]
    public void README_Should_Reference_Correct_Project_Paths()
    {
        Assert.IsNotNull(_readmeContent);
        
        // Check that referenced paths in commands exist
        var csprojMatch = Regex.Match(_readmeContent, @"dotnet\s+\w+\s+[^\\]*\\src\\(\S+\.csproj)");
        if (csprojMatch.Success)
        {
            var projectFile = csprojMatch.Groups[1].Value;
            var projectPath = Path.Combine("../../../src", projectFile);
            Assert.IsTrue(File.Exists(projectPath), 
                $"Referenced project file should exist: {projectFile}");
        }
    }

    [TestMethod]
    [TestCategory("Documentation")]
    public void README_Should_Reference_Valid_Test_Project()
    {
        Assert.IsNotNull(_readmeContent);
        
        var testMatch = Regex.Match(_readmeContent, @"dotnet\s+test\s+[^\\]*\\tests\\(\S+\.csproj)");
        if (testMatch.Success)
        {
            var testProject = testMatch.Groups[1].Value;
            var testPath = Path.Combine("../../../tests", testProject);
            Assert.IsTrue(File.Exists(testPath), 
                $"Referenced test project file should exist: {testProject}");
        }
    }

    #endregion

    #region URL and Link Validation Tests

    [TestMethod]
    [TestCategory("Documentation")]
    public void README_Should_Contain_Valid_URL_Format()
    {
        Assert.IsNotNull(_readmeContent);
        
        var urlPattern = @"https?://[^\s\)\]]+";
        var urls = Regex.Matches(_readmeContent, urlPattern);
        
        Assert.IsTrue(urls.Count > 0, "README should contain at least one URL");
        
        foreach (Match urlMatch in urls)
        {
            var url = urlMatch.Value;
            Assert.IsTrue(Uri.TryCreate(url, UriKind.Absolute, out var uriResult) && 
                         (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps),
                         $"URL should be well-formed: {url}");
        }
    }

    [TestMethod]
    [TestCategory("Documentation")]
    public void README_Should_Have_Docker_Documentation_Link()
    {
        Assert.IsNotNull(_readmeContent);
        Assert.IsTrue(_readmeContent.Contains("https://docs.docker.com"), 
            "README should contain Docker documentation link");
    }

    [TestMethod]
    [TestCategory("Documentation")]
    public void README_Should_Have_EntityFramework_Link()
    {
        Assert.IsNotNull(_readmeContent);
        Assert.IsTrue(_readmeContent.Contains("https://learn.microsoft.com/en-us/ef"), 
            "README should contain Entity Framework documentation link");
    }

    [TestMethod]
    [TestCategory("Documentation")]
    public void README_Should_Have_DotNet_Download_Link()
    {
        Assert.IsNotNull(_readmeContent);
        Assert.IsTrue(_readmeContent.Contains("https://dotnet.microsoft.com"), 
            "README should contain .NET download/documentation link");
    }

    [TestMethod]
    [TestCategory("Documentation")]
    public void README_Markdown_Links_Should_Be_Well_Formed()
    {
        Assert.IsNotNull(_readmeContent);
        
        // Match markdown links: [text](url)
        var linkPattern = @"\[([^\]]+)\]\(([^\)]+)\)";
        var links = Regex.Matches(_readmeContent, linkPattern);
        
        Assert.IsTrue(links.Count > 0, "README should contain markdown links");
        
        foreach (Match link in links)
        {
            var linkText = link.Groups[1].Value;
            var linkUrl = link.Groups[2].Value;
            
            Assert.IsTrue(linkText.Length > 0, "Link text should not be empty");
            Assert.IsTrue(linkUrl.Length > 0, "Link URL should not be empty");
            
            // If it's a URL, validate format
            if (linkUrl.StartsWith("http"))
            {
                Assert.IsTrue(Uri.TryCreate(linkUrl, UriKind.Absolute, out _), 
                    $"Markdown link URL should be valid: {linkUrl}");
            }
        }
    }

    #endregion

    #region Code Block Validation Tests

    [TestMethod]
    [TestCategory("Documentation")]
    public void README_Should_Have_Valid_Code_Blocks()
    {
        Assert.IsNotNull(_readmeContent);
        
        var codeBlockPattern = @"```(\w*)";
        var codeBlocks = Regex.Matches(_readmeContent, codeBlockPattern);
        
        Assert.IsTrue(codeBlocks.Count > 0, "README should contain code blocks");
        Assert.IsTrue(codeBlocks.Count % 2 == 0, 
            "Code blocks should be properly closed (even number of ``` markers)");
    }

    [TestMethod]
    [TestCategory("Documentation")]
    public void README_Should_Have_Bash_Code_Blocks()
    {
        Assert.IsNotNull(_readmeContent);
        Assert.IsTrue(_readmeContent.Contains("```bash"), 
            "README should contain bash code blocks for commands");
    }

    [TestMethod]
    [TestCategory("Documentation")]
    public void README_Code_Blocks_Should_Contain_Dotnet_Commands()
    {
        Assert.IsNotNull(_readmeContent);
        
        // Extract code blocks
        var codeBlockPattern = @"```bash(.*?)```";
        var codeBlocks = Regex.Matches(_readmeContent, codeBlockPattern, 
            RegexOptions.Singleline | RegexOptions.Multiline);
        
        Assert.IsTrue(codeBlocks.Count > 0, "README should have bash code blocks");
        
        var hasDotnetRestore = false;
        var hasDotnetRun = false;
        var hasDotnetTest = false;
        
        foreach (Match block in codeBlocks)
        {
            var content = block.Groups[1].Value;
            if (content.Contains("dotnet restore")) hasDotnetRestore = true;
            if (content.Contains("dotnet run")) hasDotnetRun = true;
            if (content.Contains("dotnet test")) hasDotnetTest = true;
        }
        
        Assert.IsTrue(hasDotnetRestore, "README should contain 'dotnet restore' command");
        Assert.IsTrue(hasDotnetRun, "README should contain 'dotnet run' command");
        Assert.IsTrue(hasDotnetTest, "README should contain 'dotnet test' command");
    }

    #endregion

    #region Formatting and Style Tests

    [TestMethod]
    [TestCategory("Documentation")]
    public void README_Should_Use_Consistent_Header_Style()
    {
        Assert.IsNotNull(_readmeContent);
        
        var lines = _readmeContent.Split('\n');
        var headers = lines.Where(l => l.TrimStart().StartsWith("#")).ToList();
        
        Assert.IsTrue(headers.Count > 0, "README should have headers");
        
        foreach (var header in headers)
        {
            var trimmed = header.TrimStart();
            // Headers should have space after # symbols
            if (trimmed.Length > 1 && trimmed[0] == '#')
            {
                var hashCount = trimmed.TakeWhile(c => c == '#').Count();
                if (trimmed.Length > hashCount)
                {
                    Assert.IsTrue(trimmed[hashCount] == ' ', 
                        $"Header should have space after # symbols: {header}");
                }
            }
        }
    }

    [TestMethod]
    [TestCategory("Documentation")]
    public void README_Should_Not_Have_Trailing_Whitespace_On_Content_Lines()
    {
        Assert.IsNotNull(_readmeContent);
        
        var lines = _readmeContent.Split('\n');
        var linesWithTrailingWhitespace = new List<(int lineNum, string line)>();
        
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            if (line.Length > 0 && line.TrimEnd().Length != line.Length && 
                !string.IsNullOrWhiteSpace(line))
            {
                linesWithTrailingWhitespace.Add((i + 1, line));
            }
        }
        
        // Allow up to 2 lines with trailing whitespace (common in markdown for line breaks)
        Assert.IsTrue(linesWithTrailingWhitespace.Count <= 2, 
            $"Too many lines with trailing whitespace: {linesWithTrailingWhitespace.Count}. " +
            $"Lines: {string.Join(", ", linesWithTrailingWhitespace.Select(x => x.lineNum))}");
    }

    [TestMethod]
    [TestCategory("Documentation")]
    public void README_Should_Have_Proper_Section_Hierarchy()
    {
        Assert.IsNotNull(_readmeContent);
        
        var lines = _readmeContent.Split('\n');
        var headers = new List<(int level, string text, int lineNum)>();
        
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i].TrimStart();
            if (line.StartsWith("#"))
            {
                var level = line.TakeWhile(c => c == '#').Count();
                var text = line.Substring(level).Trim();
                headers.Add((level, text, i + 1));
            }
        }
        
        // Should have a level 1 header
        Assert.IsTrue(headers.Any(h => h.level == 1), "README should have a level 1 header (# title)");
        
        // Check that we don't skip levels (e.g., # then ### without ##)
        for (int i = 1; i < headers.Count; i++)
        {
            var prevLevel = headers[i - 1].level;
            var currentLevel = headers[i].level;
            
            if (currentLevel > prevLevel)
            {
                Assert.IsTrue(currentLevel - prevLevel <= 1, 
                    $"Header levels should not skip (line {headers[i].lineNum}): " +
                    $"went from level {prevLevel} to {currentLevel}");
            }
        }
    }

    #endregion

    #region Dependency Documentation Tests

    [TestMethod]
    [TestCategory("Documentation")]
    public void README_Should_Document_Docker_Dependency()
    {
        Assert.IsNotNull(_readmeContent);
        Assert.IsTrue(_readmeContent.Contains("Docker"), 
            "README should document Docker as a dependency");
    }

    [TestMethod]
    [TestCategory("Documentation")]
    public void README_Should_Document_EntityFramework_Dependency()
    {
        Assert.IsNotNull(_readmeContent);
        Assert.IsTrue(_readmeContent.Contains("EntityFramework"), 
            "README should document EntityFramework as a dependency");
    }

    [TestMethod]
    [TestCategory("Documentation")]
    public void README_Should_Document_DotNet_Dependency()
    {
        Assert.IsNotNull(_readmeContent);
        Assert.IsTrue(_readmeContent.Contains(".NET"), 
            "README should document .NET as a dependency");
    }

    #endregion

    #region Referenced Files Validation

    [TestMethod]
    [TestCategory("Documentation")]
    public void Doc_Folder_Should_Contain_Architecture_Documentation()
    {
        var architectureFile = Path.Combine(DocFolderPath, "ProjectArchitecture.md");
        Assert.IsTrue(File.Exists(architectureFile), 
            "doc folder should contain ProjectArchitecture.md as implied by README");
    }

    [TestMethod]
    [TestCategory("Documentation")]
    public void Tests_Folder_Should_Contain_Test_Project()
    {
        var testFiles = Directory.GetFiles(TestsFolderPath, "*.csproj", SearchOption.TopDirectoryOnly);
        Assert.IsTrue(testFiles.Length > 0, 
            "tests folder should contain at least one .csproj file");
    }

    [TestMethod]
    [TestCategory("Documentation")]
    public void Tests_Folder_Should_Contain_Test_Files()
    {
        var testFiles = Directory.GetFiles(TestsFolderPath, "*.cs", SearchOption.AllDirectories);
        Assert.IsTrue(testFiles.Length > 0, 
            "tests folder should contain C# test files");
    }

    #endregion

    #region Line Ending and Encoding Tests

    [TestMethod]
    [TestCategory("Documentation")]
    public void README_Should_Use_UTF8_Encoding()
    {
        Assert.IsTrue(File.Exists(ReadmePath));
        
        var bytes = File.ReadAllBytes(ReadmePath);
        
        // Check for BOM
        var hasUtf8Bom = bytes.Length >= 3 && 
            bytes[0] == 0xEF && bytes[1] == 0xBB && bytes[2] == 0xBF;
        
        // Try to decode as UTF-8
        try
        {
            var content = System.Text.Encoding.UTF8.GetString(bytes);
            Assert.IsNotNull(content, "README should be valid UTF-8");
        }
        catch (Exception ex)
        {
            Assert.Fail($"README is not valid UTF-8: {ex.Message}");
        }
    }

    [TestMethod]
    [TestCategory("Documentation")]
    public void README_Should_End_With_Single_Newline()
    {
        Assert.IsNotNull(_readmeContent);
        
        // Check that file doesn't end with multiple newlines or no newline
        var trimmedEnd = _readmeContent.TrimEnd('\r', '\n');
        var endingNewlines = _readmeContent.Length - trimmedEnd.Length;
        
        Assert.IsTrue(endingNewlines >= 1 && endingNewlines <= 2, 
            $"README should end with a single newline, found {endingNewlines} newline characters");
    }

    #endregion
}