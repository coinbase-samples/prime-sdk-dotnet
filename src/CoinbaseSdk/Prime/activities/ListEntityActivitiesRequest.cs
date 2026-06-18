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

namespace CoinbaseSdk.Prime.Activities
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Core.Error;
  using CoinbaseSdk.Prime.Common;
  using CoinbaseSdk.Prime.Model.Enums;

  /// <summary>
  /// List Entity Activities.
  /// </summary>
  public class ListEntityActivitiesRequest(string entityId) : PaginatedRequest
  {
    [JsonIgnore]
    public string EntityId { get; set; } = entityId;

    public ActivityLevel? ActivityLevel { get; set; }

    public string[] Symbols { get; set; } = [];

    public ActivityCategory?[] Categories { get; set; } = [];

    public ActivityStatus?[] Statuses { get; set; } = [];

    public string? StartTime { get; set; }

    public string? EndTime { get; set; }

    public bool? GetNetworkUnifiedActivities { get; set; }

    public class ListEntityActivitiesRequestBuilder
    {
      private string? _entityId;
      private ActivityLevel? _activityLevel;
      private string[]? _symbols;
      private ActivityCategory?[]? _categories;
      private ActivityStatus?[]? _statuses;
      private string? _startTime;
      private string? _endTime;
      private bool? _getNetworkUnifiedActivities;
      private string? _cursor;
      private SortDirection? _sortDirection;
      private int? _limit;

      public ListEntityActivitiesRequestBuilder WithEntityId(string entityId)
      {
        _entityId = entityId;
        return this;
      }

      public ListEntityActivitiesRequestBuilder WithActivityLevel(ActivityLevel? activityLevel)
      {
        _activityLevel = activityLevel;
        return this;
      }

      public ListEntityActivitiesRequestBuilder WithSymbols(string[] symbols)
      {
        _symbols = symbols;
        return this;
      }

      public ListEntityActivitiesRequestBuilder WithCategories(ActivityCategory?[] categories)
      {
        _categories = categories;
        return this;
      }

      public ListEntityActivitiesRequestBuilder WithStatuses(ActivityStatus?[] statuses)
      {
        _statuses = statuses;
        return this;
      }

      public ListEntityActivitiesRequestBuilder WithStartTime(string? startTime)
      {
        _startTime = startTime;
        return this;
      }

      public ListEntityActivitiesRequestBuilder WithEndTime(string? endTime)
      {
        _endTime = endTime;
        return this;
      }

      public ListEntityActivitiesRequestBuilder WithGetNetworkUnifiedActivities(bool? getNetworkUnifiedActivities)
      {
        _getNetworkUnifiedActivities = getNetworkUnifiedActivities;
        return this;
      }

      public ListEntityActivitiesRequestBuilder WithCursor(string cursor)
      {
        _cursor = cursor;
        return this;
      }

      public ListEntityActivitiesRequestBuilder WithSortDirection(SortDirection sortDirection)
      {
        _sortDirection = sortDirection;
        return this;
      }

      public ListEntityActivitiesRequestBuilder WithLimit(int limit)
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

      public ListEntityActivitiesRequest Build()
      {
        Validate();
        return new ListEntityActivitiesRequest(_entityId!)
        {
          ActivityLevel = _activityLevel,
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
