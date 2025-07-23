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
    /// Base class for all paginated request builders.
    /// Provides common pagination builder methods that are shared across all paginated endpoints.
    /// </summary>
    /// <typeparam name="TRequest">The type of request this builder creates.</typeparam>
    /// <typeparam name="TBuilder">The type of the concrete builder (for fluent interface).</typeparam>
    public abstract class PaginatedRequestBuilder<TRequest, TBuilder>
        where TRequest : PaginatedRequest
        where TBuilder : PaginatedRequestBuilder<TRequest, TBuilder>
    {
        private string? _cursor;
        private int? _limit;

        /// <summary>
        /// Sets the cursor for pagination.
        /// </summary>
        /// <param name="cursor">The cursor value to use for pagination.</param>
        /// <returns>This builder instance for method chaining.</returns>
        public TBuilder WithCursor(string cursor)
        {
            this._cursor = cursor;
            return (TBuilder)this;
        }

        /// <summary>
        /// Sets the limit for pagination.
        /// </summary>
        /// <param name="limit">The maximum number of items to return.</param>
        /// <returns>This builder instance for method chaining.</returns>
        public TBuilder WithLimit(int limit)
        {
            this._limit = limit;
            return (TBuilder)this;
        }

        /// <summary>
        /// Sets pagination properties from a Pagination object.
        /// This extracts the NextCursor from the pagination object and sets it as the cursor.
        /// </summary>
        /// <param name="pagination">The pagination object containing cursor and sort direction.</param>
        /// <returns>This builder instance for method chaining.</returns>
        public TBuilder WithPagination(Pagination pagination)
        {
            this._cursor = pagination.NextCursor;
            return (TBuilder)this;
        }

        /// <summary>
        /// Sets the pagination properties on the request object.
        /// This should be called by concrete builders in their Build() method.
        /// </summary>
        /// <param name="request">The request object to set pagination properties on.</param>
        protected void SetPaginationProperties(TRequest request)
        {
            request.Cursor = this._cursor;
            request.Limit = this._limit;
        }

        /// <summary>
        /// Builds the request object.
        /// Concrete builders must implement this method to create and return their specific request type.
        /// </summary>
        /// <returns>The built request object.</returns>
        public abstract TRequest Build();
    }
}