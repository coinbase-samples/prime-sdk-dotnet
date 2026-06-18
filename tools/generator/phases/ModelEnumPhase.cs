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

using CoinbaseSdk.Tools.Generator.Processing;
using Microsoft.Extensions.Logging;

namespace CoinbaseSdk.Tools.Generator.Phases;

public class ModelEnumPhase
{
  public static async Task RunAsync(
    ILoggerFactory loggerFactory,
    string projectRoot,
    string specInputPath,
    SharedTransforms transforms,
    string tempDir,
    string modelOutputDir,
    string commonOutputDir,
    string enumsDir,
    IReadOnlyDictionary<string, string> commonModels,
    GeneratorConfiguration configuration)
  {
    var genLogger = loggerFactory.CreateLogger<OpenApiGenerator>();
    var openapi = new OpenApiGenerator(genLogger, projectRoot, specInputPath, tempDir);
    await openapi.GenerateModelsAsync();

    var postLogger = loggerFactory.CreateLogger<ModelPostProcessor>();
    var post = new ModelPostProcessor(
      postLogger,
      transforms,
      tempDir,
      modelOutputDir,
      commonOutputDir,
      enumsDir,
      commonModels,
      specInputPath,
      configuration);
    await post.ProcessModelsAsync();
  }
}
