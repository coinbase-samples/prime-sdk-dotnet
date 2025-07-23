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

    public class PortfolioUser
    {
        /// <summary>
        /// The unique ID of the user.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// The name of the user.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// The email of the user.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// The portfolio to which this user and associated permissions are identified.
        /// </summary>
        [JsonPropertyName("portfolio_id")]
        public string? PortfolioId { get; set; }

        /// <summary>
        /// The entity to which this user and associated permissions are identified.
        /// </summary>
        [JsonPropertyName("entity_id")]
        public string? EntityId { get; set; }

        /// <summary>
        /// The user's role.
        /// </summary>
        public UserRole? Role { get; set; }
    }
}