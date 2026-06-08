/*
 * Copyright 2026-present Coinbase Global, Inc.
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
  /// List Onchain Wallet Balances.
  /// </summary>
  public class ListOnchainWalletBalancesRequest(string portfolioId, string walletId) : PaginatedRequest
  {
    [JsonIgnore]
    public string PortfolioId { get; set; } = portfolioId;

    [JsonIgnore]
    public string WalletId { get; set; } = walletId;

    public VisibilityStatus[] VisibilityStatuses { get; set; } = [];

    public class ListOnchainWalletBalancesRequestBuilder
    {
      private string? _portfolioId;
      private string? _walletId;
      private VisibilityStatus[]? _visibilityStatuses;
      private string? _cursor;
      private SortDirection? _sortDirection;
      private int? _limit;

      public ListOnchainWalletBalancesRequestBuilder WithPortfolioId(string portfolioId)
      {
        _portfolioId = portfolioId;
        return this;
      }

      public ListOnchainWalletBalancesRequestBuilder WithWalletId(string walletId)
      {
        _walletId = walletId;
        return this;
      }

      public ListOnchainWalletBalancesRequestBuilder WithVisibilityStatuses(VisibilityStatus[] visibilityStatuses)
      {
        _visibilityStatuses = visibilityStatuses;
        return this;
      }

      public ListOnchainWalletBalancesRequestBuilder WithCursor(string cursor)
      {
        _cursor = cursor;
        return this;
      }

      public ListOnchainWalletBalancesRequestBuilder WithSortDirection(SortDirection sortDirection)
      {
        _sortDirection = sortDirection;
        return this;
      }

      public ListOnchainWalletBalancesRequestBuilder WithLimit(int limit)
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
        if (string.IsNullOrWhiteSpace(_walletId))
        {
          throw new CoinbaseClientException("WalletId is required");
        }
      }

      public ListOnchainWalletBalancesRequest Build()
      {
        Validate();
        return new ListOnchainWalletBalancesRequest(_portfolioId!, _walletId!)
        {
          VisibilityStatuses = _visibilityStatuses ?? [],
          Cursor = _cursor,
          SortDirection = _sortDirection,
          Limit = _limit,
        };
      }
    }
  }
}
