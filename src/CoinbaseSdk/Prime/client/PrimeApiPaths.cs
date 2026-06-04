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

namespace CoinbaseSdk.Prime.Client
{
  using System.Text.RegularExpressions;

  /// <summary>
  /// Helpers for Prime API base URL paths.
  /// </summary>
  public static class PrimeApiPaths
  {
    private static readonly Regex VersionSuffix = new(@"/v\d+/?$", RegexOptions.Compiled);

    /// <summary>
    /// Replaces a trailing <c>/vN</c> segment with <c>/{version}</c>.
    /// Used when an endpoint is served from a different API version than the default client (e.g. v2).
    /// </summary>
    /// <param name="apiBasePath">Base path such as <c>api.prime.coinbase.com/v1</c>.</param>
    /// <param name="version">Target version segment without a leading slash (e.g. <c>v2</c>).</param>
    /// <returns>Base path with the version segment replaced or appended.</returns>
    public static string VersionedApiBasePath(string apiBasePath, string version)
    {
      var trimmed = apiBasePath.TrimEnd('/');
      if (VersionSuffix.IsMatch(trimmed))
      {
        return VersionSuffix.Replace(trimmed, "/" + version);
      }

      return trimmed + "/" + version;
    }
  }
}
