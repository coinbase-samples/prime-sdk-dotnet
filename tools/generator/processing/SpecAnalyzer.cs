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
using CoinbaseSdk.Tools.Generator.Spec;
using Microsoft.Extensions.Logging;
using YamlDotNet.RepresentationModel;

namespace CoinbaseSdk.Tools.Generator.Processing;

/// <summary>
/// Derives tag routing, service metadata, and common schema-name prefixes from the OpenAPI document.
/// </summary>
public static class SpecAnalyzer
{
  private const int MinPrefixLength = 6;
  private const int MinPrefixSchemaCount = 3;

  public static void Apply(
    ParsedOpenApiDocument doc,
    GeneratorConfiguration cfg,
    ILogger logger)
  {
    DiscoverAndMergeFilePathPrefixes(doc, cfg, logger);
    DeriveTagToFolderAndServices(doc, cfg, logger);
  }

  /// <summary>
  /// Default folder key for an OpenAPI tag (lowercase, no spaces).
  /// </summary>
  public static string DefaultFolderFromTag(string tag)
  {
    return string.Concat(tag.Split(' ', StringSplitOptions.RemoveEmptyEntries)
      .Select(p => p.ToLowerInvariant()));
  }

  private static void DeriveTagToFolderAndServices(
    ParsedOpenApiDocument doc,
    GeneratorConfiguration cfg,
    ILogger logger)
  {
    var usedTags = new HashSet<string>(StringComparer.Ordinal);
    foreach (var op in doc.OperationsById.Values)
    {
      foreach (var t in op.Tags)
      {
        usedTags.Add(t);
      }
    }

    var tagToFolder = new Dictionary<string, string>(StringComparer.Ordinal);
    foreach (var tag in usedTags.OrderBy(t => t, StringComparer.Ordinal))
    {
      if (cfg.TagToFolderOverrides.TryGetValue(tag, out var folderOverride) &&
          !string.IsNullOrWhiteSpace(folderOverride))
      {
        tagToFolder[tag] = folderOverride.Trim();
      }
      else
      {
        tagToFolder[tag] = DefaultFolderFromTag(tag);
      }
    }

    cfg.TagToFolder = tagToFolder;

    var services = new Dictionary<string, ServiceDefinition>(StringComparer.Ordinal);
    foreach (var op in doc.OperationsById.Values.OrderBy(o => o.OperationId, StringComparer.Ordinal))
    {
      foreach (var tag in op.Tags)
      {
        if (!tagToFolder.TryGetValue(tag, out var folder))
        {
          continue;
        }

        if (services.ContainsKey(folder))
        {
          continue;
        }

        var canonicalTag = FindCanonicalTagForFolder(usedTags, tagToFolder, folder);
        services[folder] = BuildServiceDefinition(canonicalTag, folder);
      }
    }

    cfg.Services = services;
    logger.LogInformation(
      "Derived {TagCount} tag folder mappings and {ServiceCount} services from the OpenAPI spec.",
      tagToFolder.Count,
      services.Count);
  }

  private static string FindCanonicalTagForFolder(
    IReadOnlySet<string> usedTags,
    Dictionary<string, string> tagToFolder,
    string folder)
  {
    foreach (var tag in usedTags.OrderBy(t => t, StringComparer.Ordinal))
    {
      if (!tagToFolder.TryGetValue(tag, out var mapped) ||
          !string.Equals(mapped, folder, StringComparison.Ordinal))
      {
        continue;
      }

      if (string.Equals(DefaultFolderFromTag(tag), folder, StringComparison.Ordinal))
      {
        return tag;
      }
    }

    return usedTags
      .Where(t => tagToFolder.TryGetValue(t, out var m) &&
                  string.Equals(m, folder, StringComparison.Ordinal))
      .OrderBy(t => t, StringComparer.Ordinal)
      .First();
  }

  private static ServiceDefinition BuildServiceDefinition(string tag, string folder)
  {
    var ns = NamespaceFromTag(tag);
    return new ServiceDefinition
    {
      Folder = folder,
      Namespace = ns,
      InterfaceName = "I" + ns + "Service",
      ClassName = ns + "Service"
    };
  }

  private static string NamespaceFromTag(string tag)
  {
    var parts = tag.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    return string.Concat(parts.Select(p =>
      p.Length == 0 ? string.Empty : char.ToUpperInvariant(p[0]) + p[1..].ToLowerInvariant()));
  }

  private static void DiscoverAndMergeFilePathPrefixes(
    ParsedOpenApiDocument doc,
    GeneratorConfiguration cfg,
    ILogger logger)
  {
    if (!doc.Root.Children.TryGetValue(new YamlScalarNode("components"), out var componentsNode) ||
        componentsNode is not YamlMappingNode components)
    {
      return;
    }

    if (!components.Children.TryGetValue(new YamlScalarNode("schemas"), out var schemasNode) ||
        schemasNode is not YamlMappingNode schemas)
    {
      return;
    }

    var names = schemas.Children.Keys
      .OfType<YamlScalarNode>()
      .Select(k => k.Value!)
      .Where(s => !string.IsNullOrEmpty(s))
      .Distinct(StringComparer.Ordinal)
      .ToList();

    var prefixHits = new Dictionary<string, int>(StringComparer.Ordinal);
    foreach (var name in names)
    {
      foreach (var prefix in EnumeratePascalPrefixes(name))
      {
        if (prefix.Length < MinPrefixLength)
        {
          continue;
        }

        prefixHits[prefix] = prefixHits.GetValueOrDefault(prefix) + 1;
      }
    }

    var candidates = prefixHits
      .Where(kv => kv.Value >= MinPrefixSchemaCount)
      .Select(kv => kv.Key)
      .OrderByDescending(p => p.Length)
      .ToList();

    var addedKeys = new List<string>();
    var added = 0;
    foreach (var prefix in candidates)
    {
      if (cfg.FilePathReplacements.ContainsKey(prefix))
      {
        continue;
      }

      if (names.Any(n => n.Equals(prefix, StringComparison.Ordinal)))
      {
        continue;
      }

      if (addedKeys.Any(a => a.StartsWith(prefix, StringComparison.Ordinal)))
      {
        continue;
      }

      cfg.FilePathReplacements[prefix] = string.Empty;
      addedKeys.Add(prefix);
      added++;
      logger.LogDebug("Discovered schema name prefix for stripping: {Prefix} ({Count} schemas)", prefix, prefixHits[prefix]);
    }

    if (added > 0)
    {
      logger.LogInformation("Merged {Count} auto-detected schema prefixes into filePathReplacements.", added);
    }
  }

  private static IEnumerable<string> EnumeratePascalPrefixes(string name)
  {
    if (string.IsNullOrEmpty(name))
    {
      yield break;
    }

    for (var i = 1; i < name.Length; i++)
    {
      if (char.IsUpper(name[i]))
      {
        yield return name[..i];
      }
    }

    yield return name;
  }
}
