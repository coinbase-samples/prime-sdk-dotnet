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

using CoinbaseSdk.Tools.Generator;
using CoinbaseSdk.Tools.Generator.Phases;
using CoinbaseSdk.Tools.Generator.Processing;
using CoinbaseSdk.Tools.Generator.Spec;
using Microsoft.Extensions.Logging;

var dryRun = args.Contains("--dry-run", StringComparer.Ordinal);
var diffMode = args.Contains("--diff", StringComparer.Ordinal);
var liveFetch = args.Contains("--live", StringComparer.Ordinal) ||
                args.Contains("--fetch-spec", StringComparer.Ordinal);

using var loggerFactory = LoggerFactory.Create(builder =>
{
  builder
    .AddConsole()
    .SetMinimumLevel(LogLevel.Information);
});

var logger = loggerFactory.CreateLogger<Program>();

try
{
  logger.LogInformation("Coinbase Prime .NET SDK Generator");
  logger.LogInformation("===================================");
  if (dryRun)
  {
    logger.LogInformation("Mode: DRY-RUN (no files written)");
  }
  else if (diffMode)
  {
    logger.LogInformation("Mode: DIFF (compare to existing files)");
  }
  else
  {
    logger.LogInformation("Mode: WRITE (full holistic generation)");
  }

  var projectRoot = GeneratorPaths.FindProjectRoot();
  CopyrightHelper.InitializeSdkEmittedCopyrightYear(projectRoot);
  var cfg = GeneratorConfiguration.Load(projectRoot);

  var committedSpecPath = string.IsNullOrWhiteSpace(cfg.CommittedSpecPath)
    ? GeneratorPaths.DefaultCommittedSpecPath(projectRoot)
    : Path.Combine(projectRoot, cfg.CommittedSpecPath);
  var cachedSpecPath = GeneratorPaths.CachedSpecPath(projectRoot);
  string specPath;
  if (liveFetch)
  {
    Directory.CreateDirectory(Path.GetDirectoryName(cachedSpecPath)!);
    logger.LogInformation("Downloading OpenAPI spec from {Url} to {Path}...", cfg.SpecUrl, cachedSpecPath);
    using (var http = new HttpClient())
    {
      var yaml = await http.GetStringAsync(cfg.SpecUrl);
      await File.WriteAllTextAsync(cachedSpecPath, yaml);
    }

    specPath = cachedSpecPath;
  }
  else if (File.Exists(committedSpecPath))
  {
    specPath = committedSpecPath;
    logger.LogInformation("Using committed OpenAPI spec at {Path}", specPath);
  }
  else if (File.Exists(cachedSpecPath))
  {
    specPath = cachedSpecPath;
    logger.LogWarning("Committed spec missing; falling back to cached spec at {Path}", specPath);
  }
  else
  {
    throw new FileNotFoundException(
      $"OpenAPI spec not found. Run `make fetch-spec` or pass --live to download from {cfg.SpecUrl}.",
      committedSpecPath);
  }

  logger.LogInformation("Parsing OpenAPI YAML for client surface...");
  var document = await SpecParser.LoadAsync(specPath);
  SpecAnalyzer.Apply(document, cfg, logger);
  var transforms = new SharedTransforms(cfg);

  var primeRoot = Path.Combine(projectRoot, "src", "CoinbaseSdk", "Prime");
  var modelDir = Path.Combine(primeRoot, "model");
  var commonDir = Path.Combine(primeRoot, "common");
  var enumsDir = Path.Combine(modelDir, "enums");
  var tempDir = Path.Combine(projectRoot, "generated", "model-cli");

  if (!dryRun && !diffMode)
  {
    await ModelEnumPhase.RunAsync(
      loggerFactory,
      projectRoot,
      specPath,
      transforms,
      tempDir,
      modelDir,
      commonDir,
      enumsDir,
      cfg.CommonModels,
      cfg);
  }
  else
  {
    logger.LogInformation("Skipping model/enum CLI phase (--dry-run or --diff).");
  }

  var bindingMerge = GeneratorConfiguration.MergeOperationBindings(document, cfg, transforms, projectRoot);
  OperationBindingValidator.ValidateOperationBindings(logger, document, bindingMerge);
  var operations = bindingMerge.Merged;

  var clientLogger = loggerFactory.CreateLogger<ClientSurfacePhase>();
  var clientPhase = new ClientSurfacePhase(clientLogger, document, cfg, transforms, primeRoot);
  await clientPhase.RunAsync(operations, dryRun, diffMode);

  var examplesRoot = Path.Combine(projectRoot, "src", "CoinbaseSdk", "PrimeExample", "examples");
  if (!dryRun && !diffMode)
  {
    ExampleRequestBuilderNamingSync.Run(logger, examplesRoot);
  }

  var exampleLogger = loggerFactory.CreateLogger<ExamplePhase>();
  var examplePhase = new ExamplePhase(exampleLogger, document, cfg, transforms, examplesRoot);
  await examplePhase.RunAsync(operations, dryRun, diffMode);

  logger.LogInformation("");
  logger.LogInformation("Holistic generation finished.");
  if (!dryRun && !diffMode)
  {
    logger.LogInformation("Next: dotnet build prime-sdk-dotnet.sln && dotnet test src/CoinbaseSdk/Prime.Tests");
  }
}
catch (Exception ex)
{
  logger.LogError(ex, "Generator failed");
  Environment.Exit(1);
}
