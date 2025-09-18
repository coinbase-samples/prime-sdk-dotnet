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
    /// <summary>
    /// Base class for all paginated request objects.
    /// Provides common pagination properties that are shared across all paginated endpoints.
    /// </summary>
    public abstract class PaginatedRequest
    {
        /// <summary>
        /// Gets or sets the cursor for pagination.
        /// Used to specify where to start the next page of results.
        /// </summary>
        public string? Cursor { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of items to return.
        /// If not specified, the server's default limit will be used.
        /// </summary>
        public int? Limit { get; set; }
    }
}