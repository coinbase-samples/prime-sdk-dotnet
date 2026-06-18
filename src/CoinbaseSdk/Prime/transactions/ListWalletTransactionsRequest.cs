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

namespace CoinbaseSdk.Prime.Transactions
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Core.Error;
  using CoinbaseSdk.Prime.Common;
  using CoinbaseSdk.Prime.Model.Enums;

  /// <summary>
  /// List Wallet Transactions.
  /// </summary>
  public class ListWalletTransactionsRequest(string portfolioId, string walletId) : PaginatedRequest
  {
    [JsonIgnore]
    public string PortfolioId { get; set; } = portfolioId;

    [JsonIgnore]
    public string WalletId { get; set; } = walletId;

    public TransactionType[] Types { get; set; } = [];

    public string? StartTime { get; set; }

    public string? EndTime { get; set; }

    public class ListWalletTransactionsRequestBuilder
    {
      private string? _portfolioId;
      private string? _walletId;
      private TransactionType[]? _types;
      private string? _startTime;
      private string? _endTime;
      private string? _cursor;
      private SortDirection? _sortDirection;
      private int? _limit;

      public ListWalletTransactionsRequestBuilder WithPortfolioId(string portfolioId)
      {
        _portfolioId = portfolioId;
        return this;
      }

      public ListWalletTransactionsRequestBuilder WithWalletId(string walletId)
      {
        _walletId = walletId;
        return this;
      }

      public ListWalletTransactionsRequestBuilder WithTypes(TransactionType[] types)
      {
        _types = types;
        return this;
      }

      public ListWalletTransactionsRequestBuilder WithStartTime(string? startTime)
      {
        _startTime = startTime;
        return this;
      }

      public ListWalletTransactionsRequestBuilder WithEndTime(string? endTime)
      {
        _endTime = endTime;
        return this;
      }

      public ListWalletTransactionsRequestBuilder WithCursor(string cursor)
      {
        _cursor = cursor;
        return this;
      }

      public ListWalletTransactionsRequestBuilder WithSortDirection(SortDirection sortDirection)
      {
        _sortDirection = sortDirection;
        return this;
      }

      public ListWalletTransactionsRequestBuilder WithLimit(int limit)
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

      public ListWalletTransactionsRequest Build()
      {
        Validate();
        return new ListWalletTransactionsRequest(_portfolioId!, _walletId!)
        {
          Types = _types ?? [],
          StartTime = _startTime,
          EndTime = _endTime,
          Cursor = _cursor,
          SortDirection = _sortDirection,
          Limit = _limit,
        };
      }
    }
  }
}
