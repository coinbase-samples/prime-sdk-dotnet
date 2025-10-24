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

using Microsoft.Extensions.Logging;
using CoinbaseSdk.Tools.ModelGenerator;

// Configure logging
using var loggerFactory = LoggerFactory.Create(builder =>
{
  builder
    .AddConsole()
    .SetMinimumLevel(LogLevel.Information);
});

var logger = loggerFactory.CreateLogger<Program>();

try
{
  logger.LogInformation("Coinbase Prime .NET SDK Model Generator");
  logger.LogInformation("========================================");
  logger.LogInformation("Mode: FULL GENERATION (regenerates all models from OpenAPI spec)");
  logger.LogInformation("");

  // Find project root
  var projectRoot = FindProjectRoot();
  var specUrl = "https://api.prime.coinbase.com/v1/openapi.yaml";
  var outputDir = Path.Combine(projectRoot, "src", "CoinbaseSdk", "Prime", "model");
  var enumsDir = Path.Combine(outputDir, "enums");
  var tempDir = Path.Combine(projectRoot, "generated");

  logger.LogInformation("Project Root: {Path}", projectRoot);
  logger.LogInformation("OpenAPI Spec URL: {Url}", specUrl);
  logger.LogInformation("Output Directory: {Path}", outputDir);
  logger.LogInformation("Enums Directory: {Path}", enumsDir);
  logger.LogInformation("Temp Directory: {Path}", tempDir);

  // Phase 1: Generate raw models using OpenAPI Generator
  logger.LogInformation("");
  logger.LogInformation("Phase 1: Generating raw models with OpenAPI Generator CLI...");
  var generatorLogger = loggerFactory.CreateLogger<OpenApiGenerator>();
  var generator = new OpenApiGenerator(generatorLogger, specUrl, tempDir);
  await generator.GenerateModelsAsync();

  // Phase 2: Post-process models to match existing patterns
  logger.LogInformation("");
  logger.LogInformation("Phase 2: Post-processing models...");
  var postProcessorLogger = loggerFactory.CreateLogger<PostProcessor>();
  var postProcessor = new PostProcessor(postProcessorLogger, tempDir, outputDir, enumsDir);
  await postProcessor.ProcessModelsAsync();

  logger.LogInformation("");
  logger.LogInformation("Model generation completed successfully!");
  logger.LogInformation("");
  logger.LogInformation("Generated models are in: {Path}", outputDir);
  logger.LogInformation("Generated enums are in: {Path}", enumsDir);
  logger.LogInformation("");
  logger.LogInformation("Next steps:");
  logger.LogInformation("1. Review the generated models");
  logger.LogInformation("2. Run tests to ensure everything works");
  logger.LogInformation("3. Build the project: dotnet build");
}
catch (Exception ex)
{
  logger.LogError(ex, "Error during model generation");
  Environment.Exit(1);
}

static string FindProjectRoot()
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
