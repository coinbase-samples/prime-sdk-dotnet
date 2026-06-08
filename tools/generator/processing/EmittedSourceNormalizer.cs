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

/// <summary>
/// Normalizes emitted C# so generator output matches <c>dotnet format</c> (reduces --diff noise).
/// </summary>
public static class EmittedSourceNormalizer
{
  public static string Normalize(string content)
  {
    if (string.IsNullOrEmpty(content))
    {
      return content;
    }

    var lines = content.Replace("\r\n", "\n", StringComparison.Ordinal).Split('\n');
    var builder = new System.Text.StringBuilder();
    for (var i = 0; i < lines.Length; i++)
    {
      builder.Append(lines[i].TrimEnd());
      if (i < lines.Length - 1)
      {
        builder.Append('\n');
      }
    }

    var normalized = builder.ToString();
    if (!normalized.EndsWith('\n'))
    {
      normalized += '\n';
    }

    return normalized;
  }
}
