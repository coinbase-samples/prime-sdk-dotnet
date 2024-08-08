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

namespace Coinbase.Prime.Transactions
{
  using System.Text.Json.Serialization;
  using Coinbase.Core.Error;
  using Coinbase.Prime.Common;
  public class ListWalletTransactionsRequest(string portfolioId, string walletId)
  : BaseListRequest(portfolioId, null)
  {
    [JsonIgnore]
    public string WalletId { get; set; } = walletId;

    [JsonPropertyName("type")]
    public TransactionType Type { get; set; }

    [JsonPropertyName("start_time")]
    public string? StartTime { get; set; }

    [JsonPropertyName("end_time")]
    public string? EndTime { get; set; }

    public class ListWalletTransactionsRequestBuilder
    {
      private string? _portfolioId;
      private string? _walletId;
      private TransactionType _type;
      private string? _startTime;
      private string? _endTime;
      private string? _cursor;
      private string? _sortDirection;
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

      public ListWalletTransactionsRequestBuilder WithType(TransactionType type)
      {
        _type = type;
        return this;
      }

      public ListWalletTransactionsRequestBuilder WithStartTime(string startTime)
      {
        _startTime = startTime;
        return this;
      }

      public ListWalletTransactionsRequestBuilder WithEndTime(string endTime)
      {
        _endTime = endTime;
        return this;
      }

      public ListWalletTransactionsRequestBuilder WithCursor(string cursor)
      {
        _cursor = cursor;
        return this;
      }

      public ListWalletTransactionsRequestBuilder WithSortDirection(string sortDirection)
      {
        _sortDirection = sortDirection;
        return this;
      }

      public ListWalletTransactionsRequestBuilder WithLimit(int limit)
      {
        _limit = limit;
        return this;
      }

      public ListWalletTransactionsRequestBuilder WithPagination(Pagination pagination)
      {
        _cursor = pagination.NextCursor;
        _sortDirection = pagination.SortDirection;
        return this;
      }

      /// <summary>
      /// Validates the builder.
      /// </summary>
      /// <exception cref="CoinbaseClientException">Thrown when <see cref="_portfolioId" /> is null, empty, or whitespace.</exception>
      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(this._portfolioId))
        {
          throw new CoinbaseClientException("PortfolioId is required");
        }
        if (string.IsNullOrWhiteSpace(this._walletId))
        {
          throw new CoinbaseClientException("WalletId is required");
        }
      }

      /// <summary>
      /// Builds the <see cref="ListWalletTransactionsRequest"/>.
      /// </summary>
      /// <returns>The <see cref="ListWalletTransactionsRequest"/>.</returns>
      /// <exception cref="CoinbaseClientException">Thrown when the required fields are not set.</exception>
      public ListWalletTransactionsRequest Build()
      {
        this.Validate();
        return new ListWalletTransactionsRequest(this._portfolioId!, this._walletId!)
        {
          Type = this._type,
          StartTime = this._startTime,
          EndTime = this._endTime,
          Cursor = this._cursor,
          SortDirection = this._sortDirection,
          Limit = this._limit
        };
      }
    }
  }
}
