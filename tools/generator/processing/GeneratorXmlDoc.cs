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

namespace CoinbaseSdk.Tools.Generator.Processing;

/// <summary>
/// XML documentation comments derived from OpenAPI operation summaries.
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

  private static string FormatSummary(string? summary, int baseIndentSpaces)
  {
    if (string.IsNullOrWhiteSpace(summary))
    {
      return string.Empty;
    }

    var pad = new string(' ', baseIndentSpaces);
    var sb = new StringBuilder();
    sb.Append(pad).AppendLine("/// <summary>");
    foreach (var line in summary.Trim().Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
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
