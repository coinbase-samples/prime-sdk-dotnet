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

using Microsoft.Extensions.Logging;

namespace CoinbaseSdk.Tools.Generator.Processing;

/// <summary>
/// Keeps hand-maintained runnable examples aligned with request emission from RequestPhase:
/// nested request builders are named <c>{SdkMethod}RequestBuilder</c>, not <c>Builder</c>.
/// </summary>
public static class ExampleRequestBuilderNamingSync
{
  /// <summary>
  /// For each <c>examples/.../{SdkMethod}.cs</c>, replaces
  /// <c>{SdkMethod}Request.Builder</c> with <c>{SdkMethod}Request.{SdkMethod}RequestBuilder</c>.
  /// Does not affect model types (e.g. <c>AllocationLeg.Builder</c>).
  /// </summary>
  public static void Run(ILogger logger, string examplesRoot)
  {
    if (!Directory.Exists(examplesRoot))
    {
      return;
    }

    foreach (var file in Directory.EnumerateFiles(examplesRoot, "*.cs", SearchOption.AllDirectories))
    {
      var sdkMethod = Path.GetFileNameWithoutExtension(file);
      var requestPrefix = $"{sdkMethod}Request";
      var oldToken = $"{requestPrefix}.Builder";
      var newToken = $"{requestPrefix}.{sdkMethod}RequestBuilder";

      var original = File.ReadAllText(file);
      if (!original.Contains(oldToken, StringComparison.Ordinal))
      {
        continue;
      }

      var updated = original.Replace(oldToken, newToken, StringComparison.Ordinal);
      if (!string.Equals(original, updated, StringComparison.Ordinal))
      {
        File.WriteAllText(file, updated);
        logger.LogInformation("EXAMPLE synced builder name: {Path}", file);
      }
    }
  }
}
