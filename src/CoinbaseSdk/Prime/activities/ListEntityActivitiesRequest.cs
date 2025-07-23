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
  using CoinbaseSdk.Prime.Model;

  public class ListEntityActivitiesRequest(string entityId) : PaginatedRequest
  {
    [JsonIgnore]
    public string EntityId { get; set; } = entityId;

    [JsonPropertyName("activity_level")]
    public ActivityLevel? ActivityLevel { get; set; }
    public string[] Symbols { get; set; } = [];
    public ActivityCategory[] Categories { get; set; } = [];
    public ActivityStatus[] Statuses { get; set; } = [];
    [JsonPropertyName("start_time")]
    public string? StartTime { get; set; }
    [JsonPropertyName("end_time")]
    public string? EndTime { get; set; }
    [JsonPropertyName("sort_direction")]
    public SortDirection? SortDirection { get; set; }

    public class ListEntityActivitiesRequestBuilder : PaginatedRequestBuilder<ListEntityActivitiesRequest, ListEntityActivitiesRequestBuilder>
    {
      private string? _entityId;
      private ActivityLevel? _activityLevel;
      private string[]? _symbols;
      private ActivityCategory[]? _categories;
      private ActivityStatus[]? _statuses;
      private string? _startTime;
      private string? _endTime;
      private SortDirection? _sortDirection;

      public ListEntityActivitiesRequestBuilder WithEntityId(string entityId)
      {
        _entityId = entityId;
        return this;
      }

      public ListEntityActivitiesRequestBuilder WithActivityLevel(ActivityLevel activityLevel)
      {
        _activityLevel = activityLevel;
        return this;
      }

      public ListEntityActivitiesRequestBuilder WithSymbols(string[] symbols)
      {
        _symbols = symbols;
        return this;
      }

      public ListEntityActivitiesRequestBuilder WithCategories(ActivityCategory[] categories)
      {
        _categories = categories;
        return this;
      }

      public ListEntityActivitiesRequestBuilder WithStatuses(ActivityStatus[] statuses)
      {
        _statuses = statuses;
        return this;
      }

      public ListEntityActivitiesRequestBuilder WithStartTime(string startTime)
      {
        _startTime = startTime;
        return this;
      }

      public ListEntityActivitiesRequestBuilder WithEndTime(string endTime)
      {
        _endTime = endTime;
        return this;
      }

      public ListEntityActivitiesRequestBuilder WithSortDirection(SortDirection sortDirection)
      {
        _sortDirection = sortDirection;
        return this;
      }

      public new ListEntityActivitiesRequestBuilder WithPagination(Pagination pagination)
      {
        base.WithPagination(pagination);
        _sortDirection = pagination.SortDirection;
        return this;
      }

      /// <summary>
      /// Validates the builder.
      /// </summary>
      /// <exception cref="CoinbaseClientException">Thrown when <see cref="_entityId" /> is null, empty, or whitespace.</exception>
      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(_entityId))
        {
          throw new CoinbaseClientException("EntityId is required");
        }
      }

      /// <summary>
      /// Builds the <see cref="ListEntityActivitiesRequest"/>.
      /// </summary>
      /// <returns>The <see cref="ListEntityActivitiesRequest"/>.</returns>
      /// <exception cref="CoinbaseClientException">Thrown when <see cref="_entityId" /> is null, empty, or whitespace.</exception>
      public override ListEntityActivitiesRequest Build()
      {
        this.Validate();
        var request = new ListEntityActivitiesRequest(_entityId!)
        {
          ActivityLevel = _activityLevel,
          Symbols = _symbols ?? [],
          Categories = _categories ?? [],
          Statuses = _statuses ?? [],
          StartTime = _startTime,
          EndTime = _endTime,
          SortDirection = _sortDirection
        };
        SetPaginationProperties(request);
        return request;
      }
    }
  }
}
