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

using System.Collections.Generic;
using System.Linq;
using CoinbaseSdk.Tools.Generator.Spec;
using YamlDotNet.RepresentationModel;

namespace CoinbaseSdk.Tools.Generator.Processing;

public static class OperationBindingGenerator
{
  private const string OperationIdPrefix = "PrimeRESTAPI_";

  public static List<SdkOperationBinding> DeriveAll(
    ParsedOpenApiDocument doc,
    GeneratorConfiguration cfg,
    SharedTransforms transforms)
  {
    var list = new List<SdkOperationBinding>();
    foreach (var op in doc.OperationsById.Values.OrderBy(o => o.OperationId, StringComparer.Ordinal))
    {
      list.Add(DeriveOne(doc.Root, cfg, transforms, op));
    }

    return list;
  }

  private static SdkOperationBinding DeriveOne(
    YamlMappingNode root,
    GeneratorConfiguration cfg,
    SharedTransforms transforms,
    ParsedOperation op)
  {
    var sdkMethod = DeriveSdkMethod(op, transforms);
    var service = ResolveServiceKey(cfg, op);
    var omitRequest = DeriveOmitRequest(op);
    var paramOverrides = DeriveParamTypeOverrides(root, transforms, op);
    var forcePaginated = DeriveForcePaginated(root, op);

    return new SdkOperationBinding
    {
      OperationId = op.OperationId,
      SdkMethod = sdkMethod,
      Service = service,
      OmitRequest = omitRequest,
      ForcePaginated = forcePaginated,
      ParamTypeOverrides = paramOverrides
    };
  }

  private static string DeriveSdkMethod(ParsedOperation op, SharedTransforms transforms)
  {
    if (!string.IsNullOrWhiteSpace(op.ExtensionSdkMethodName))
    {
      var ext = op.ExtensionSdkMethodName.Trim();
      ext = transforms.NormalizeAcronyms(ext);
      return transforms.ApplyWeb3ToOnchainName(ext);
    }

    var name = op.OperationId;
    if (name.StartsWith(OperationIdPrefix, StringComparison.Ordinal))
    {
      name = name[OperationIdPrefix.Length..];
    }

    name = transforms.NormalizeAcronyms(name);
    name = transforms.ApplyWeb3ToOnchainName(name);

    if (op.HttpMethod == "GET" &&
        !string.IsNullOrEmpty(op.Summary) &&
        op.Summary.StartsWith("List ", StringComparison.Ordinal) &&
        name.StartsWith("Get", StringComparison.Ordinal))
    {
      name = string.Concat("List", name.AsSpan(3));
    }

    name = ApplyPortfolioPathPrefix(name, op);
    return name;
  }

  /// <summary>
  /// Portfolio-scoped paths use <c>GetPortfolio…</c> only for selected OpenAPI operation names
  /// (broad <c>Get</c>+portfolio injection would rename e.g. <c>GetAllocation</c> incorrectly).
  /// </summary>
  private static readonly HashSet<string> PortfolioScopedGetSuffixes = new(StringComparer.Ordinal)
  {
    "BuyingPower",
    "WithdrawalPower"
  };

  private static string ApplyPortfolioPathPrefix(string name, ParsedOperation op)
  {
    var path = op.Path;
    if (!path.Contains("{portfolio_id}", StringComparison.Ordinal) &&
        !path.Contains("/portfolios/", StringComparison.Ordinal))
    {
      return name;
    }

    if (!name.StartsWith("Get", StringComparison.Ordinal) ||
        name.Contains("Portfolio", StringComparison.Ordinal) ||
        name.Length <= 3)
    {
      return name;
    }

    var rest = name[3..];
    if (!PortfolioScopedGetSuffixes.Contains(rest))
    {
      return name;
    }

    return "GetPortfolio" + rest;
  }

  private static string ResolveServiceKey(GeneratorConfiguration cfg, ParsedOperation op)
  {
    foreach (var tag in op.Tags)
    {
      if (cfg.TagToFolder.TryGetValue(tag, out var folder) &&
          cfg.Services.ContainsKey(folder))
      {
        return folder;
      }
    }

    throw new InvalidOperationException(
      $"Operation '{op.OperationId}' has no tag mapped in derived tagToFolder " +
      $"(tags: {string.Join(", ", op.Tags.Select(t => "'" + t + "'"))}).");
  }

  private static bool DeriveOmitRequest(ParsedOperation op)
  {
    return op.Parameters.Count == 0 && op.RequestBodyJsonSchema == null;
  }

  private static bool DeriveForcePaginated(YamlMappingNode root, ParsedOperation op)
  {
    return op.HttpMethod == "GET" && SpecResponseSchema.ResponseSchemaSuggestsPagination(root, op);
  }

  /// <summary>
  /// Derives query-parameter CLR overrides (e.g. <c>symbols</c> as array, enum <c>$ref</c> parameters).
  /// </summary>
  private static Dictionary<string, string> DeriveParamTypeOverrides(
    YamlMappingNode root,
    SharedTransforms transforms,
    ParsedOperation op)
  {
    var result = new Dictionary<string, string>(StringComparer.Ordinal);
    foreach (var p in op.Parameters.Where(x => x.In == "query"))
    {
      if (p.Name == "symbols" &&
          p.Schema.Children.ContainsKey(new YamlScalarNode("type")) &&
          p.Schema.Children[new YamlScalarNode("type")] is YamlScalarNode tn &&
          tn.Value == "string")
      {
        var symbolsDefaultClr = OpenApiSchemaCodegen.ToClrType(root, p.Schema, transforms, out _, out _);
        if (symbolsDefaultClr != "string[]")
        {
          result["symbols"] = "string[]";
        }
      }
    }

    return result;
  }
}
