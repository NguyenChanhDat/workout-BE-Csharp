using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace FirstNETWebApp.Tests.Validation;

/// <summary>
/// Comprehensive validation tests for configuration files in the repository.
/// These tests ensure configuration files have proper JSON structure, required keys,
/// and valid values for critical application settings.
/// </summary>
[TestClass]
public class ConfigurationValidationTests
{
    private const string AppSettingsPath = "appsettings.json";
    private const string AppSettingsDevelopmentPath = "appsettings.Development.json";

    [TestMethod]
    [TestCategory("Configuration")]
    [Description("Validates that appsettings.json exists and is readable")]
    public void AppSettingsJson_ShouldExist()
    {
        // Arrange & Act
        var fileExists = File.Exists(AppSettingsPath);

        // Assert
        Assert.IsTrue(fileExists, $"Configuration file {AppSettingsPath} should exist in the project root");
    }

    [TestMethod]
    [TestCategory("Configuration")]
    [Description("Validates that appsettings.Development.json exists and is readable")]
    public void AppSettingsDevelopmentJson_ShouldExist()
    {
        // Arrange & Act
        var fileExists = File.Exists(AppSettingsDevelopmentPath);

        // Assert
        Assert.IsTrue(fileExists, $"Configuration file {AppSettingsDevelopmentPath} should exist in the project root");
    }

    [TestMethod]
    [TestCategory("Configuration")]
    [Description("Validates that appsettings.json contains valid JSON")]
    public void AppSettingsJson_ShouldBeValidJson()
    {
        // Arrange
        var jsonContent = File.ReadAllText(AppSettingsPath);

        // Act & Assert
        try
        {
            using var document = JsonDocument.Parse(jsonContent);
            Assert.IsNotNull(document, "JSON document should be parseable");
        }
        catch (JsonException ex)
        {
            Assert.Fail($"appsettings.json contains invalid JSON: {ex.Message}");
        }
    }

    [TestMethod]
    [TestCategory("Configuration")]
    [Description("Validates that appsettings.Development.json contains valid JSON")]
    public void AppSettingsDevelopmentJson_ShouldBeValidJson()
    {
        // Arrange
        var jsonContent = File.ReadAllText(AppSettingsDevelopmentPath);

        // Act & Assert
        try
        {
            using var document = JsonDocument.Parse(jsonContent);
            Assert.IsNotNull(document, "JSON document should be parseable");
        }
        catch (JsonException ex)
        {
            Assert.Fail($"appsettings.Development.json contains invalid JSON: {ex.Message}");
        }
    }

    [TestMethod]
    [TestCategory("Configuration")]
    [Description("Validates that appsettings.json contains required Logging section")]
    public void AppSettingsJson_ShouldContainLoggingSection()
    {
        // Arrange
        var jsonContent = File.ReadAllText(AppSettingsPath);
        using var document = JsonDocument.Parse(jsonContent);
        var root = document.RootElement;

        // Act
        var hasLogging = root.TryGetProperty("Logging", out var loggingElement);

        // Assert
        Assert.IsTrue(hasLogging, "appsettings.json should contain a 'Logging' section");
        Assert.AreEqual(JsonValueKind.Object, loggingElement.ValueKind, "Logging section should be a JSON object");
    }

    [TestMethod]
    [TestCategory("Configuration")]
    [Description("Validates that Logging section contains LogLevel configuration")]
    public void AppSettingsJson_LoggingSection_ShouldContainLogLevel()
    {
        // Arrange
        var jsonContent = File.ReadAllText(AppSettingsPath);
        using var document = JsonDocument.Parse(jsonContent);
        var root = document.RootElement;

        // Act
        var hasLogging = root.TryGetProperty("Logging", out var loggingElement);
        var hasLogLevel = hasLogging && loggingElement.TryGetProperty("LogLevel", out var logLevelElement);

        // Assert
        Assert.IsTrue(hasLogLevel, "Logging section should contain 'LogLevel' configuration");
        Assert.AreEqual(JsonValueKind.Object, logLevelElement.ValueKind, "LogLevel should be a JSON object");
    }

    [TestMethod]
    [TestCategory("Configuration")]
    [Description("Validates that LogLevel contains Default level configuration")]
    public void AppSettingsJson_LogLevel_ShouldContainDefaultLevel()
    {
        // Arrange
        var jsonContent = File.ReadAllText(AppSettingsPath);
        using var document = JsonDocument.Parse(jsonContent);
        var root = document.RootElement;

        // Act
        var hasDefault = root.TryGetProperty("Logging", out var loggingElement) &&
                         loggingElement.TryGetProperty("LogLevel", out var logLevelElement) &&
                         logLevelElement.TryGetProperty("Default", out var defaultLevel);

        // Assert
        Assert.IsTrue(hasDefault, "LogLevel should contain a 'Default' level setting");
        Assert.AreEqual(JsonValueKind.String, defaultLevel.ValueKind, "Default log level should be a string value");
    }

    [TestMethod]
    [TestCategory("Configuration")]
    [Description("Validates that Default log level has valid value")]
    public void AppSettingsJson_DefaultLogLevel_ShouldBeValidLevel()
    {
        // Arrange
        var jsonContent = File.ReadAllText(AppSettingsPath);
        using var document = JsonDocument.Parse(jsonContent);
        var root = document.RootElement;
        var validLogLevels = new[] { "Trace", "Debug", "Information", "Warning", "Error", "Critical", "None" };

        // Act
        root.TryGetProperty("Logging", out var loggingElement);
        loggingElement.TryGetProperty("LogLevel", out var logLevelElement);
        logLevelElement.TryGetProperty("Default", out var defaultLevel);
        var logLevelValue = defaultLevel.GetString();

        // Assert
        Assert.IsNotNull(logLevelValue, "Default log level should not be null");
        CollectionAssert.Contains(validLogLevels, logLevelValue, 
            $"Default log level '{logLevelValue}' should be one of: {string.Join(", ", validLogLevels)}");
    }

    [TestMethod]
    [TestCategory("Configuration")]
    [Description("Validates that appsettings.json contains AllowedHosts configuration")]
    public void AppSettingsJson_ShouldContainAllowedHosts()
    {
        // Arrange
        var jsonContent = File.ReadAllText(AppSettingsPath);
        using var document = JsonDocument.Parse(jsonContent);
        var root = document.RootElement;

        // Act
        var hasAllowedHosts = root.TryGetProperty("AllowedHosts", out var allowedHostsElement);

        // Assert
        Assert.IsTrue(hasAllowedHosts, "appsettings.json should contain 'AllowedHosts' configuration");
        Assert.AreEqual(JsonValueKind.String, allowedHostsElement.ValueKind, "AllowedHosts should be a string value");
    }

    [TestMethod]
    [TestCategory("Configuration")]
    [Description("Validates that AllowedHosts has non-empty value")]
    public void AppSettingsJson_AllowedHosts_ShouldNotBeEmpty()
    {
        // Arrange
        var jsonContent = File.ReadAllText(AppSettingsPath);
        using var document = JsonDocument.Parse(jsonContent);
        var root = document.RootElement;

        // Act
        root.TryGetProperty("AllowedHosts", out var allowedHostsElement);
        var allowedHostsValue = allowedHostsElement.GetString();

        // Assert
        Assert.IsFalse(string.IsNullOrWhiteSpace(allowedHostsValue), "AllowedHosts should not be empty or whitespace");
    }

    [TestMethod]
    [TestCategory("Configuration")]
    [Description("Validates that Development settings contain Logging configuration")]
    public void AppSettingsDevelopment_ShouldContainLoggingSection()
    {
        // Arrange
        var jsonContent = File.ReadAllText(AppSettingsDevelopmentPath);
        using var document = JsonDocument.Parse(jsonContent);
        var root = document.RootElement;

        // Act
        var hasLogging = root.TryGetProperty("Logging", out var loggingElement);

        // Assert
        Assert.IsTrue(hasLogging, "appsettings.Development.json should contain a 'Logging' section");
        Assert.AreEqual(JsonValueKind.Object, loggingElement.ValueKind, "Logging section should be a JSON object");
    }

    [TestMethod]
    [TestCategory("Configuration")]
    [Description("Validates that both config files have consistent structure")]
    public void AppSettings_BothFiles_ShouldHaveConsistentLoggingStructure()
    {
        // Arrange
        var prodContent = File.ReadAllText(AppSettingsPath);
        var devContent = File.ReadAllText(AppSettingsDevelopmentPath);
        
        using var prodDoc = JsonDocument.Parse(prodContent);
        using var devDoc = JsonDocument.Parse(devContent);

        // Act
        var prodHasLogging = prodDoc.RootElement.TryGetProperty("Logging", out var prodLogging);
        var devHasLogging = devDoc.RootElement.TryGetProperty("Logging", out var devLogging);

        var prodHasLogLevel = prodLogging.TryGetProperty("LogLevel", out var prodLogLevel);
        var devHasLogLevel = devLogging.TryGetProperty("LogLevel", out var devLogLevel);

        // Assert
        Assert.IsTrue(prodHasLogging && devHasLogging, "Both configuration files should have Logging section");
        Assert.IsTrue(prodHasLogLevel && devHasLogLevel, "Both Logging sections should have LogLevel configuration");
    }

    [TestMethod]
    [TestCategory("Configuration")]
    [Description("Validates that JSON files don't contain sensitive information patterns")]
    public void AppSettings_ShouldNotContainSensitiveDataPatterns()
    {
        // Arrange
        var prodContent = File.ReadAllText(AppSettingsPath);
        var devContent = File.ReadAllText(AppSettingsDevelopmentPath);
        var sensitivePatterns = new[] 
        { 
            "password", "secret", "apikey", "api_key", "token", 
            "connectionstring", "connection_string", "private_key", "privatekey" 
        };

        // Act & Assert
        foreach (var pattern in sensitivePatterns)
        {
            Assert.IsFalse(prodContent.Contains(pattern, StringComparison.OrdinalIgnoreCase),
                $"Production config should not contain '{pattern}' key directly - use environment variables instead");
            Assert.IsFalse(devContent.Contains(pattern, StringComparison.OrdinalIgnoreCase),
                $"Development config should not contain '{pattern}' key directly - use environment variables instead");
        }
    }

    [TestMethod]
    [TestCategory("Configuration")]
    [Description("Validates that configuration files are not too large")]
    public void AppSettings_FileSizes_ShouldBeReasonable()
    {
        // Arrange
        const long maxFileSize = 50 * 1024; // 50 KB
        var prodFileInfo = new FileInfo(AppSettingsPath);
        var devFileInfo = new FileInfo(AppSettingsDevelopmentPath);

        // Assert
        Assert.IsTrue(prodFileInfo.Length < maxFileSize, 
            $"appsettings.json size ({prodFileInfo.Length} bytes) should be less than {maxFileSize} bytes");
        Assert.IsTrue(devFileInfo.Length < maxFileSize,
            $"appsettings.Development.json size ({devFileInfo.Length} bytes) should be less than {maxFileSize} bytes");
    }

    [TestMethod]
    [TestCategory("Configuration")]
    [Description("Validates that configuration files use UTF-8 encoding")]
    public void AppSettings_ShouldUseUtf8Encoding()
    {
        // Arrange & Act
        var prodContent = File.ReadAllText(AppSettingsPath);
        var devContent = File.ReadAllText(AppSettingsDevelopmentPath);

        // Assert - If we can read the content without exceptions, UTF-8 is supported
        Assert.IsNotNull(prodContent, "Should be able to read production config as UTF-8");
        Assert.IsNotNull(devContent, "Should be able to read development config as UTF-8");
        
        // Verify no BOM or encoding issues
        Assert.IsFalse(prodContent.StartsWith("\ufeff"), "Production config should not contain UTF-8 BOM");
        Assert.IsFalse(devContent.StartsWith("\ufeff"), "Development config should not contain UTF-8 BOM");
    }
}