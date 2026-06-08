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

public static class OperationBindingValidator
{
  public static void ValidateOperationBindings(
    ILogger logger,
    ParsedOpenApiDocument document,
    OperationBindingMergeResult merge)
  {
    var merged = merge.Merged;
    var missing = merged
      .Where(b => !document.OperationsById.ContainsKey(b.OperationId))
      .Select(b => b.OperationId)
      .Distinct(StringComparer.Ordinal)
      .OrderBy(x => x, StringComparer.Ordinal)
      .ToList();
    if (missing.Count > 0)
    {
      throw new InvalidOperationException(
        "Merged operation bindings reference operationId values that are not in the OpenAPI spec: " +
        string.Join(", ", missing));
    }

    var derivedById = merge.Derived.ToDictionary(b => b.OperationId, StringComparer.Ordinal);
    foreach (var patch in merge.Patches)
    {
      if (!document.OperationsById.ContainsKey(patch.OperationId))
      {
        throw new InvalidOperationException(
          $"operations-overrides.json references operationId '{patch.OperationId}' that is not in the OpenAPI spec.");
      }

      if (!derivedById.TryGetValue(patch.OperationId, out var derived))
      {
        continue;
      }

      if (PatchMatchesDerived(patch, derived))
      {
        logger.LogWarning(
          "operations-overrides.json entry for operationId {OperationId} matches derived defaults and can be removed.",
          patch.OperationId);
      }
    }
  }

  private static bool PatchMatchesDerived(SdkOperationBindingPatch patch, SdkOperationBinding derived)
  {
    var any = false;
    if (patch.SdkMethod != null)
    {
      any = true;
      if (!string.Equals(patch.SdkMethod, derived.SdkMethod, StringComparison.Ordinal))
      {
        return false;
      }
    }

    if (patch.Service != null)
    {
      any = true;
      if (!string.Equals(patch.Service, derived.Service, StringComparison.Ordinal))
      {
        return false;
      }
    }

    if (patch.OmitRequest.HasValue)
    {
      any = true;
      if (patch.OmitRequest.Value != derived.OmitRequest)
      {
        return false;
      }
    }

    if (patch.ForcePaginated.HasValue)
    {
      any = true;
      if (patch.ForcePaginated.Value != derived.ForcePaginated)
      {
        return false;
      }
    }

    if (patch.ParamTypeOverrides != null && patch.ParamTypeOverrides.Count > 0)
    {
      any = true;
      foreach (var kv in patch.ParamTypeOverrides)
      {
        if (!derived.ParamTypeOverrides.TryGetValue(kv.Key, out var v) ||
            !string.Equals(kv.Value, v, StringComparison.Ordinal))
        {
          return false;
        }
      }
    }

    return any;
  }
}
