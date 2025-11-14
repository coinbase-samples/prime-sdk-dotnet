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

  public class ListActivitiesRequest(string portfolioId) : PaginatedRequest
  {
    [JsonIgnore]
    public string PortfolioId { get; set; } = portfolioId;

    public string[] Symbols { get; set; } = [];

    public ActivityCategory?[] Categories { get; set; } = [];

    public ActivityStatus?[] Statuses { get; set; } = [];

    [JsonPropertyName("start_time")]
    public string? StartTime { get; set; }

    [JsonPropertyName("end_time")]
    public string? EndTime { get; set; }

    public class Builder
    {
      private string? _portfolioId;
      private string[] _symbols = [];
      private ActivityCategory?[] _categories = [];
      private ActivityStatus?[] _statuses = [];
      private string? _startTime;
      private string? _endTime;
      private string? _cursor;
      private SortDirection? _sortDirection;
      private int? _limit;

      public Builder WithPortfolioId(string portfolioId)
      {
        _portfolioId = portfolioId;
        return this;
      }

      public Builder WithSymbols(string[] symbols)
      {
        _symbols = symbols;
        return this;
      }

      public Builder WithCategories(ActivityCategory?[] categories)
      {
        _categories = categories;
        return this;
      }

      public Builder WithStatuses(ActivityStatus?[] statuses)
      {
        _statuses = statuses;
        return this;
      }

      public Builder WithStartTime(string startTime)
      {
        _startTime = startTime;
        return this;
      }

      public Builder WithEndTime(string endTime)
      {
        _endTime = endTime;
        return this;
      }

      public Builder WithSortDirection(SortDirection sortDirection)
      {
        _sortDirection = sortDirection;
        return this;
      }

      public Builder WithCursor(string cursor)
      {
        _cursor = cursor;
        return this;
      }

      public Builder WithLimit(int limit)
      {
        _limit = limit;
        return this;
      }

      /// <summary>
      /// Validates the builder.
      /// </summary>
      /// <exception cref="CoinbaseClientException">Thrown when <see cref="_portfolioId" /> is null, empty, or whitespace.</exception>
      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(_portfolioId))
        {
          throw new CoinbaseClientException("PortfolioId is required");
        }
      }

      /// <summary>
      /// Builds the <see cref="ListActivitiesRequest"/>.
      /// </summary>
      /// <returns>The <see cref="ListActivitiesRequest"/>.</returns>
      /// <exception cref="CoinbaseClientException">Thrown when <see cref="_portfolioId" /> is null, empty, or whitespace.</exception>
      public ListActivitiesRequest Build()
      {
        Validate();
        var request = new ListActivitiesRequest(_portfolioId!)
        {
          Symbols = _symbols ?? [],
          Categories = _categories ?? [],
          Statuses = _statuses ?? [],
          StartTime = _startTime,
          EndTime = _endTime,
          Cursor = _cursor,
          SortDirection = _sortDirection,
          Limit = _limit,
        };
        return request;
      }
    }
  }
}
