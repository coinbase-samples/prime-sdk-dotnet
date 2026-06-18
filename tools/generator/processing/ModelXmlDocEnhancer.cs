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

using System.Text.RegularExpressions;

namespace CoinbaseSdk.Tools.Generator.Processing;

/// <summary>
/// Injects XML documentation into generated model classes from <see cref="SchemaDocumentationIndex"/>.
/// </summary>
public static class ModelXmlDocEnhancer
{
  private static readonly Regex ClassDeclaration =
    new(@"^  public class (\w+)", RegexOptions.Compiled);

  private static readonly Regex PropertyDeclaration =
    new(@"^    public .+ (\w+) \{ get; set; \}", RegexOptions.Compiled);

  private static readonly Regex JsonPropertyNameAttribute =
    new(@"^\s*\[JsonPropertyName\(""([^""]+)""\)\]\s*$", RegexOptions.Compiled);

  public static string Apply(string content, string className, SchemaDocumentationIndex index)
  {
    var entry = index.TryGet(className);
    if (entry == null)
    {
      return content;
    }

    var lines = content.Replace("\r\n", "\n", StringComparison.Ordinal).Split('\n');
    var output = new List<string>(lines.Length + 16);

    for (var i = 0; i < lines.Length; i++)
    {
      var line = lines[i];

      var classMatch = ClassDeclaration.Match(line);
      if (classMatch.Success &&
          string.Equals(classMatch.Groups[1].Value, className, StringComparison.Ordinal) &&
          !XmlDocInsertionHelper.HasTypeDocBeforeMember(output) &&
          !string.IsNullOrWhiteSpace(entry.TypeDoc))
      {
        XmlDocInsertionHelper.InsertTypeDocBeforeMember(
          output,
          GeneratorXmlDoc.FormatTypeSummary(entry.TypeDoc));
      }

      var propertyMatch = PropertyDeclaration.Match(line);
      if (propertyMatch.Success && !XmlDocInsertionHelper.HasSummaryDocAbove(output))
      {
        var propertyName = propertyMatch.Groups[1].Value;
        var wireName = ResolveWireName(lines, i, propertyName);
        if (entry.PropertyDocs.TryGetValue(wireName, out var propertyDoc))
        {
          output.AddRange(XmlDocInsertionHelper.SplitLines(GeneratorXmlDoc.FormatPropertySummary(propertyDoc)));
        }
      }

      output.Add(line);
    }

    return string.Join('\n', output);
  }

  private static string ResolveWireName(string[] lines, int propertyLineIndex, string propertyName)
  {
    for (var i = propertyLineIndex - 1; i >= 0 && i >= propertyLineIndex - 3; i--)
    {
      var trimmed = lines[i].Trim();
      if (trimmed.Length == 0)
      {
        continue;
      }

      var attributeMatch = JsonPropertyNameAttribute.Match(lines[i]);
      if (attributeMatch.Success)
      {
        return attributeMatch.Groups[1].Value;
      }

      break;
    }

    return JsonNameCodegen.ToSnakeCase(propertyName);
  }
}
