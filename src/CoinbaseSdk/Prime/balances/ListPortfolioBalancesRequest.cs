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
  using CoinbaseSdk.Prime.Model.Enums;

  /// <summary>
  /// List Portfolio Balances.
  /// </summary>
  public class ListPortfolioBalancesRequest(string portfolioId) : PaginatedRequest
  {
    [JsonIgnore]
    public string PortfolioId { get; set; } = portfolioId;

    public string[] Symbols { get; set; } = [];

    public PortfolioBalanceType? BalanceType { get; set; }

    public class ListPortfolioBalancesRequestBuilder
    {
      private string? _portfolioId;
      private string[]? _symbols;
      private PortfolioBalanceType? _balanceType;
      private string? _cursor;
      private SortDirection? _sortDirection;
      private int? _limit;

      public ListPortfolioBalancesRequestBuilder WithPortfolioId(string portfolioId)
      {
        _portfolioId = portfolioId;
        return this;
      }

      public ListPortfolioBalancesRequestBuilder WithSymbols(string[] symbols)
      {
        _symbols = symbols;
        return this;
      }

      public ListPortfolioBalancesRequestBuilder WithBalanceType(PortfolioBalanceType? balanceType)
      {
        _balanceType = balanceType;
        return this;
      }

      public ListPortfolioBalancesRequestBuilder WithCursor(string cursor)
      {
        _cursor = cursor;
        return this;
      }

      public ListPortfolioBalancesRequestBuilder WithSortDirection(SortDirection sortDirection)
      {
        _sortDirection = sortDirection;
        return this;
      }

      public ListPortfolioBalancesRequestBuilder WithLimit(int limit)
      {
        _limit = limit;
        return this;
      }

      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(_portfolioId))
        {
          throw new CoinbaseClientException("PortfolioId is required");
        }
      }

      public ListPortfolioBalancesRequest Build()
      {
        Validate();
        return new ListPortfolioBalancesRequest(_portfolioId!)
        {
          Symbols = _symbols ?? [],
          BalanceType = _balanceType,
          Cursor = _cursor,
          SortDirection = _sortDirection,
          Limit = _limit,
        };
      }
    }
  }
}
