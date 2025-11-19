/*
 * Copyright 2025-present Coinbase Global, Inc.
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
    using System;
    using System.Text.Json;
    using CoinbaseSdk.Core.Serialization;

    /// <summary>
    /// Provides a single location to construct JSON serializer options for the Prime SDK.
    /// The factory also wires the shared defaults exposed by <see cref="JsonUtility"/>.
    /// </summary>
    internal static class PrimeJsonSerializerOptionsFactory
    {
        private static readonly Lazy<JsonSerializerOptions> CachedOptions = new (CreateOptions);

        /// <summary>
        /// Gets the cached serializer options used throughout the SDK.
        /// </summary>
        internal static JsonSerializerOptions Default => CachedOptions.Value;

        /// <summary>
        /// Clone the cached options. Useful when a caller must mutate settings locally.
        /// </summary>
        internal static JsonSerializerOptions Clone() => new (CachedOptions.Value);

        private static JsonSerializerOptions CreateOptions()
        {
            return new JsonSerializerOptions(JsonUtility.DefaultOptions);
        }
    }
}

