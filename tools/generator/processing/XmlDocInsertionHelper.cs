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

namespace CoinbaseSdk.Tools.Generator.Processing;

internal static class XmlDocInsertionHelper
{
  internal static void InsertTypeDocBeforeMember(IList<string> output, string docBlock)
  {
    if (string.IsNullOrEmpty(docBlock))
    {
      return;
    }

    var insertIndex = output.Count;
    while (insertIndex > 0 && string.IsNullOrWhiteSpace(output[insertIndex - 1]))
    {
      insertIndex--;
    }

    var attrStart = insertIndex;
    while (attrStart > 0 && output[attrStart - 1].TrimStart().StartsWith("[", StringComparison.Ordinal))
    {
      attrStart--;
    }

    foreach (var line in docBlock.Split('\n', StringSplitOptions.RemoveEmptyEntries))
    {
      output.Insert(attrStart++, line.TrimEnd('\r'));
    }
  }

  internal static bool HasTypeDocBeforeMember(IReadOnlyList<string> output)
  {
    var index = output.Count;
    while (index > 0 && string.IsNullOrWhiteSpace(output[index - 1]))
    {
      index--;
    }

    while (index > 0 && output[index - 1].TrimStart().StartsWith("[", StringComparison.Ordinal))
    {
      index--;
    }

    return index > 0 && output[index - 1].TrimStart().StartsWith("///", StringComparison.Ordinal);
  }

  internal static bool HasSummaryDocAbove(IReadOnlyList<string> output)
  {
    for (var i = output.Count - 1; i >= 0; i--)
    {
      var trimmed = output[i].Trim();
      if (trimmed.Length == 0)
      {
        continue;
      }

      return trimmed.StartsWith("///", StringComparison.Ordinal);
    }

    return false;
  }

  internal static IEnumerable<string> SplitLines(string block)
  {
    if (string.IsNullOrEmpty(block))
    {
      yield break;
    }

    foreach (var line in block.Split('\n', StringSplitOptions.RemoveEmptyEntries))
    {
      yield return line.TrimEnd('\r');
    }
  }
}
