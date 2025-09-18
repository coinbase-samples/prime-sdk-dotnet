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
  using CoinbaseSdk.Prime.Model;

  public class ListPortfolioBalancesRequest(string portfolioId) : PaginatedRequest
  {
    [JsonIgnore]
    public string PortfolioId { get; set; } = portfolioId;

    public string[] Symbols { get; set; } = [];

    [JsonPropertyName("balance_type")]
    public BalanceType? BalanceType { get; set; }

    [JsonPropertyName("sort_direction")]
    public SortDirection? SortDirection { get; set; }

    public class ListPortfolioBalancesRequestBuilder : PaginatedRequestBuilder<ListPortfolioBalancesRequest, ListPortfolioBalancesRequestBuilder>
    {
      private string? _portfolioId;
      private string[] _symbols = [];
      private BalanceType _balanceType;
      private SortDirection? _sortDirection;

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

      public ListPortfolioBalancesRequestBuilder WithSortDirection(SortDirection sortDirection)
      {
        this._sortDirection = sortDirection;
        return this;
      }

      public new ListPortfolioBalancesRequestBuilder WithPagination(Pagination pagination)
      {
        base.WithPagination(pagination);
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
      public override ListPortfolioBalancesRequest Build()
      {
        Validate();
        var request = new ListPortfolioBalancesRequest(_portfolioId!)
        {
          Symbols = this._symbols,
          BalanceType = this._balanceType,
          SortDirection = this._sortDirection
        };
        SetPaginationProperties(request);
        return request;
      }
    }
  }
}
