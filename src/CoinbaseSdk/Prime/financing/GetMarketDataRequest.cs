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

namespace CoinbaseSdk.Prime.Financing
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Core.Error;
  using CoinbaseSdk.Prime.Common;
  using CoinbaseSdk.Prime.Model.Enums;

  /// <summary>
  /// Get Market Data.
  /// </summary>
  public class GetMarketDataRequest(string entityId) : PaginatedRequest
  {
    [JsonIgnore]
    public string EntityId { get; set; } = entityId;

    public class GetMarketDataRequestBuilder
    {
      private string? _entityId;
      private string? _cursor;
      private SortDirection? _sortDirection;
      private int? _limit;

      public GetMarketDataRequestBuilder WithEntityId(string entityId)
      {
        _entityId = entityId;
        return this;
      }

      public GetMarketDataRequestBuilder WithCursor(string cursor)
      {
        _cursor = cursor;
        return this;
      }

      public GetMarketDataRequestBuilder WithSortDirection(SortDirection sortDirection)
      {
        _sortDirection = sortDirection;
        return this;
      }

      public GetMarketDataRequestBuilder WithLimit(int limit)
      {
        _limit = limit;
        return this;
      }

      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(_entityId))
        {
          throw new CoinbaseClientException("EntityId is required");
        }
      }

      public GetMarketDataRequest Build()
      {
        Validate();
        return new GetMarketDataRequest(_entityId!)
        {
          Cursor = _cursor,
          SortDirection = _sortDirection,
          Limit = _limit,
        };
      }
    }
  }
}
