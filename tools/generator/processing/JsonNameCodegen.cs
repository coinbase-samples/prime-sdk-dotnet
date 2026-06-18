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

using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace CoinbaseSdk.Tools.Generator.Processing;

public static class JsonNameCodegen
{
  private static readonly JsonNamingPolicy SnakeCasePolicy = JsonNamingPolicy.SnakeCaseLower;

  /// <summary>
  /// Prime HTTP serialization uses snake_case naming policy (<see cref="PrimeJsonDefaults"/>).
  /// Emit <c>[JsonPropertyName]</c> only for wire names the policy cannot infer (renames, <c>web3</c>, etc.).
  /// </summary>
  public static bool NeedsJsonPropertyName(string jsonName, string clrPropertyName)
  {
    if (string.Equals(jsonName, ToSnakeCase(clrPropertyName), StringComparison.Ordinal))
    {
      return false;
    }

    if (jsonName.Equals(clrPropertyName, StringComparison.OrdinalIgnoreCase))
    {
      return false;
    }

    if (jsonName.Equals(ToCamelCase(clrPropertyName), StringComparison.Ordinal))
    {
      return false;
    }

    return true;
  }

  public static bool UsesSystemTextJsonSerialization(string content)
  {
    return content.Contains("[JsonPropertyName(", StringComparison.Ordinal) ||
           content.Contains("[JsonIgnore]", StringComparison.Ordinal) ||
           content.Contains("[JsonConverter(", StringComparison.Ordinal);
  }

  public static string ToSnakeCase(string clrPropertyName)
  {
    return SnakeCasePolicy.ConvertName(clrPropertyName);
  }

  public static string ToCamelCase(string clrPropertyName)
  {
    if (string.IsNullOrEmpty(clrPropertyName))
    {
      return clrPropertyName;
    }

    return char.ToLowerInvariant(clrPropertyName[0]) + clrPropertyName[1..];
  }

  public static void AppendJsonPropertyNameIfNeeded(StringBuilder sb, string jsonName, string clrPropertyName, string indent)
  {
    if (NeedsJsonPropertyName(jsonName, clrPropertyName))
    {
      sb.AppendLine($"{indent}[JsonPropertyName(\"{jsonName}\")]");
    }
  }

  public static string StripRedundantJsonPropertyNames(string content)
  {
    var lines = content.Split('\n');
    var builder = new StringBuilder();
    for (var i = 0; i < lines.Length; i++)
    {
      var line = lines[i].TrimEnd('\r');
      var attributeMatch = Regex.Match(line, @"^\s*\[JsonPropertyName\(""([^""]+)""\)\]\s*$");
      if (attributeMatch.Success && i + 1 < lines.Length)
      {
        var nextLine = lines[i + 1].TrimEnd('\r');
        var propertyMatch = Regex.Match(nextLine, @"^\s*public\s+.+\s+(\w+)\s+\{");
        if (propertyMatch.Success)
        {
          var jsonName = attributeMatch.Groups[1].Value;
          var clrName = propertyMatch.Groups[1].Value;
          if (!NeedsJsonPropertyName(jsonName, clrName))
          {
            continue;
          }
        }
      }

      builder.Append(line);
      if (i < lines.Length - 1)
      {
        builder.Append('\n');
      }
    }

    return builder.ToString();
  }

  public static string StripUnusedSerializationUsings(string content)
  {
    if (UsesSystemTextJsonSerialization(content))
    {
      return content;
    }

    return Regex.Replace(
      content,
      @"^[ \t]*using System\.Text\.Json\.Serialization;\r?\n",
      string.Empty,
      RegexOptions.Multiline);
  }

  public static string PostProcessEmittedSource(string content)
  {
    return EmittedSourceNormalizer.Normalize(
      SharedTransforms.DeduplicateUsings(
        StripUnusedSerializationUsings(
          StripRedundantJsonPropertyNames(content))));
  }
}
