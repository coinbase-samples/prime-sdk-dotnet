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

    public class OrderEdit
    {
        /// <summary>
        /// New price for the edited order.
        /// </summary>
        public string? Price { get; set; }

        /// <summary>
        /// New base quantity for the edited order, populated if order is in base size.
        /// </summary>
        [JsonPropertyName("base_quantity")]
        public string? BaseQuantity { get; set; }

        /// <summary>
        /// New quote value for the edited order, populated if order is in quote size.
        /// </summary>
        [JsonPropertyName("quote_value")]
        public string? QuoteValue { get; set; }

        /// <summary>
        /// Display base size for the edited order, populated if order is in base size.
        /// </summary>
        [JsonPropertyName("display_base_size")]
        public string? DisplayBaseSize { get; set; }

        /// <summary>
        /// Display quote size for the edited order, populated if order is in quote size.
        /// </summary>
        [JsonPropertyName("display_quote_size")]
        public string? DisplayQuoteSize { get; set; }

        /// <summary>
        /// New stop price for the edited order.
        /// </summary>
        [JsonPropertyName("stop_price")]
        public string? StopPrice { get; set; }

        /// <summary>
        /// New expiry/end time for the edited order.
        /// </summary>
        [JsonPropertyName("expiry_time")]
        public string? ExpiryTime { get; set; }

        /// <summary>
        /// Time when the edit was accepted.
        /// </summary>
        [JsonPropertyName("accept_time")]
        public string? AcceptTime { get; set; }

        /// <summary>
        /// The new client order identifier that the order adopted after the replacement was successfully accepted.
        /// </summary>
        [JsonPropertyName("client_order_id")]
        public string? ClientOrderId { get; set; }
    }
}