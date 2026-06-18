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
using System.Text.RegularExpressions;

namespace CoinbaseSdk.Tools.Generator.Processing;

/// <summary>
/// XML documentation comments derived from OpenAPI summaries, descriptions, and titles.
/// </summary>
public static class GeneratorXmlDoc
{
  public static string FormatTypeSummary(string? summary)
  {
    // Two spaces inside namespace block (after usings), before type declarations.
    return FormatSummary(summary, baseIndentSpaces: 2);
  }

  public static string FormatMemberSummary(string? summary)
  {
    // Match emitted service/interface member indentation (four spaces inside namespace block).
    return FormatSummary(summary, baseIndentSpaces: 4);
  }

  public static string FormatPropertySummary(string? summary, int baseIndentSpaces = 4)
  {
    return FormatSummary(summary, baseIndentSpaces);
  }

  public static string FormatEnumMemberSummary(string? summary)
  {
    return FormatSummary(summary, baseIndentSpaces: 4);
  }

  public static string NormalizeDocumentationText(string? text)
  {
    if (string.IsNullOrWhiteSpace(text))
    {
      return string.Empty;
    }

    return DecodeHtmlEntities(text.Trim());
  }

  public static string DecodeHtmlEntities(string s)
  {
    if (string.IsNullOrEmpty(s))
    {
      return s;
    }

    // Numeric entities first, then named; decode &amp; last.
    s = Regex.Replace(s, @"&#(\d+);", match =>
    {
      if (int.TryParse(match.Groups[1].Value, out var code) && code >= 0 && code <= 0x10FFFF)
      {
        return char.ConvertFromUtf32(code);
      }

      return match.Value;
    });

    s = Regex.Replace(s, @"&#x([0-9A-Fa-f]+);", match =>
    {
      if (int.TryParse(match.Groups[1].Value, System.Globalization.NumberStyles.HexNumber, null, out var code) &&
          code >= 0 && code <= 0x10FFFF)
      {
        return char.ConvertFromUtf32(code);
      }

      return match.Value;
    });

    return s
      .Replace("&lt;", "<", StringComparison.Ordinal)
      .Replace("&gt;", ">", StringComparison.Ordinal)
      .Replace("&apos;", "'", StringComparison.Ordinal)
      .Replace("&quot;", "\"", StringComparison.Ordinal)
      .Replace("&#39;", "'", StringComparison.Ordinal)
      .Replace("&amp;", "&", StringComparison.Ordinal);
  }

  private static string FormatSummary(string? summary, int baseIndentSpaces)
  {
    var normalized = NormalizeDocumentationText(summary);
    if (string.IsNullOrWhiteSpace(normalized))
    {
      return string.Empty;
    }

    var pad = new string(' ', baseIndentSpaces);
    var sb = new StringBuilder();
    sb.Append(pad).AppendLine("/// <summary>");
    foreach (var line in normalized.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
    {
      sb.Append(pad).Append("/// ").AppendLine(XmlEscape(EnsureDocumentationEndsWithPeriod(line)));
    }

    sb.Append(pad).AppendLine("/// </summary>");
    return sb.ToString();
  }

  private static string XmlEscape(string s)
  {
    return s.Replace("&", "&amp;", StringComparison.Ordinal)
      .Replace("<", "&lt;", StringComparison.Ordinal)
      .Replace(">", "&gt;", StringComparison.Ordinal);
  }

  /// <summary>
  /// Satisfies StyleCop SA1629 for generated documentation.
  /// </summary>
  private static string EnsureDocumentationEndsWithPeriod(string line)
  {
    if (line.Length == 0)
    {
      return line;
    }

    var last = line[^1];
    if (last is '.' or '!' or '?' or ':' or ';')
    {
      return line;
    }

    return line + ".";
  }
}
