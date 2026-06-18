/*
 * Copyright 2026-present Coinbase Global, Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace CoinbaseSdk.Prime.Serialization
{
  using System.Text.Json;
  using CoinbaseSdk.Core.Serialization;

  /// <summary>
  /// Prime API wire-format defaults: Core serializer settings with snake_case property naming.
  /// Use <see cref="JsonUtility"/> for HTTP calls, or pass a custom <see cref="IJsonUtility"/> to
  /// <see cref="Client.CoinbasePrimeClient"/> when advanced overrides are required.
  /// </summary>
  public static class PrimeJsonDefaults
  {
    private static readonly Lazy<IJsonUtility> CachedJsonUtility =
        new (() => new Core.Serialization.JsonUtility(CreateOptions()));

    /// <summary>
    /// Shared Prime HTTP serializer (snake_case naming policy, Core converters).
    /// </summary>
    public static IJsonUtility JsonUtility => CachedJsonUtility.Value;

    /// <summary>
    /// Builds Prime <see cref="JsonSerializerOptions"/> (clone of Core defaults + snake_case policy).
    /// </summary>
    public static JsonSerializerOptions CreateOptions()
    {
      var options = Core.Serialization.JsonUtility.CloneDefaultOptions();
      options.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
      return options;
    }

    /// <summary>
    /// Creates a new <see cref="IJsonUtility"/> using <see cref="CreateOptions"/>.
    /// </summary>
    public static IJsonUtility CreateJsonUtility()
    {
      return new Core.Serialization.JsonUtility(CreateOptions());
    }
  }
}
