/*
 * Copyright 2024-present Coinbase Global, Inc.
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

namespace CoinbaseSdk.Prime.Balances
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Core.Error;
  using CoinbaseSdk.Prime.Common;
  using CoinbaseSdk.Prime.Model;

  public class ListPortfolioBalancesRequest(string portfolioId)
  : BaseListRequest(portfolioId, null)
  {
    public string[] Symbols { get; set; } = [];
    [JsonPropertyName("balance_type")]
    public BalanceType? BalanceType;

    public class ListPortfolioBalancesRequestBuilder
    {
      private string? _portfolioId;
      private string[] _symbols = [];
      private BalanceType _balanceType;
      private string? _cursor;
      private string? _sortDirection;
      private int? _limit;

      public ListPortfolioBalancesRequestBuilder WithPortfolioId(string portfolioId)
      {
        this._portfolioId = portfolioId;
        return this;
      }

      public ListPortfolioBalancesRequestBuilder WithSymbols(string[] symbols)
      {
        this._symbols = symbols;
        return this;
      }

      public ListPortfolioBalancesRequestBuilder WithBalanceType(BalanceType balanceType)
      {
        this._balanceType = balanceType;
        return this;
      }

      public ListPortfolioBalancesRequestBuilder WithCursor(string cursor)
      {
        this._cursor = cursor;
        return this;
      }

      public ListPortfolioBalancesRequestBuilder WithSortDirection(string sortDirection)
      {
        this._sortDirection = sortDirection;
        return this;
      }

      public ListPortfolioBalancesRequestBuilder WithLimit(int limit)
      {
        this._limit = limit;
        return this;
      }

      public ListPortfolioBalancesRequestBuilder WithPagination(Pagination pagination)
      {
        this._cursor = pagination.NextCursor;
        this._sortDirection = pagination.SortDirection;
        return this;
      }

      /// <summary>
      /// Validates the request.
      /// </summary>
      /// <exception cref="CoinbaseClientException">Thrown when <see cref="_portfolioId" /> is null, empty, or whitespace.</exception>
      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(_portfolioId))
        {
          throw new CoinbaseClientException("PortfolioId is required");
        }
      }

      /// <summary>
      /// Builds the <see cref="ListPortfolioBalancesRequest"/>.
      /// </summary>
      /// <returns>The <see cref="ListPortfolioBalancesRequest"/>.</returns>
      /// <exception cref="CoinbaseClientException">Thrown when <see cref="_portfolioId" /> is null, empty, or whitespace.</exception>
      public ListPortfolioBalancesRequest Build()
      {
        Validate();
        return new ListPortfolioBalancesRequest(_portfolioId!)
        {
          Symbols = this._symbols,
          BalanceType = this._balanceType,
          Cursor = this._cursor,
          SortDirection = this._sortDirection,
          Limit = this._limit
        };
      }
    }
  }
}
