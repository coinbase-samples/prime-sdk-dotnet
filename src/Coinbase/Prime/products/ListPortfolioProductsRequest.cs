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

namespace Coinbase.Prime.Products
{
  using Coinbase.Core.Error;
  using Coinbase.Prime.Common;
  public class ListPortfolioProductsRequest(string portfolioId)
  : BaseListRequest(portfolioId, null)
  {
    public class ListPortfolioProductsRequestBuilder
    {
      private string? _portfolioId;
      private string? _cursor;
      private string? _sortDirection;
      private int? _limit;

      public ListPortfolioProductsRequestBuilder WithPortfolioId(string portfolioId)
      {
        this._portfolioId = portfolioId;
        return this;
      }

      public ListPortfolioProductsRequestBuilder WithCursor(string cursor)
      {
        this._cursor = cursor;
        return this;
      }

      public ListPortfolioProductsRequestBuilder WithSortDirection(string sortDirection)
      {
        this._sortDirection = sortDirection;
        return this;
      }

      public ListPortfolioProductsRequestBuilder WithLimit(int limit)
      {
        this._limit = limit;
        return this;
      }

      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(this._portfolioId))
        {
          throw new CoinbaseClientException("PortfolioId cannot be null or empty");
        }
      }

      public ListPortfolioProductsRequest Build()
      {
        this.Validate();
        return new ListPortfolioProductsRequest(this._portfolioId!)
        {
          Cursor = this._cursor,
          SortDirection = this._sortDirection,
          Limit = this._limit
        };
      }
    }
  }
}
