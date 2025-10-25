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

namespace CoinbaseSdk.Prime.Balances
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Prime.Common;
  using CoinbaseSdk.Prime.Model.Enums;

  public class ListEntityBalancesRequest(string entityId) : PaginatedRequest
  {
    [JsonIgnore]
    public string EntityId { get; set; } = entityId;

    public string[] Symbols { get; set; } = [];


    [JsonPropertyName("aggregation_type")]
    public PortfolioBalanceType? AggregationType { get; set; }

    public class Builder(string entityId)
    {
      private readonly string _entityId = entityId;
      private string[] _symbols = [];
      private PortfolioBalanceType? _aggregationType;
      private string? _cursor;
      private SortDirection? _sortDirection;
      private int? _limit;

      public Builder WithSymbols(string[] symbols)
      {
        _symbols = symbols;
        return this;
      }

      public Builder WithAggregationType(PortfolioBalanceType? aggregationType)
      {
        _aggregationType = aggregationType;
        return this;
      }

      public Builder WithCursor(string cursor)
      {
        _cursor = cursor;
        return this;
      }

      public Builder WithSortDirection(SortDirection sortDirection)
      {
        _sortDirection = sortDirection;
        return this;
      }

      public Builder WithLimit(int limit)
      {
        _limit = limit;
        return this;
      }

      public ListEntityBalancesRequest Build()
      {
        var request = new ListEntityBalancesRequest(_entityId)
        {
          Symbols = _symbols,
          AggregationType = _aggregationType,
          Cursor = _cursor,
          SortDirection = _sortDirection,
          Limit = _limit
        };
        return request;
      }
    }
  }
}
