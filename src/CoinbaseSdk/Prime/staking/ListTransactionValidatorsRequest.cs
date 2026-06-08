/*
 * Copyright 2025-present Coinbase Global, Inc.
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

namespace CoinbaseSdk.Prime.Staking
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Core.Error;
  using CoinbaseSdk.Prime.Model.Enums;

  /// <summary>
  /// List Transaction Validators.
  /// </summary>
  public class ListTransactionValidatorsRequest(string portfolioId)
  {
    [JsonIgnore]
    public string PortfolioId { get; set; } = portfolioId;

    public string[] TransactionIds { get; set; } = [];

    public string? Cursor { get; set; }

    public int? Limit { get; set; }

    public SortDirection? SortDirection { get; set; }

    public class ListTransactionValidatorsRequestBuilder
    {
      private string? _portfolioId;
      private string[] _transactionIds;
      private string? _cursor;
      private int? _limit;
      private SortDirection? _sortDirection;

      public ListTransactionValidatorsRequestBuilder WithPortfolioId(string portfolioId)
      {
        _portfolioId = portfolioId;
        return this;
      }

      public ListTransactionValidatorsRequestBuilder WithTransactionIds(string[] transactionIds)
      {
        _transactionIds = transactionIds;
        return this;
      }

      public ListTransactionValidatorsRequestBuilder WithCursor(string? cursor)
      {
        _cursor = cursor;
        return this;
      }

      public ListTransactionValidatorsRequestBuilder WithLimit(int? limit)
      {
        _limit = limit;
        return this;
      }

      public ListTransactionValidatorsRequestBuilder WithSortDirection(SortDirection? sortDirection)
      {
        _sortDirection = sortDirection;
        return this;
      }

      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(_portfolioId))
        {
          throw new CoinbaseClientException("PortfolioId is required");
        }
      }

      public ListTransactionValidatorsRequest Build()
      {
        Validate();
        return new ListTransactionValidatorsRequest(_portfolioId!)
        {
          TransactionIds = _transactionIds ?? [],
          Cursor = _cursor,
          Limit = _limit,
          SortDirection = _sortDirection,
        };
      }
    }
  }
}
