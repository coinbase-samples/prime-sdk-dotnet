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
  using CoinbaseSdk.Core.Error;
  using CoinbaseSdk.Prime.Common;
  using System.Text.Json.Serialization;
  public class ListActivitiesRequest(string portfolioId)
  : BaseListRequest(portfolioId, null)
  {
    public string[] Symbols { get; set; } = [];
    public string[] Categories { get; set; } = [];
    public string[] Statuses { get; set; } = [];
    [JsonPropertyName("start_time")]
    public string? StartTime { get; set; }
    [JsonPropertyName("end_time")]
    public string? EndTime { get; set; }


    public class ListActivitiesRequestBuilder
    {
      private string? _portfolioId;
      private string[]? _symbols;
      private string[]? _categories;
      private string[]? _statuses;
      private string? _startTime;
      private string? _endTime;
      private string? _cursor;
      private string? _sortDirection;
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

      public ListActivitiesRequestBuilder WithCategories(string[] categories)
      {
        _categories = categories;
        return this;
      }

      public ListActivitiesRequestBuilder WithStatuses(string[] statuses)
      {
        _statuses = statuses;
        return this;
      }

      public ListActivitiesRequestBuilder WithStartTime(string startTime)
      {
        _startTime = startTime;
        return this;
      }

      public ListActivitiesRequestBuilder WithEndTime(string endTime)
      {
        _endTime = endTime;
        return this;
      }

      public ListActivitiesRequestBuilder WithCursor(string cursor)
      {
        _cursor = cursor;
        return this;
      }

      public ListActivitiesRequestBuilder WithSortDirection(string sortDirection)
      {
        _sortDirection = sortDirection;
        return this;
      }

      public ListActivitiesRequestBuilder WithLimit(int limit)
      {
        _limit = limit;
        return this;
      }

      public ListActivitiesRequestBuilder WithPagination(Pagination pagination)
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
        this.Validate();
        return new ListActivitiesRequest(_portfolioId!)
        {
          Symbols = _symbols ?? new string[] { },
          Categories = _categories ?? new string[] { },
          Statuses = _statuses ?? new string[] { },
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
