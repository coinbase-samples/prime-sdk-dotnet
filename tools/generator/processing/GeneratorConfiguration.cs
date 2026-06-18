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

using System.Text.Json;
using System.Text.Json.Serialization;
using CoinbaseSdk.Tools.Generator;
using CoinbaseSdk.Tools.Generator.Spec;

namespace CoinbaseSdk.Tools.Generator.Processing;

public class GeneratorConfiguration
{
  [JsonPropertyName("specUrl")]
  public string SpecUrl { get; set; } = "https://api.prime.coinbase.com/v1/openapi.yaml";

  [JsonPropertyName("committedSpecPath")]
  public string CommittedSpecPath { get; set; } = "apiSpec/prime-public-api-spec.yaml";

  /// <summary>
  /// When true (default), request DTOs include nested fluent <c>*RequestBuilder</c> classes.
  /// Set false to emit property-only requests (breaking change for callers using builders).
  /// </summary>
  [JsonPropertyName("emitRequestBuilders")]
  public bool EmitRequestBuilders { get; set; } = true;

  /// <summary>
  /// OpenAPI component schemas (matched by filename substring) emitted under
  /// <c>src/CoinbaseSdk/Prime/common/</c> instead of <c>model/</c>.
  /// Values are CLR type names (e.g. PaginatedResponse → Pagination).
  /// </summary>
  [JsonPropertyName("commonModels")]
  public Dictionary<string, string> CommonModels { get; set; } = new();

  [JsonPropertyName("filePathReplacements")]
  public Dictionary<string, string> FilePathReplacements { get; set; } = new();

  [JsonPropertyName("contentReplacements")]
  public Dictionary<string, string> ContentReplacements { get; set; } = new();

  [JsonPropertyName("acronymMappings")]
  public List<AcronymMappingEntry> AcronymMappings { get; set; } = new();

  [JsonPropertyName("enumNameMappings")]
  public Dictionary<string, string> EnumNameMappings { get; set; } = new();

  /// <summary>
  /// Optional tag → folder overrides when <see cref="SpecAnalyzer.DefaultFolderFromTag"/> is not enough (e.g. route a tag to an existing service folder).
  /// </summary>
  [JsonPropertyName("tagToFolderOverrides")]
  public Dictionary<string, string> TagToFolderOverrides { get; set; } = new();

  /// <summary>
  /// Populated by <see cref="SpecAnalyzer"/> from spec tags plus <see cref="TagToFolderOverrides"/>.
  /// </summary>
  [JsonIgnore]
  public Dictionary<string, string> TagToFolder { get; set; } = new();

  /// <summary>
  /// Populated by <see cref="SpecAnalyzer"/> from derived folders and canonical tag names.
  /// </summary>
  [JsonIgnore]
  public Dictionary<string, ServiceDefinition> Services { get; set; } = new();

  public static GeneratorConfiguration Load(string projectRoot)
  {
    var path = Path.Combine(GeneratorPaths.ConfigDirectory(projectRoot), "generator-config.json");
    var json = File.ReadAllText(path);
    var cfg = JsonSerializer.Deserialize<GeneratorConfiguration>(json, JsonOptions())
               ?? throw new InvalidOperationException("Failed to deserialize generator-config.json");
    return cfg;
  }

  public static List<SdkOperationBindingPatch> LoadOperationBindingOverrides(string projectRoot)
  {
    var path = Path.Combine(GeneratorPaths.ConfigDirectory(projectRoot), "operations-overrides.json");
    if (!File.Exists(path))
    {
      return new List<SdkOperationBindingPatch>();
    }

    var json = File.ReadAllText(path);
    return JsonSerializer.Deserialize<List<SdkOperationBindingPatch>>(json, JsonOptions())
           ?? new List<SdkOperationBindingPatch>();
  }

  public static OperationBindingMergeResult MergeOperationBindings(
    ParsedOpenApiDocument document,
    GeneratorConfiguration cfg,
    SharedTransforms transforms,
    string projectRoot)
  {
    var derived = OperationBindingGenerator.DeriveAll(document, cfg, transforms);
    var patches = LoadOperationBindingOverrides(projectRoot);
    var mergedById = derived.ToDictionary(
      b => b.OperationId,
      CloneBinding,
      StringComparer.Ordinal);
    foreach (var patch in patches)
    {
      if (!mergedById.TryGetValue(patch.OperationId, out var binding))
      {
        throw new InvalidOperationException(
          $"operations-overrides.json references operationId '{patch.OperationId}' that is not in the OpenAPI spec.");
      }

      ApplyOperationBindingPatch(binding, patch);
    }

    var merged = mergedById.Values.OrderBy(b => b.OperationId, StringComparer.Ordinal).ToList();
    return new OperationBindingMergeResult(derived, merged, patches);
  }

  private static SdkOperationBinding CloneBinding(SdkOperationBinding b)
  {
    return new SdkOperationBinding
    {
      OperationId = b.OperationId,
      SdkMethod = b.SdkMethod,
      Service = b.Service,
      OmitRequest = b.OmitRequest,
      ForcePaginated = b.ForcePaginated,
      ParamTypeOverrides = new Dictionary<string, string>(b.ParamTypeOverrides, StringComparer.Ordinal)
    };
  }

  private static void ApplyOperationBindingPatch(SdkOperationBinding binding, SdkOperationBindingPatch patch)
  {
    if (patch.SdkMethod != null)
    {
      binding.SdkMethod = patch.SdkMethod;
    }

    if (patch.Service != null)
    {
      binding.Service = patch.Service;
    }

    if (patch.OmitRequest.HasValue)
    {
      binding.OmitRequest = patch.OmitRequest.Value;
    }

    if (patch.ForcePaginated.HasValue)
    {
      binding.ForcePaginated = patch.ForcePaginated.Value;
    }

    if (patch.ParamTypeOverrides != null)
    {
      foreach (var kv in patch.ParamTypeOverrides)
      {
        binding.ParamTypeOverrides[kv.Key] = kv.Value;
      }
    }
  }

  private static JsonSerializerOptions JsonOptions()
  {
    return new JsonSerializerOptions
    {
      PropertyNameCaseInsensitive = true,
      ReadCommentHandling = JsonCommentHandling.Skip,
      AllowTrailingCommas = true
    };
  }
}

public sealed record OperationBindingMergeResult(
  IReadOnlyList<SdkOperationBinding> Derived,
  IReadOnlyList<SdkOperationBinding> Merged,
  IReadOnlyList<SdkOperationBindingPatch> Patches);

public class SdkOperationBindingPatch
{
  [JsonPropertyName("operationId")]
  public string OperationId { get; set; } = "";

  [JsonPropertyName("sdkMethod")]
  public string? SdkMethod { get; set; }

  [JsonPropertyName("service")]
  public string? Service { get; set; }

  [JsonPropertyName("omitRequest")]
  public bool? OmitRequest { get; set; }

  [JsonPropertyName("forcePaginated")]
  public bool? ForcePaginated { get; set; }

  [JsonPropertyName("paramTypeOverrides")]
  public Dictionary<string, string>? ParamTypeOverrides { get; set; }
}

public class AcronymMappingEntry
{
  [JsonPropertyName("acronym")]
  public string Acronym { get; set; } = "";

  [JsonPropertyName("normalized")]
  public string Normalized { get; set; } = "";
}

public class ServiceDefinition
{
  [JsonPropertyName("folder")]
  public string Folder { get; set; } = "";

  [JsonPropertyName("namespace")]
  public string Namespace { get; set; } = "";

  [JsonPropertyName("interfaceName")]
  public string InterfaceName { get; set; } = "";

  [JsonPropertyName("className")]
  public string ClassName { get; set; } = "";
}

public class SdkOperationBinding
{
  [JsonPropertyName("operationId")]
  public string OperationId { get; set; } = "";

  [JsonPropertyName("sdkMethod")]
  public string SdkMethod { get; set; } = "";

  [JsonPropertyName("service")]
  public string Service { get; set; } = "";

  [JsonPropertyName("omitRequest")]
  public bool OmitRequest { get; set; }

  /// <summary>
  /// OpenAPI parameter name → CLR type override (e.g. inline string-enums mapped to SDK enum types).
  /// </summary>
  [JsonPropertyName("paramTypeOverrides")]
  public Dictionary<string, string> ParamTypeOverrides { get; set; } = new();

  /// <summary>
  /// When true, the request extends <c>PaginatedRequest</c> even if the OpenAPI operation omits pagination query parameters.
  /// </summary>
  [JsonPropertyName("forcePaginated")]
  public bool ForcePaginated { get; set; }
}
