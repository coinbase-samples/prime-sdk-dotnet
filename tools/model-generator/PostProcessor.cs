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

  // Prefix mappings for stripping common API/service prefixes from generated class names
  private static readonly Dictionary<string, string> PrefixMappings = new()
  {
    { "CoinbaseBrokerageProxyEventsMaterializedApi", "" },
    { "CoinbasePublicRestApi", "" },
    { "CoinbaseCustodyApi", "" },
    { "PrimeRESTAPI", "" },
    { "PublicRestApi", "" },
    { "rFQ", "RFQ" },
    { "FcmFuturesSweep", "FuturesSweep" }
  };

  // Content replacements (matching prime-sdk-java logic)
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
    // Preserve all-caps acronym casing
    { "FcmMarginCall", "FCMMarginCall" },
    { "XmLoan", "XMLoan" },
    { "XmMarginCall", "XMMarginCall" },
    { "XmSummary", "XMSummary" },
    // Simplify verbose model names
    { "CreateOnchainTransactionRequestEvmParams", "EvmParams" },
    { "FcmFuturesSweepRequestAmount", "SweepAmount" },
    { "FcmFuturesSweep", "FuturesSweep" }
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
    var ignorePatterns = new[]
    {
      "Request", "Response", "Google", "AllOf", "OneOf", "AnyOf",
      "RequestIsARequestTo", "AbstractOpenApiSchema"
    };

    return ignorePatterns.Any(pattern => name.Contains(pattern));
  }

  private async Task ProcessEnumFileAsync(string filePath)
  {
    var content = await File.ReadAllTextAsync(filePath);
    
    // Extract original class name BEFORE applying content replacements
    var originalClassName = ExtractClassName(content);
    
    // Apply specialized transformations to class name (e.g., ActivityType variants)
    var className = ApplyClassNameTransformations(originalClassName);
    
    // Strip common prefixes from the transformed name
    className = StripCommonPrefixes(className);

    // Apply content replacements to the content
    content = ApplyContentReplacements(content);

    // Update class declaration in content
    if (className != originalClassName)
    {
      content = content.Replace($"enum {originalClassName}", $"enum {className}");
      _logger.LogInformation("Transformed enum name: {Original} -> {New}", originalClassName, className);
    }

    // Update namespace to enums
    content = content.Replace("namespace CoinbaseSdk.Prime.Model", "namespace CoinbaseSdk.Prime.Model.Enums");

    var fileName = $"{className}.cs";
    var outputPath = Path.Combine(_enumsDir, fileName);
    var existsBefore = File.Exists(outputPath);

    // Handle case-only filename changes
    HandleCaseVariants(_enumsDir, fileName);

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
    
    // Extract original class name BEFORE applying content replacements
    var originalClassName = ExtractClassName(content);
    
    // Apply specialized transformations to class name (e.g., ActivityType variants)
    var className = ApplyClassNameTransformations(originalClassName);
    
    // Strip common prefixes from the transformed name
    className = StripCommonPrefixes(className);

    // Apply content replacements to the content
    content = ApplyContentReplacements(content);

    // Update class declaration in content
    if (className != originalClassName)
    {
      content = content.Replace($"class {originalClassName}", $"class {className}");
      _logger.LogInformation("Transformed class name: {Original} -> {New}", originalClassName, className);
    }

    // Apply Web3 to Onchain transformation
    content = ApplyWeb3ToOnchainTransformation(content, className);

    // Fix enum imports (add Enums namespace)
    content = FixEnumImports(content);

    // Apply Web3 to Onchain transformation to filename
    var fileName = className.Replace("Web3", "Onchain") + ".cs";

    var outputPath = Path.Combine(_outputDir, fileName);
    var existsBefore = File.Exists(outputPath);

    // Handle case-only filename changes
    HandleCaseVariants(_outputDir, fileName);

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
      content = content.Replace("[JsonPropertyName(\"onchain", "[JsonPropertyName(\"web3");
    }

    return content;
  }

  private string FixEnumImports(string content)
  {
    // Add using statement for enums if not already present
    if (!content.Contains("using CoinbaseSdk.Prime.Model.Enums;"))
    {
      content = Regex.Replace(
        content,
        @"(namespace\s+CoinbaseSdk\.Prime\.Model\s*\{?\s*\n)",
        "$1  using CoinbaseSdk.Prime.Model.Enums;\n"
      );
    }

    return content;
  }
  private string ApplyContentReplacements(string content)
  {
    foreach (var replacement in ContentReplacements)
    {
      content = content.Replace(replacement.Key, replacement.Value);
    }
    return content;
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

  private string ApplyClassNameTransformations(string className)
  {
    // Apply specific transformations that need to happen before prefix stripping
    // to avoid collisions (e.g., CustodyActivityType vs PrimeActivityType)
    var transformations = new Dictionary<string, string>
    {
      { "CoinbaseCustodyApiActivityType", "CustodyActivityType" },
      { "CoinbasePublicRestApiActivityType", "PrimeActivityType" }
    };

    if (transformations.TryGetValue(className, out var transformed))
    {
      return transformed;
    }

    return className;
  }

  private string StripCommonPrefixes(string className)
  {
    // Apply prefix mappings
    foreach (var mapping in PrefixMappings)
    {
      if (className.StartsWith(mapping.Key))
      {
        var stripped = mapping.Value + className.Substring(mapping.Key.Length);
        // Return the stripped name, or original if it would result in empty string
        return !string.IsNullOrEmpty(stripped) ? stripped : className;
      }
    }

    return className;
  }
}
