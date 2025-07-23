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

namespace CoinbaseSdk.Prime.Model
{
    using System.Text.Json.Serialization;

    public class Commission
    {
        /// <summary>
        /// Fee model (all_in or cost_plus).
        /// </summary>
        public string? Type { get; set; }

        /// <summary>
        /// Commission rate (in whole percentage. Commission of 15bps is "0.0015").
        /// </summary>
        public string? Rate { get; set; }

        /// <summary>
        /// Average 30 days over past 3 months (e.g. 90 days divided by 3).
        /// </summary>
        [JsonPropertyName("trading_volume")]
        public string? TradingVolume { get; set; }
    }
}