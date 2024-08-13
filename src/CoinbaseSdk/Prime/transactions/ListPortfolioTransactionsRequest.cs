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

namespace CoinbaseSdk.Prime.Transactions
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Core.Error;
  using CoinbaseSdk.Prime.Common;
  public class ListPortfolioTransactionsRequest(string portfolioId)
  : BaseListRequest(portfolioId, null)
  {
    [JsonPropertyName("symbols")]
    public string[] Symbols { get; set; } = [];

    [JsonPropertyName("types")]
    public TransactionType[] Types { get; set; } = [];

    [JsonPropertyName("start_time")]
    public string? StartTime { get; set; }

    [JsonPropertyName("end_time")]
    public string? EndTime { get; set; }

    public class ListPortfolioTransactionsRequestBuilder
    {
      private string? _portfolioId;
      private string[] _symbols = [];
      private TransactionType[] _types = [];
      private string? _startTime;
      private string? _endTime;
      private string? _cursor;
      private string? _sortDirection;
      private int? _limit;

      public ListPortfolioTransactionsRequestBuilder WithPortfolioId(string portfolioId)
      {
        _portfolioId = portfolioId;
        return this;
      }

      public ListPortfolioTransactionsRequestBuilder WithSymbols(string[] symbols)
      {
        _symbols = symbols;
        return this;
      }

      public ListPortfolioTransactionsRequestBuilder WithTypes(TransactionType[] types)
      {
        _types = types;
        return this;
      }

      public ListPortfolioTransactionsRequestBuilder WithStartTime(string startTime)
      {
        _startTime = startTime;
        return this;
      }

      public ListPortfolioTransactionsRequestBuilder WithEndTime(string endTime)
      {
        _endTime = endTime;
        return this;
      }

      public ListPortfolioTransactionsRequestBuilder WithCursor(string cursor)
      {
        _cursor = cursor;
        return this;
      }

      public ListPortfolioTransactionsRequestBuilder WithSortDirection(string sortDirection)
      {
        _sortDirection = sortDirection;
        return this;
      }

      public ListPortfolioTransactionsRequestBuilder WithLimit(int limit)
      {
        _limit = limit;
        return this;
      }

      public ListPortfolioTransactionsRequestBuilder WithPagination(Pagination pagination)
      {
        _cursor = pagination.NextCursor;
        _sortDirection = pagination.SortDirection;
        return this;
      }

      /// <summary>
      /// Validates the request.
      /// </summary>
      /// <exception cref="CoinbaseClientException">Thrown when <see cref="_portfolioId" /> is null, empty, or whitespace.</exception>
      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(this._portfolioId))
        {
          throw new CoinbaseClientException("PortfolioId cannot be null or empty");
        }
      }

      /// <summary>
      /// Builds the <see cref="ListPortfolioTransactionsRequest"/> object.
      /// </summary>
      /// <returns>The <see cref="ListPortfolioTransactionsRequest"/> object.</returns>
      /// <exception cref="CoinbaseClientException">Thrown when the required fields are not set.</exception>
      public ListPortfolioTransactionsRequest Build()
      {
        return new ListPortfolioTransactionsRequest(this._portfolioId!)
        {
          Symbols = _symbols,
          Types = _types,
          StartTime = _startTime,
          EndTime = _endTime,
          Cursor = _cursor,
          SortDirection = _sortDirection,
          Limit = _limit
        };
      }
    }
  }
}
