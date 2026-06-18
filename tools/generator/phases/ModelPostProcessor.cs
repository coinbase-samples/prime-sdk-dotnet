/*
 * Copyright 2026-present Coinbase Global, Inc.
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
using CoinbaseSdk.Tools.Generator.Processing;
using Microsoft.Extensions.Logging;

namespace CoinbaseSdk.Tools.Generator.Phases;

public class ModelPostProcessor
{
  private readonly ILogger<ModelPostProcessor> _logger;
  private readonly SharedTransforms _transforms;
  private readonly string _tempDir;
  private readonly string _outputDir;
  private readonly string _commonDir;
  private readonly string _enumsDir;
  private readonly IReadOnlyDictionary<string, string> _commonModels;
  private readonly string _specPath;
  private readonly GeneratorConfiguration _configuration;
  private int _newModelsCount;
  private int _updatedModelsCount;

  public ModelPostProcessor(
    ILogger<ModelPostProcessor> logger,
    SharedTransforms transforms,
    string tempDir,
    string outputDir,
    string commonDir,
    string enumsDir,
    IReadOnlyDictionary<string, string> commonModels,
    string specPath,
    GeneratorConfiguration configuration)
  {
    _logger = logger;
    _transforms = transforms;
    _tempDir = tempDir;
    _outputDir = outputDir;
    _commonDir = commonDir;
    _enumsDir = enumsDir;
    _commonModels = commonModels;
    _specPath = specPath;
    _configuration = configuration;
  }

  public async Task ProcessModelsAsync()
  {
    _logger.LogInformation("Loading schema documentation index from {Path}...", _specPath);
    var docIndex = await SchemaDocumentationIndex.LoadAsync(
      _specPath,
      _transforms,
      _commonModels,
      _configuration.EnumNameMappings);

    _logger.LogInformation("Finding generated model files...");
    var modelFiles = FindGeneratedModelFiles();
    _logger.LogInformation("Found {Count} model files to process", modelFiles.Count);

    Directory.CreateDirectory(_outputDir);
    Directory.CreateDirectory(_enumsDir);

    var enumFiles = new List<string>();
    var classFiles = new List<string>();
    foreach (var file in modelFiles)
    {
      var content = await File.ReadAllTextAsync(file);
      if (content.Contains("public enum ", StringComparison.Ordinal))
      {
        enumFiles.Add(file);
      }
      else
      {
        classFiles.Add(file);
      }
    }

    _logger.LogInformation("Found {EnumCount} enums and {ModelCount} models", enumFiles.Count, classFiles.Count);

    _logger.LogInformation("Processing enums first...");
    foreach (var file in enumFiles)
    {
      var fileName = Path.GetFileName(file);
      _logger.LogInformation("Processing enum: {Name}", fileName);
      try
      {
        await ProcessEnumFileAsync(file, docIndex);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error processing enum file: {Name}", fileName);
      }
    }

    _logger.LogInformation("Processing models...");
    foreach (var file in classFiles)
    {
      var fileName = Path.GetFileName(file);
      _logger.LogInformation("Processing model: {Name}", fileName);
      try
      {
        await ProcessModelFileAsync(file, docIndex);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error processing model file: {Name}", fileName);
      }
    }

    _logger.LogInformation("Cleaning up temporary files...");
    Directory.Delete(_tempDir, recursive: true);

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
        if (ShouldIgnore(Path.GetFileNameWithoutExtension(fileName), _commonModels))
        {
          continue;
        }

        if (fileName.Contains("AssemblyInfo", StringComparison.Ordinal) ||
            fileName.Contains("Attributes", StringComparison.Ordinal))
        {
          continue;
        }

        if (fileName.EndsWith("Tests.cs", StringComparison.Ordinal) ||
            fileName.EndsWith("Test.cs", StringComparison.Ordinal))
        {
          continue;
        }

        models.Add(file);
      }
    }

    return models;
  }

  internal static bool ShouldIgnoreModelFile(string name, IReadOnlyDictionary<string, string> commonModels)
  {
    return ShouldIgnore(name, commonModels);
  }

  private static bool ShouldIgnore(string name, IReadOnlyDictionary<string, string> commonModels)
  {
    if (IsCommonModelSource(name, commonModels))
    {
      return false;
    }

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

  private static bool IsCommonModelSource(string fileNameWithoutExtension, IReadOnlyDictionary<string, string> commonModels)
  {
    foreach (var source in commonModels.Keys)
    {
      if (fileNameWithoutExtension.Contains(source, StringComparison.Ordinal))
      {
        return true;
      }
    }

    return false;
  }

  private async Task ProcessEnumFileAsync(string filePath, SchemaDocumentationIndex docIndex)
  {
    var content = await File.ReadAllTextAsync(filePath);
    var className = ExtractClassName(content);
    var originalClassName = className;
    var originalFileName = Path.GetFileName(filePath);

    content = _transforms.ApplyContentReplacements(content);
    className = _transforms.StripCommonPrefixes(className);
    if (className != originalClassName)
    {
      content = content.Replace($"enum {originalClassName}", $"enum {className}", StringComparison.Ordinal);
      _logger.LogInformation("Transformed enum name: {Original} -> {New}", originalClassName, className);
    }

    className = ExtractClassName(content);
    var fileName = $"{className}.cs";
    var outputPath = Path.Combine(_enumsDir, fileName);
    var existsBefore = File.Exists(outputPath);

    HandleCaseVariants(_enumsDir, fileName);
    RemoveStaleFile(_enumsDir, originalFileName, fileName, className, isEnum: true);

    content = JsonNameCodegen.PostProcessEmittedSource(content);
    content = EnumXmlDocEnhancer.Apply(content, className, docIndex);
    content = CopyrightHelper.ApplyCopyrightYear(outputPath, content);
    await File.WriteAllTextAsync(outputPath, content);

    if (!existsBefore)
    {
      _newModelsCount++;
    }
    else
    {
      _updatedModelsCount++;
    }
  }

  private async Task ProcessModelFileAsync(string filePath, SchemaDocumentationIndex docIndex)
  {
    var content = await File.ReadAllTextAsync(filePath);
    var className = ExtractClassName(content);
    var originalClassName = className;
    var originalFileName = Path.GetFileName(filePath);

    content = _transforms.ApplyContentReplacements(content);
    className = _transforms.StripCommonPrefixes(className);
    if (className != originalClassName)
    {
      content = content.Replace($"class {originalClassName}", $"class {className}", StringComparison.Ordinal);
      content = content.Replace($"enum {originalClassName}", $"enum {className}", StringComparison.Ordinal);
      _logger.LogInformation("Transformed class name: {Original} -> {New}", originalClassName, className);
    }

    content = _transforms.ApplyWeb3ToOnchainContent(content, className);
    className = ExtractClassName(content);
    var fileName = $"{className}.cs";
    if (fileName.Contains("Web3", StringComparison.Ordinal))
    {
      fileName = fileName.Replace("Web3", "Onchain", StringComparison.Ordinal);
      className = className.Replace("Web3", "Onchain", StringComparison.Ordinal);
    }

    content = _transforms.FixConstructorNames(content, className);

    className = ExtractClassName(content);
    fileName = $"{className}.cs";

    var outputDirectory = _commonModels.Values.Contains(className, StringComparer.Ordinal)
      ? _commonDir
      : _outputDir;
    if (outputDirectory == _commonDir)
    {
      content = content.Replace(
        "namespace CoinbaseSdk.Prime.Model",
        "namespace CoinbaseSdk.Prime.Common",
        StringComparison.Ordinal);
    }

    var outputPath = Path.Combine(outputDirectory, fileName);
    var existsBefore = File.Exists(outputPath);

    HandleCaseVariants(outputDirectory, fileName);
    RemoveStaleFile(outputDirectory, originalFileName, fileName, className, isEnum: false);

    var actualEnumNames = new HashSet<string>();
    if (Directory.Exists(_enumsDir))
    {
      foreach (var f in Directory.GetFiles(_enumsDir, "*.cs"))
      {
        actualEnumNames.Add(Path.GetFileNameWithoutExtension(f));
      }
    }

    content = _transforms.ApplyEnumMappings(content, actualEnumNames);
    content = JsonNameCodegen.PostProcessEmittedSource(content);
    content = ModelXmlDocEnhancer.Apply(content, className, docIndex);
    content = CopyrightHelper.ApplyCopyrightYear(outputPath, content);

    await File.WriteAllTextAsync(outputPath, content);

    if (!existsBefore)
    {
      _newModelsCount++;
    }
    else
    {
      _updatedModelsCount++;
    }
  }

  private void HandleCaseVariants(string directory, string fileName)
  {
    if (!Directory.Exists(directory))
    {
      return;
    }

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

  private static string ExtractClassName(string content)
  {
    var pattern = new Regex(@"public\s+(?:class|enum)\s+(\w+)");
    var match = pattern.Match(content);
    return match.Success ? match.Groups[1].Value : string.Empty;
  }

  private void RemoveStaleFile(string directory, string originalFileName, string newFileName, string typeName, bool isEnum)
  {
    if (!Directory.Exists(directory) || string.IsNullOrWhiteSpace(typeName))
    {
      return;
    }

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

    foreach (var path in Directory.GetFiles(directory, "*.cs"))
    {
      var fileName = Path.GetFileName(path);
      if (fileName.Equals(newFileName, StringComparison.OrdinalIgnoreCase))
      {
        continue;
      }

      var fc = File.ReadAllText(path);
      if (Regex.IsMatch(fc, $@"public\s+(?:class|enum)\s+{Regex.Escape(typeName)}\b"))
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
}
