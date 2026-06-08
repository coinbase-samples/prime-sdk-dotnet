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

namespace CoinbaseSdk.Prime.Wallets
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Core.Error;
  using CoinbaseSdk.Prime.Common;
  using CoinbaseSdk.Prime.Model.Enums;

  /// <summary>
  /// List Portfolio Wallets.
  /// </summary>
  public class ListWalletsRequest(string portfolioId) : PaginatedRequest
  {
    [JsonIgnore]
    public string PortfolioId { get; set; } = portfolioId;

    public string? Type { get; set; }

    public string[] Symbols { get; set; } = [];

    public bool? GetNetworkUnifiedWallets { get; set; }

    public class ListWalletsRequestBuilder
    {
      private string? _portfolioId;
      private string? _type;
      private string[]? _symbols;
      private bool? _getNetworkUnifiedWallets;
      private string? _cursor;
      private SortDirection? _sortDirection;
      private int? _limit;

      public ListWalletsRequestBuilder WithPortfolioId(string portfolioId)
      {
        _portfolioId = portfolioId;
        return this;
      }

      public ListWalletsRequestBuilder WithType(string? type)
      {
        _type = type;
        return this;
      }

      public ListWalletsRequestBuilder WithSymbols(string[] symbols)
      {
        _symbols = symbols;
        return this;
      }

      public ListWalletsRequestBuilder WithGetNetworkUnifiedWallets(bool? getNetworkUnifiedWallets)
      {
        _getNetworkUnifiedWallets = getNetworkUnifiedWallets;
        return this;
      }

      public ListWalletsRequestBuilder WithCursor(string cursor)
      {
        _cursor = cursor;
        return this;
      }

      public ListWalletsRequestBuilder WithSortDirection(SortDirection sortDirection)
      {
        _sortDirection = sortDirection;
        return this;
      }

      public ListWalletsRequestBuilder WithLimit(int limit)
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

      public ListWalletsRequest Build()
      {
        Validate();
        return new ListWalletsRequest(_portfolioId!)
        {
          Type = _type,
          Symbols = _symbols ?? [],
          GetNetworkUnifiedWallets = _getNetworkUnifiedWallets,
          Cursor = _cursor,
          SortDirection = _sortDirection,
          Limit = _limit,
        };
      }
    }
  }
}
