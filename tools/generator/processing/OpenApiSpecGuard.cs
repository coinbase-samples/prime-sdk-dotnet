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

using CoinbaseSdk.Tools.Generator.Spec;
using Microsoft.Extensions.Logging;

namespace CoinbaseSdk.Tools.Generator.Processing;

public static class OpenApiSpecGuard
{
  public static void ValidateOperationBindings(
    ILogger logger,
    ParsedOpenApiDocument document,
    IReadOnlyList<SdkOperationBinding> operations)
  {
    var missing = operations
      .Where(b => !document.OperationsById.ContainsKey(b.OperationId))
      .Select(b => b.OperationId)
      .Distinct(StringComparer.Ordinal)
      .OrderBy(x => x, StringComparer.Ordinal)
      .ToList();
    if (missing.Count > 0)
    {
      throw new InvalidOperationException(
        "operations.json references operationId values that are not in the OpenAPI spec: " +
        string.Join(", ", missing));
    }

    var bound = new HashSet<string>(operations.Select(o => o.OperationId), StringComparer.Ordinal);
    var unbound = document.OperationsById.Keys
      .Where(id => !bound.Contains(id))
      .OrderBy(x => x, StringComparer.Ordinal)
      .ToList();
    if (unbound.Count > 0)
    {
      logger.LogWarning(
        "OpenAPI spec lists {Count} operation(s) not present in operations.json (SDK may omit new endpoints until added): {Sample}",
        unbound.Count,
        string.Join(", ", unbound.Take(12)) + (unbound.Count > 12 ? ", …" : string.Empty));
    }
  }
}
