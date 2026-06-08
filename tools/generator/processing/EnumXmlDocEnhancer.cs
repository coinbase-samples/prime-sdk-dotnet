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
/// Injects XML documentation into generated enum types from <see cref="SchemaDocumentationIndex"/>.
/// </summary>
public static class EnumXmlDocEnhancer
{
  private static readonly Regex EnumDeclaration =
    new(@"^  public enum (\w+)", RegexOptions.Compiled);

  private static readonly Regex EnumMemberDeclaration =
    new(@"^    (\w+),?\s*$", RegexOptions.Compiled);

  public static string Apply(string content, string enumName, SchemaDocumentationIndex index)
  {
    var entry = index.TryGet(enumName);
    if (entry == null)
    {
      return content;
    }

    var lines = content.Replace("\r\n", "\n", StringComparison.Ordinal).Split('\n');
    var output = new List<string>(lines.Length + 16);

    for (var i = 0; i < lines.Length; i++)
    {
      var line = lines[i];

      var enumMatch = EnumDeclaration.Match(line);
      if (enumMatch.Success &&
          string.Equals(enumMatch.Groups[1].Value, enumName, StringComparison.Ordinal) &&
          !XmlDocInsertionHelper.HasTypeDocBeforeMember(output) &&
          !string.IsNullOrWhiteSpace(entry.TypeDoc))
      {
        XmlDocInsertionHelper.InsertTypeDocBeforeMember(
          output,
          GeneratorXmlDoc.FormatTypeSummary(entry.TypeDoc));
      }

      var memberMatch = EnumMemberDeclaration.Match(line);
      if (memberMatch.Success && !XmlDocInsertionHelper.HasSummaryDocAbove(output))
      {
        var memberName = memberMatch.Groups[1].Value;
        if (entry.EnumValueDocs.TryGetValue(memberName, out var memberDoc))
        {
          output.AddRange(XmlDocInsertionHelper.SplitLines(GeneratorXmlDoc.FormatEnumMemberSummary(memberDoc)));
        }
      }

      output.Add(line);
    }

    return string.Join('\n', output);
  }

  internal static Dictionary<string, string> ParseEnumValueDescriptions(string description)
  {
    var result = new Dictionary<string, string>(StringComparer.Ordinal);
    foreach (var line in description.Split('\n'))
    {
      var trimmed = line.Trim();
      if (!trimmed.StartsWith("- ", StringComparison.Ordinal))
      {
        continue;
      }

      var body = trimmed[2..].Trim();
      var colon = body.IndexOf(':');
      if (colon <= 0)
      {
        continue;
      }

      var key = body[..colon].Trim();
      var value = body[(colon + 1)..].Trim();
      if (key.Length > 0 && value.Length > 0)
      {
        result[key] = value;
      }
    }

    return result;
  }
}
