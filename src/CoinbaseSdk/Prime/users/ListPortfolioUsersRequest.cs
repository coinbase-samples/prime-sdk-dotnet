/*
 * Copyright 2024-present Coinbase Global, Inc.
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

namespace CoinbaseSdk.Prime.Users
{
  using CoinbaseSdk.Core.Error;
  using CoinbaseSdk.Prime.Common;

  public class ListPortfolioUsersRequest(string portfolioId)
  : BaseListRequest(portfolioId, null)
  {
    public class ListPortfolioUsersRequestBuilder
    {
      private string? _portfolioId;
      private string? _cursor;
      private string? _sortDirection;
      private int? _limit;

      public ListPortfolioUsersRequestBuilder WithPortfolioId(string portfolioId)
      {
        this._portfolioId = portfolioId;
        return this;
      }

      public ListPortfolioUsersRequestBuilder WithCursor(string? cursor)
      {
        this._cursor = cursor;
        return this;
      }

      public ListPortfolioUsersRequestBuilder WithSortDirection(string? sortDirection)
      {
        this._sortDirection = sortDirection;
        return this;
      }

      public ListPortfolioUsersRequestBuilder WithLimit(int? limit)
      {
        this._limit = limit;
        return this;
      }

      public ListPortfolioUsersRequestBuilder WithPagination(Pagination pagination)
      {
        this._cursor = pagination.NextCursor;
        this._sortDirection = pagination.SortDirection;
        return this;
      }

      /// <summary>
      /// Validates the builder.
      /// </summary>
      /// <exception cref="CoinbaseClientException">Thrown when the <see cref="_portfolioId"/> is null, empty or whitespace.</exception>
      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(this._portfolioId))
        {
          throw new CoinbaseClientException("PortfolioId is required");
        }
      }

      /// <summary>
      /// Builds the <see cref="ListPortfolioUsersRequest"/> object.
      /// </summary>
      /// <returns>The <see cref="ListPortfolioUsersRequest"/> object.</returns>
      /// <exception cref="CoinbaseClientException">Thrown when the required fields are not set.</exception>
      public ListPortfolioUsersRequest Build()
      {
        this.Validate();
        return new ListPortfolioUsersRequest(this._portfolioId!)
        {
          Cursor = this._cursor,
          SortDirection = this._sortDirection,
          Limit = this._limit
        };
      }
    }
  }
}
