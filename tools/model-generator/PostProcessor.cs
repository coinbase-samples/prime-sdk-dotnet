/*
 * Copyright 2025-present Coinbase Global, Inc.
 *
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 */

using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;

namespace CoinbaseSdk.Tools.ModelGenerator;

public class PostProcessor
{
  private readonly ILogger<PostProcessor> _logger;
  private readonly string _tempDir;
  private readonly string _outputDir;
  private readonly string _enumsDir;
  private int _newModelsCount = 0;
  private int _updatedModelsCount = 0;

  // Special case enum renames: stripped name -> final enum name
  // This handles cases where enums need custom naming beyond simple prefix stripping
  private static readonly Dictionary<string, string> EnumNameMappings = new()
  {
    { "ActivityType", "PrimeActivityType" }  // CoinbasePublicRestApiActivityType -> PrimeActivityType
    // CoinbaseCustodyApiActivityType is handled by FILE_PATH_REPLACEMENTS
  };

  // File path replacements (matching prime-sdk-java FILE_PATH_REPLACEMENTS)
  // Used for transforming class names and file names
  private static readonly Dictionary<string, string> FilePathReplacements = new()
  {
    { "CoinbaseCustodyApiActivityType", "CustodyActivityType" },
    { "CoinbasePublicRestApiActivityType", "PrimeActivityType" },
    { "CoinbaseBrokerageProxyEventsMaterializedApi", "" },
    { "CoinbasePublicRestApi", "" },
    { "CoinbaseCustodyApi", "" },
    { "PrimeRESTAPI", "" },
    { "PublicRestApi", "" },
    { "rFQ", "RFQ" },
    { "FcmFuturesSweep", "FuturesSweep" }
  };

  // Content replacements (matching prime-sdk-java CONTENT_REPLACEMENTS)
  // Applied to all file content to strip prefixes from type references
  private static readonly Dictionary<string, string> ContentReplacements = new()
  {
    { "coinbaseCustodyApiActivityType", "CustodyActivityType" },
    { "coinbasePublicRestApiActivityType", "PrimeActivityType" },
    { "CoinbaseCustodyApiActivityType", "CustodyActivityType" },
    { "CoinbasePublicRestApiActivityType", "PrimeActivityType" },
    { "CoinbasePublicRestApi", "" },
    { "coinbasePublicRestApi", "" },
    { "PrimeRESTAPI", "" },
    { "primeRESTAPI", "" },
    { "CoinbaseCustodyApi", "" },
    { "coinbaseCustodyApi", "" },
    { "CoinbaseBrokerageProxyEventsMaterializedApi", "" },
    { "coinbaseBrokerageProxyEventsMaterializedApi", "" },
    { "publicRestApi", "" },
    { "PublicRestApi", "" },
    // Simplify verbose model names
    { "CreateOnchainTransactionRequestEvmParams", "EvmParams" },
    { "FcmFuturesSweepRequestAmount", "SweepAmount" },
    { "FcmFuturesSweep", "FuturesSweep" },
    // Fix type name mismatches (handwritten files and generated)
    { "EvmParam ", "EvmParams " },
    { "EvmParam?", "EvmParams?" },
    { "EvmParam>", "EvmParams>" },
    { "RPC ", "RpcConfig " },
    { "RPC?", "RpcConfig?" },
    { "RPC>", "RpcConfig>" }
    // { "BalanceType ", "PortfolioBalanceType " },
    // { "BalanceType>", "PortfolioBalanceType>" },
    // { "BalanceType;", "PortfolioBalanceType;" },
    // { "BalanceType{", "PortfolioBalanceType{" }
  };

  // Acronym normalization map to keep type names PascalCase (aligns with Java generator)
  private static readonly List<(string Acronym, string Normalized)> AcronymMappings = new()
  {
    ("FCM", "Fcm"),
    ("XML", "Xml"),
    ("XM", "Xm"),
    ("PM", "Pm"),
    ("RFQ", "Rfq"),
    ("NFT", "Nft"),
    ("EVM", "Evm"),
    ("VASP", "Vasp")
  };

  public PostProcessor(ILogger<PostProcessor> logger, string tempDir, string outputDir, string enumsDir)
  {
    _logger = logger;
    _tempDir = tempDir;
    _outputDir = outputDir;
    _enumsDir = enumsDir;
  }

  public async Task ProcessModelsAsync()
  {
    _logger.LogInformation("Finding generated model files...");
    var modelFiles = FindGeneratedModelFiles();
    _logger.LogInformation("Found {Count} model files to process", modelFiles.Count);

    // Create output directories
    Directory.CreateDirectory(_outputDir);
    Directory.CreateDirectory(_enumsDir);

    // Separate enums from models by reading file content
    var enumFiles = new List<string>();
    var classFiles = new List<string>();

    foreach (var file in modelFiles)
    {
      var content = await File.ReadAllTextAsync(file);
      if (content.Contains("public enum "))
      {
        enumFiles.Add(file);
      }
      else
      {
        classFiles.Add(file);
      }
    }

    _logger.LogInformation("Found {EnumCount} enums and {ModelCount} models", enumFiles.Count, classFiles.Count);

    // Process enums first
    _logger.LogInformation("Processing enums first...");
    foreach (var file in enumFiles)
    {
      var fileName = Path.GetFileName(file);
      _logger.LogInformation("Processing enum: {Name}", fileName);
      try
      {
        await ProcessEnumFileAsync(file);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error processing enum file: {Name}", fileName);
      }
    }

    // Then process models
    _logger.LogInformation("Processing models...");
    foreach (var file in classFiles)
    {
      var fileName = Path.GetFileName(file);
      _logger.LogInformation("Processing model: {Name}", fileName);
      try
      {
        await ProcessModelFileAsync(file);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error processing model file: {Name}", fileName);
      }
    }

    // Clean up temporary directory
    _logger.LogInformation("Cleaning up temporary files...");
    Directory.Delete(_tempDir, recursive: true);

    // Log summary
    _logger.LogInformation("==========================================");
    _logger.LogInformation("Model Generation Summary:");
    _logger.LogInformation("  New models: {Count}", _newModelsCount);
    _logger.LogInformation("  Updated models: {Count}", _updatedModelsCount);
    _logger.LogInformation("  Total processed: {Count}", _newModelsCount + _updatedModelsCount);
    _logger.LogInformation("==========================================");
  }

  private List<string> FindGeneratedModelFiles()
  {
    var models = new List<string>();
    var srcPath = Path.Combine(_tempDir, "raw", "src");

    if (!Directory.Exists(srcPath))
    {
      _logger.LogWarning("Source directory not found at {Path}, checking alternative locations", srcPath);
      srcPath = Path.Combine(_tempDir, "raw");
    }

    if (Directory.Exists(srcPath))
    {
      foreach (var file in Directory.GetFiles(srcPath, "*.cs", SearchOption.AllDirectories))
      {
        var fileName = Path.GetFileName(file);

        // Skip ignored patterns
        if (ShouldIgnore(Path.GetFileNameWithoutExtension(fileName)))
        {
          _logger.LogDebug("Skipping ignored file: {Name}", fileName);
          continue;
        }

        // Skip infrastructure files
        if (fileName.Contains("AssemblyInfo") || fileName.Contains("Attributes") ||
            fileName.EndsWith("Api.cs") && !fileName.Contains("Api"))
        {
          continue;
        }

        // Skip test files
        if (fileName.EndsWith("Tests.cs") || fileName.EndsWith("Test.cs"))
        {
          _logger.LogDebug("Skipping test file: {Name}", fileName);
          continue;
        }

        models.Add(file);
      }
    }

    return models;
  }

  private bool ShouldIgnore(string name)
  {
    if (name.EndsWith("Request", StringComparison.Ordinal) ||
        name.EndsWith("Response", StringComparison.Ordinal))
    {
      return true;
    }

    if (name.StartsWith("Google", StringComparison.Ordinal))
    {
      return true;
    }

    if (name.Contains("AllOf", StringComparison.Ordinal) ||
        name.Contains("OneOf", StringComparison.Ordinal) ||
        name.Contains("AnyOf", StringComparison.Ordinal))
    {
      return true;
    }

    if (name.Contains("RequestIsARequestTo", StringComparison.Ordinal) ||
        name.Equals("AbstractOpenApiSchema", StringComparison.Ordinal))
    {
      return true;
    }

    return false;
  }

  private async Task ProcessEnumFileAsync(string filePath)
  {
    var content = await File.ReadAllTextAsync(filePath);
    var className = ExtractClassName(content);
    var originalClassName = className;
    var originalFileName = Path.GetFileName(filePath);

    // Apply content replacements to ALL files (matching Java applyContentReplacements logic)
    content = ApplyContentReplacements(content);

    // Strip prefixes from class name (matching Java stripCommonPrefixes logic)
    className = StripCommonPrefixes(className);

    if (className != originalClassName)
    {
      content = content.Replace($"enum {originalClassName}", $"enum {className}");
      _logger.LogInformation("Transformed enum name: {Original} -> {New}", originalClassName, className);
    }

    // Extract final class name after all transformations (for correct filename)
    className = ExtractClassName(content);
    var fileName = $"{className}.cs";
    var outputPath = Path.Combine(_enumsDir, fileName);
    var existsBefore = File.Exists(outputPath);

    // Handle case-only filename changes
    HandleCaseVariants(_enumsDir, fileName);

    // Remove stale files produced before renaming rules converged
    RemoveStaleFile(_enumsDir, originalFileName, fileName, className, isEnum: true);

    content = DeduplicateUsings(content);

    await File.WriteAllTextAsync(outputPath, content);

    if (!existsBefore)
    {
      _logger.LogInformation("Generated new enum: {Name}", className);
      _newModelsCount++;
    }
    else
    {
      _logger.LogInformation("Updated enum: {Name}", className);
      _updatedModelsCount++;
    }
  }

  private async Task ProcessModelFileAsync(string filePath)
  {
    var content = await File.ReadAllTextAsync(filePath);
    var className = ExtractClassName(content);
    var originalClassName = className;
    var originalFileName = Path.GetFileName(filePath);

    // Apply content replacements to ALL files (matching Java applyContentReplacements logic)
    content = ApplyContentReplacements(content);

    // Strip prefixes from class name (matching Java stripCommonPrefixes logic)
    className = StripCommonPrefixes(className);

    if (className != originalClassName)
    {
      content = content.Replace($"class {originalClassName}", $"class {className}");
      content = content.Replace($"enum {originalClassName}", $"enum {className}");
      _logger.LogInformation("Transformed class name: {Original} -> {New}", originalClassName, className);
    }

    // Apply Web3 to Onchain transformation
    content = ApplyWeb3ToOnchainTransformation(content, className);

    // Extract final class name after all transformations (for correct filename)
    className = ExtractClassName(content);
    var fileName = $"{className}.cs";

    // Apply Web3 to Onchain transformation to filename
    if (fileName.Contains("Web3"))
    {
      fileName = fileName.Replace("Web3", "Onchain");
      className = className.Replace("Web3", "Onchain");
    }

    // Fix constructor names to match final class name
    content = FixConstructorNames(content, className);

    var outputPath = Path.Combine(_outputDir, fileName);
    var existsBefore = File.Exists(outputPath);

    // Handle case-only filename changes
    HandleCaseVariants(_outputDir, fileName);

    // Remove stale files produced before renaming rules converged
    RemoveStaleFile(_outputDir, originalFileName, fileName, className, isEnum: false);

    content = ApplyEnumMappings(content);
    content = DeduplicateUsings(content);

    await File.WriteAllTextAsync(outputPath, content);

    if (!existsBefore)
    {
      _logger.LogInformation("Generated new model: {Name}", className);
      _newModelsCount++;
    }
    else
    {
      _logger.LogInformation("Updated model: {Name}", className);
      _updatedModelsCount++;
    }
  }

  private string ApplyWeb3ToOnchainTransformation(string content, string className)
  {
    if (content.Contains("Web3") || content.Contains("web3"))
    {
      _logger.LogInformation("Applying Web3 to Onchain transformation for: {Name}", className);

      // Replace class names
      content = Regex.Replace(content, @"\bWeb3", "Onchain");

      // Replace in property names and method names
      content = Regex.Replace(content, @"\bweb3", "onchain");

      // Keep JSON property mappings unchanged
      content = content.Replace("[JsonPropertyName(\"onchain\")]", "[JsonPropertyName(\"web3\")]");
    }

    return content;
  }

  /// <summary>
  /// Applies special case enum name mappings to match the renamed enum files.
  /// </summary>
  private string ApplyEnumMappings(string content)
  {
    // Get list of all actual enum names from enums directory
    var actualEnumNames = new HashSet<string>();
    if (Directory.Exists(_enumsDir))
    {
      foreach (var file in Directory.GetFiles(_enumsDir, "*.cs"))
      {
        var fileName = Path.GetFileNameWithoutExtension(file);
        actualEnumNames.Add(fileName);
      }
    }

    // First, apply special case enum name mappings (e.g., ActivityType -> PrimeActivityType)
    // This must happen BEFORE fixing import paths
    foreach (var mapping in EnumNameMappings)
    {
      var strippedName = mapping.Key;
      var actualEnumName = mapping.Value;

      // Only apply mapping if the actual enum exists
      if (actualEnumNames.Contains(actualEnumName))
      {
        // Replace type references (but not in JsonPropertyName attributes)
        // Pattern: word boundary + strippedName + word boundary (not inside attribute)
        content = Regex.Replace(
          content,
          $@"\b{Regex.Escape(strippedName)}\b(?![^\[]*\[JsonPropertyName)",
          actualEnumName
        );
        _logger.LogDebug("Applied enum mapping: {Original} -> {New}", strippedName, actualEnumName);
      }
    }
    var referencesEnum = actualEnumNames.Any(enumName => Regex.IsMatch(content, $@"\b{Regex.Escape(enumName)}\b"));
    if (referencesEnum && !content.Contains("using CoinbaseSdk.Prime.Model.Enums;", StringComparison.Ordinal))
    {
      if (content.Contains("using System.Text.Json.Serialization;", StringComparison.Ordinal))
      {
        content = content.Replace(
          "using System.Text.Json.Serialization;\n",
          "using System.Text.Json.Serialization;\n  using CoinbaseSdk.Prime.Model.Enums;\n",
          StringComparison.Ordinal);
      }
      else
      {
        content = Regex.Replace(
          content,
          @"(namespace\s+CoinbaseSdk\.Prime\.Model\s*\{?\s*\n)",
          "$1  using CoinbaseSdk.Prime.Model.Enums;\n",
          RegexOptions.None);
      }
    }
    return content;
  }
  /// <summary>
  /// Apply content replacements to all files to strip prefixes from type references.
  /// Matches prime-sdk-java applyContentReplacements() behavior.
  /// </summary>
  private string ApplyContentReplacements(string content)
  {
    // Apply content replacements (matching Java CONTENT_REPLACEMENTS)
    // Uses String.Replace() which replaces ALL occurrences (like Java)
    foreach (var replacement in ContentReplacements)
    {
      content = content.Replace(replacement.Key, replacement.Value);
    }
    content = NormalizeAcronymsInContent(content);
    return content;
  }

  private string NormalizeAcronyms(string value)
  {
    var result = value;

    foreach (var (acronym, normalized) in AcronymMappings)
    {
      result = Regex.Replace(result, $@"\b{Regex.Escape(acronym)}(?=[A-Z])", normalized);

      if (result.EndsWith(acronym, StringComparison.Ordinal))
      {
        result = string.Concat(result.AsSpan(0, result.Length - acronym.Length), normalized);
      }
    }

    return result;
  }

  private string NormalizeAcronymsInContent(string content)
  {
    var result = content;

    foreach (var (acronym, normalized) in AcronymMappings)
    {
      // Replace acronyms in various contexts, but NOT in constructor names or Build() return types
      // Match: VASP followed by PascalCase identifier (e.g., VASPBalance -> VaspBalance)
      result = Regex.Replace(result, $@"\b{Regex.Escape(acronym)}(?=[A-Z](?!_))", normalized);
      // Match: VASP followed by space and lowercase letter (e.g., "VASP value" -> "Vasp value")
      result = Regex.Replace(result, $@"\b{Regex.Escape(acronym)}(?=\s+[a-z])", normalized);
      // Match: VASP followed by delimiter, but NOT followed by ( or ) (to preserve constructors and Build())
      result = Regex.Replace(result, $@"\b{Regex.Escape(acronym)}(?=[<>;])", normalized);
      // Match: VASP followed by dot (for namespace/member access)
      result = Regex.Replace(result, $@"\b{Regex.Escape(acronym)}(?=\.)", normalized);
    }

    return result;
  }


  private void HandleCaseVariants(string directory, string fileName)
  {
    if (!Directory.Exists(directory)) return;

    foreach (var file in Directory.GetFiles(directory))
    {
      var existingFileName = Path.GetFileName(file);
      if (existingFileName.Equals(fileName, StringComparison.OrdinalIgnoreCase) &&
          !existingFileName.Equals(fileName, StringComparison.Ordinal))
      {
        File.Delete(file);
        _logger.LogInformation("Deleted old file with different casing: {Name}", existingFileName);
      }
    }
  }

  private string ExtractClassName(string content)
  {
    var pattern = new Regex(@"public\s+(?:class|enum)\s+(\w+)");
    var match = pattern.Match(content);
    return match.Success ? match.Groups[1].Value : string.Empty;
  }

  /// <summary>
  /// Fix constructor names and Build() return type to match the final class name.
  /// This handles cases where acronym normalization changed constructor names
  /// but the class name remained an acronym (e.g., class VASP with Vasp() constructor).
  /// </summary>
  private string FixConstructorNames(string content, string className)
  {
    // Find all potential constructor-like patterns (public Xxx( and new Xxx()
    // and replace them with the correct class name

    // Pattern 1: public ConstructorName() - match any identifier followed by (
    // Pattern 2: public ConstructorName(Builder - match any identifier followed by (Builder
    // Pattern 3: new ConstructorName(this) - in Builder.Build()
    // Pattern 4: public ClassName Build() - return type in Build method

    // Find incorrect constructor names by looking for patterns that might be wrong
    foreach (var (acronym, normalized) in AcronymMappings)
    {
      if (className == acronym)
      {
        // Class name is the acronym (e.g., VASP), so constructors should use acronym
        // Fix: public Vasp() -> public VASP()
        content = Regex.Replace(content, $@"public {normalized}\s*\(", $"public {acronym}(");
        // Fix: new Vasp(this) -> new VASP(this)
        content = Regex.Replace(content, $@"new {normalized}\s*\(", $"new {acronym}(");
        // Fix: public Vasp Build() -> public VASP Build()
        content = Regex.Replace(content, $@"public {normalized} Build\(\)", $"public {acronym} Build()");
      }
      else if (className == normalized)
      {
        // Class name is normalized (e.g., Vasp), so constructors should use normalized form
        // Fix: public VASP() -> public Vasp()
        content = Regex.Replace(content, $@"public {acronym}\s*\(", $"public {normalized}(");
        // Fix: new VASP(this) -> new Vasp(this)
        content = Regex.Replace(content, $@"new {acronym}\s*\(", $"new {normalized}(");
        // Fix: public VASP Build() -> public Vasp Build()
        content = Regex.Replace(content, $@"public {acronym} Build\(\)", $"public {normalized} Build()");
      }
    }

    return content;
  }

  private void RemoveStaleFile(string directory, string originalFileName, string newFileName, string typeName, bool isEnum)
  {
    if (!Directory.Exists(directory))
    {
      return;
    }

    if (string.IsNullOrWhiteSpace(typeName))
    {
      return;
    }

    // Delete stale file that matches the old generator output name
    if (!string.Equals(originalFileName, newFileName, StringComparison.OrdinalIgnoreCase))
    {
      var oldPath = Path.Combine(directory, originalFileName);
      if (File.Exists(oldPath))
      {
        File.Delete(oldPath);
        _logger.LogInformation(
          "Deleted stale {Kind} file from previous naming: {FileName}",
          isEnum ? "enum" : "model",
          originalFileName);
      }
    }

    // Delete any other file in the directory that still defines this type (guards against historic duplicates)
    foreach (var path in Directory.GetFiles(directory, "*.cs"))
    {
      var fileName = Path.GetFileName(path);
      if (fileName.Equals(newFileName, StringComparison.OrdinalIgnoreCase))
      {
        continue;
      }

      var content = File.ReadAllText(path);
      if (Regex.IsMatch(content, $@"public\s+(?:class|enum)\s+{Regex.Escape(typeName)}\b"))
      {
        File.Delete(path);
        _logger.LogInformation(
          "Deleted duplicate {Kind} definition for {TypeName}: {FileName}",
          isEnum ? "enum" : "model",
          typeName,
          fileName);
      }
    }
  }

  /// <summary>
  /// Apply file path replacements to strip common prefixes from class names.
  /// Matches prime-sdk-java stripCommonPrefixes() behavior.
  /// </summary>
  private string StripCommonPrefixes(string className)
  {
    var result = className;

    // Apply replacements in order (Dictionary maintains insertion order in .NET Core 3.0+)
    foreach (var entry in FilePathReplacements)
    {
      if (result.Contains(entry.Key))
      {
        result = result.Replace(entry.Key, entry.Value);
      }
    }

    return NormalizeAcronyms(result);
  }

  private string DeduplicateUsings(string content)
  {
    var lines = content.Split('\n');
    var seen = new HashSet<string>(StringComparer.Ordinal);
    var builder = new StringBuilder();

    foreach (var line in lines)
    {
      var trimmed = line.Trim();
      if (trimmed.StartsWith("using ", StringComparison.Ordinal) && trimmed.EndsWith(";", StringComparison.Ordinal))
      {
        if (!seen.Add(trimmed))
        {
          continue;
        }
      }

      builder.Append(line);
      builder.Append('\n');
    }

    return builder.ToString().TrimEnd('\n') + '\n';
  }
}
