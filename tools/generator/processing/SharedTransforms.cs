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

using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CoinbaseSdk.Tools.Generator.Processing;

public class SharedTransforms
{
  private readonly GeneratorConfiguration _cfg;

  public SharedTransforms(GeneratorConfiguration cfg)
  {
    _cfg = cfg;
  }

  public string ApplyContentReplacements(string content)
  {
    foreach (var replacement in _cfg.ContentReplacements.OrderByDescending(e => e.Key.Length))
    {
      content = content.Replace(replacement.Key, replacement.Value, StringComparison.Ordinal);
    }

    return NormalizeAcronymsInContent(content);
  }

  public string StripCommonPrefixes(string className)
  {
    var result = className;
    foreach (var entry in _cfg.FilePathReplacements.OrderByDescending(e => e.Key.Length))
    {
      if (result.Contains(entry.Key, StringComparison.Ordinal))
      {
        result = result.Replace(entry.Key, entry.Value, StringComparison.Ordinal);
      }
    }

    return NormalizeAcronyms(result);
  }

  public string TransformSchemaRefToClrName(string refName)
  {
    // Convert the full dotted ref (e.g. coinbase.custody.api.ActivityType) into a single
    // PascalCase identifier (CoinbaseCustodyApiActivityType) so that namespace-qualified
    // content replacements like "CoinbaseCustodyApiActivityType" → "CustodyActivityType" apply.
    // Each dot-segment is itself converted to PascalCase (underscores handled like snake_case).
    var dotSegments = refName.Split('.', StringSplitOptions.RemoveEmptyEntries);
    var sb = new System.Text.StringBuilder();
    foreach (var dotSeg in dotSegments)
    {
      var underscoreParts = dotSeg.Split('_', StringSplitOptions.RemoveEmptyEntries);
      foreach (var part in underscoreParts)
      {
        if (part.Length == 0) continue;
        sb.Append(char.ToUpperInvariant(part[0]));
        sb.Append(part.AsSpan(1));
      }
    }

    var name = sb.ToString();
    name = StripCommonPrefixes(name);
    name = ApplyContentReplacements(name);
    name = name.Replace(".", string.Empty, StringComparison.Ordinal);
    return ApplyWeb3ToOnchainName(name);
  }

  public string ApplyWeb3ToOnchainContent(string content, string className)
  {
    if (!content.Contains("Web3", StringComparison.Ordinal) &&
        !content.Contains("web3", StringComparison.Ordinal))
    {
      return content;
    }

    // Preserve OpenAPI wire names inside JsonPropertyName literals (e.g. web3_transaction_metadata).
    var wireNames = new List<string>();
    content = Regex.Replace(
      content,
      @"\[JsonPropertyName\(""([^""]*)""\)\]",
      match =>
      {
        wireNames.Add(match.Groups[1].Value);
        return $"[JsonPropertyName(\"__WIRE_{wireNames.Count - 1}__\")]";
      });

    content = Regex.Replace(content, @"\bWeb3", "Onchain");
    content = Regex.Replace(content, @"\bweb3", "onchain");

    for (var i = 0; i < wireNames.Count; i++)
    {
      content = content.Replace($"__WIRE_{i}__", wireNames[i], StringComparison.Ordinal);
    }

    return content;
  }

  public string ApplyWeb3ToOnchainName(string name)
  {
    return name.Replace("Web3", "Onchain", StringComparison.Ordinal);
  }

  public string NormalizeAcronyms(string value)
  {
    var result = value;
    foreach (var pair in _cfg.AcronymMappings)
    {
      var acronym = pair.Acronym;
      var normalized = pair.Normalized;
      result = Regex.Replace(result, $@"\b{Regex.Escape(acronym)}(?=[A-Z])", normalized);
      if (result.EndsWith(acronym, StringComparison.Ordinal))
      {
        result = string.Concat(result.AsSpan(0, result.Length - acronym.Length), normalized);
      }
    }

    return result;
  }

  public string NormalizeAcronymsInContent(string content)
  {
    var result = content;
    foreach (var pair in _cfg.AcronymMappings)
    {
      var acronym = pair.Acronym;
      var normalized = pair.Normalized;
      result = Regex.Replace(result, $@"\b{Regex.Escape(acronym)}(?=[A-Z](?!_))", normalized);
      result = Regex.Replace(result, $@"\b{Regex.Escape(acronym)}(?=\s+[a-z])", normalized);
      result = Regex.Replace(result, $@"\b{Regex.Escape(acronym)}(?=[<>;])", normalized);
      result = Regex.Replace(result, $@"\b{Regex.Escape(acronym)}(?=\.)", normalized);
    }

    return result;
  }

  public string FixConstructorNames(string content, string className)
  {
    foreach (var pair in _cfg.AcronymMappings)
    {
      var acronym = pair.Acronym;
      var normalized = pair.Normalized;
      if (className == acronym)
      {
        content = Regex.Replace(content, $@"public {normalized}\s*\(", $"public {acronym}(");
        content = Regex.Replace(content, $@"new {normalized}\s*\(", $"new {acronym}(");
        content = Regex.Replace(content, $@"public {normalized} Build\(\)", $"public {acronym} Build()");
      }
      else if (className == normalized)
      {
        content = Regex.Replace(content, $@"public {acronym}\s*\(", $"public {normalized}(");
        content = Regex.Replace(content, $@"new {acronym}\s*\(", $"new {normalized}(");
        content = Regex.Replace(content, $@"public {acronym} Build\(\)", $"public {normalized} Build()");
      }
    }

    return content;
  }

  public string ApplyEnumMappings(string content, IReadOnlySet<string> actualEnumNames)
  {
    foreach (var mapping in _cfg.EnumNameMappings)
    {
      var strippedName = mapping.Key;
      var actualEnumName = mapping.Value;
      if (actualEnumNames.Contains(actualEnumName))
      {
        content = Regex.Replace(
          content,
          $@"\b{Regex.Escape(strippedName)}\b(?![^\[]*\[JsonPropertyName)",
          actualEnumName);
      }
    }

    var referencesEnum = actualEnumNames.Any(enumName =>
      Regex.IsMatch(content, $@"\b{Regex.Escape(enumName)}\b"));
    if (referencesEnum && !content.Contains("using CoinbaseSdk.Prime.Model.Enums;", StringComparison.Ordinal))
    {
      if (content.Contains("using System.Text.Json.Serialization;", StringComparison.Ordinal))
      {
        content = content.Replace(
          "using System.Text.Json.Serialization;\n",
          "using System.Text.Json.Serialization;\n  using CoinbaseSdk.Prime.Model.Enums;\n",
          StringComparison.Ordinal);
      }
      else
      {
        content = Regex.Replace(
          content,
          @"(namespace\s+CoinbaseSdk\.Prime\.Model\s*\{?\s*\n)",
          "$1  using CoinbaseSdk.Prime.Model.Enums;\n");
      }
    }

    return content;
  }

  public static string DeduplicateUsings(string content)
  {
    var lines = content.Split('\n');
    var builder = new StringBuilder();
    var usingRun = new List<string>();

    void FlushUsingRun()
    {
      if (usingRun.Count == 0)
      {
        return;
      }

      var unique = new Dictionary<string, string>(StringComparer.Ordinal);
      foreach (var line in usingRun)
      {
        var trimmed = line.Trim();
        if (!unique.ContainsKey(trimmed))
        {
          unique[trimmed] = line;
        }
      }

      var sorted = unique.Values
        .OrderBy(line =>
        {
          var ns = line.Trim()["using ".Length..^1].Trim();
          return ns.StartsWith("System", StringComparison.Ordinal) ? 0 : 1;
        })
        .ThenBy(line => line.Trim()["using ".Length..^1].Trim(), StringComparer.Ordinal)
        .ToList();

      foreach (var line in sorted)
      {
        builder.Append(line);
        builder.Append('\n');
      }

      usingRun.Clear();
    }

    foreach (var line in lines)
    {
      var trimmed = line.Trim();
      if (trimmed.StartsWith("using ", StringComparison.Ordinal) && trimmed.EndsWith(';'))
      {
        usingRun.Add(line);
        continue;
      }

      FlushUsingRun();
      builder.Append(line);
      builder.Append('\n');
    }

    FlushUsingRun();
    return builder.ToString().TrimEnd('\n') + '\n';
  }
}
