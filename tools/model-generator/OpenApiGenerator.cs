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

using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace CoinbaseSdk.Tools.ModelGenerator;

public class OpenApiGenerator
{
  private readonly ILogger<OpenApiGenerator> _logger;
  private readonly string _specUrl;
  private readonly string _outputDir;

  public OpenApiGenerator(ILogger<OpenApiGenerator> logger, string specUrl, string outputDir)
  {
    _logger = logger;
    _specUrl = specUrl;
    _outputDir = outputDir;
  }

  public async Task GenerateModelsAsync()
  {
    var rawOutputDir = Path.Combine(_outputDir, "raw");

    // Create output directory
    Directory.CreateDirectory(rawOutputDir);

    // Copy .openapi-generator-ignore file BEFORE cleaning
    var projectRoot = FindProjectRoot();
    var ignoreFile = Path.Combine(projectRoot, "tools", "model-generator", ".openapi-generator-ignore");
    var targetIgnoreFile = Path.Combine(rawOutputDir, ".openapi-generator-ignore");

    if (File.Exists(ignoreFile))
    {
      File.Copy(ignoreFile, targetIgnoreFile, overwrite: true);
      _logger.LogInformation("Copied .openapi-generator-ignore to: {Path}", targetIgnoreFile);
    }
    else
    {
      _logger.LogWarning(".openapi-generator-ignore not found at: {Path}", ignoreFile);
    }

    // Clean output directory (but preserve .openapi-generator-ignore)
    if (Directory.Exists(rawOutputDir))
    {
      foreach (var file in Directory.GetFiles(rawOutputDir))
      {
        if (file != targetIgnoreFile)
        {
          File.Delete(file);
        }
      }
      foreach (var dir in Directory.GetDirectories(rawOutputDir))
      {
        Directory.Delete(dir, recursive: true);
      }
    }

    // Copy custom templates
    var templatesDir = Path.Combine(projectRoot, "tools", "model-generator", "templates");
    if (Directory.Exists(templatesDir))
    {
      _logger.LogInformation("Using custom templates from: {Path}", templatesDir);
    }
    else
    {
      _logger.LogWarning("Custom templates directory not found: {Path}, using defaults", templatesDir);
    }

    // Run OpenAPI Generator CLI
    await RunOpenApiGeneratorAsync(rawOutputDir, templatesDir);

    _logger.LogInformation("Raw models generated in: {Path}", rawOutputDir);
  }

  private async Task RunOpenApiGeneratorAsync(string outputDir, string templatesDir)
  {
    _logger.LogInformation("Running OpenAPI Generator CLI...");

    // Check if openapi-generator-cli is available
    var hasGenerator = await CheckOpenApiGeneratorInstalled();
    if (!hasGenerator)
    {
      throw new InvalidOperationException(
        "openapi-generator-cli not found. Please install it:\n" +
        "  npm install -g @openapitools/openapi-generator-cli\n" +
        "  or\n" +
        "  brew install openapi-generator"
      );
    }

    var startInfo = new ProcessStartInfo
    {
      FileName = "openapi-generator-cli",
      WorkingDirectory = outputDir,
      RedirectStandardOutput = true,
      RedirectStandardError = true,
      UseShellExecute = false,
      CreateNoWindow = true
    };

    // Build arguments
    var args = new List<string>
    {
      "generate",
      "-i", _specUrl,
      "-g", "csharp",
      "-o", ".",
      "--model-package", "CoinbaseSdk.Prime.Model",
      "--global-property", "models",
      "--global-property", "apis=false",
      "--global-property", "supportingFiles=false",
      "--additional-properties", "packageName=CoinbaseSdk.Prime",
      "--additional-properties", "targetFramework=net8.0",
      "--additional-properties", "library=httpclient",
      "--additional-properties", "nullableReferenceTypes=true",
      "--additional-properties", "optionalProjectFile=false",
      "--additional-properties", "optionalAssemblyInfo=false",
      "--additional-properties", "optionalEmitDefaultValues=false",
      "--additional-properties", "hideGenerationTimestamp=true",
      "--additional-properties", "modelPropertyNaming=PascalCase",
      "--additional-properties", "enumPropertyNaming=original",
      "--additional-properties", "generateApiTests=false",
      "--additional-properties", "generateModelTests=false"
    };

    if (Directory.Exists(templatesDir))
    {
      args.Add("-t");
      args.Add(templatesDir);
    }

    startInfo.Arguments = string.Join(" ", args);

    _logger.LogDebug("Command: {FileName} {Arguments}", startInfo.FileName, startInfo.Arguments);

    using var process = Process.Start(startInfo);
    if (process == null)
    {
      throw new InvalidOperationException("Failed to start openapi-generator-cli process");
    }

    var outputTask = process.StandardOutput.ReadToEndAsync();
    var errorTask = process.StandardError.ReadToEndAsync();

    await process.WaitForExitAsync();

    var output = await outputTask;
    var error = await errorTask;

    if (!string.IsNullOrWhiteSpace(output))
    {
      foreach (var line in output.Split('\n'))
      {
        if (!string.IsNullOrWhiteSpace(line))
        {
          _logger.LogDebug("OpenAPI Generator: {Line}", line.Trim());
        }
      }
    }

    if (process.ExitCode != 0)
    {
      _logger.LogError("OpenAPI Generator failed with exit code: {ExitCode}", process.ExitCode);
      if (!string.IsNullOrWhiteSpace(error))
      {
        _logger.LogError("Error output: {Error}", error);
      }
      throw new InvalidOperationException($"OpenAPI Generator failed with exit code {process.ExitCode}");
    }
  }

  private async Task<bool> CheckOpenApiGeneratorInstalled()
  {
    try
    {
      var startInfo = new ProcessStartInfo
      {
        FileName = "openapi-generator-cli",
        Arguments = "version",
        RedirectStandardOutput = true,
        RedirectStandardError = true,
        UseShellExecute = false,
        CreateNoWindow = true
      };

      using var process = Process.Start(startInfo);
      if (process == null) return false;

      await process.WaitForExitAsync();
      return process.ExitCode == 0;
    }
    catch
    {
      return false;
    }
  }

  private string FindProjectRoot()
  {
    var current = new DirectoryInfo(Directory.GetCurrentDirectory());
    while (current != null)
    {
      var hasSln = Directory.GetFiles(current.FullName, "*.sln").Any();
      if (hasSln)
      {
        return current.FullName;
      }
      current = current.Parent;
    }

    // If we can't find it, check if we're in the tools directory
    var toolsPath = Directory.GetCurrentDirectory();
    if (toolsPath.Contains("tools") && toolsPath.Contains("model-generator"))
    {
      return Path.GetFullPath(Path.Combine(toolsPath, "..", ".."));
    }

    throw new InvalidOperationException("Could not find project root (looking for .sln file)");
  }
}
