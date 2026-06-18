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

namespace CoinbaseSdk.Prime.Activities
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Core.Error;
  using CoinbaseSdk.Prime.Common;
  using CoinbaseSdk.Prime.Model.Enums;

  /// <summary>
  /// List Activities.
  /// </summary>
  public class ListActivitiesRequest(string portfolioId) : PaginatedRequest
  {
    [JsonIgnore]
    public string PortfolioId { get; set; } = portfolioId;

    public string[] Symbols { get; set; } = [];

    public ActivityCategory?[] Categories { get; set; } = [];

    public ActivityStatus?[] Statuses { get; set; } = [];

    public string? StartTime { get; set; }

    public string? EndTime { get; set; }

    public bool? GetNetworkUnifiedActivities { get; set; }

    public class ListActivitiesRequestBuilder
    {
      private string? _portfolioId;
      private string[]? _symbols;
      private ActivityCategory?[]? _categories;
      private ActivityStatus?[]? _statuses;
      private string? _startTime;
      private string? _endTime;
      private bool? _getNetworkUnifiedActivities;
      private string? _cursor;
      private SortDirection? _sortDirection;
      private int? _limit;

      public ListActivitiesRequestBuilder WithPortfolioId(string portfolioId)
      {
        _portfolioId = portfolioId;
        return this;
      }

      public ListActivitiesRequestBuilder WithSymbols(string[] symbols)
      {
        _symbols = symbols;
        return this;
      }

      public ListActivitiesRequestBuilder WithCategories(ActivityCategory?[] categories)
      {
        _categories = categories;
        return this;
      }

      public ListActivitiesRequestBuilder WithStatuses(ActivityStatus?[] statuses)
      {
        _statuses = statuses;
        return this;
      }

      public ListActivitiesRequestBuilder WithStartTime(string? startTime)
      {
        _startTime = startTime;
        return this;
      }

      public ListActivitiesRequestBuilder WithEndTime(string? endTime)
      {
        _endTime = endTime;
        return this;
      }

      public ListActivitiesRequestBuilder WithGetNetworkUnifiedActivities(bool? getNetworkUnifiedActivities)
      {
        _getNetworkUnifiedActivities = getNetworkUnifiedActivities;
        return this;
      }

      public ListActivitiesRequestBuilder WithCursor(string cursor)
      {
        _cursor = cursor;
        return this;
      }

      public ListActivitiesRequestBuilder WithSortDirection(SortDirection sortDirection)
      {
        _sortDirection = sortDirection;
        return this;
      }

      public ListActivitiesRequestBuilder WithLimit(int limit)
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

      public ListActivitiesRequest Build()
      {
        Validate();
        return new ListActivitiesRequest(_portfolioId!)
        {
          Symbols = _symbols ?? [],
          Categories = _categories ?? [],
          Statuses = _statuses ?? [],
          StartTime = _startTime,
          EndTime = _endTime,
          GetNetworkUnifiedActivities = _getNetworkUnifiedActivities,
          Cursor = _cursor,
          SortDirection = _sortDirection,
          Limit = _limit,
        };
      }
    }
  }
}
