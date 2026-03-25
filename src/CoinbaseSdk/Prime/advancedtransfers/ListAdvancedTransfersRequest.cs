/*
 * Copyright 2025-present Coinbase Global, Inc.
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

namespace CoinbaseSdk.Prime.AdvancedTransfers
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Prime.Common;
  using CoinbaseSdk.Prime.Model.Enums;

  public class ListAdvancedTransfersRequest(string portfolioId) : PaginatedRequest
  {
    [JsonIgnore]
    public string PortfolioId { get; set; } = portfolioId;

    [JsonPropertyName("state")]
    public string? State { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("start_time")]
    public string? StartTime { get; set; }

    [JsonPropertyName("end_time")]
    public string? EndTime { get; set; }

    [JsonPropertyName("reference_id")]
    public string? ReferenceId { get; set; }

    public class Builder
    {
      private string? _portfolioId;
      private string? _state;
      private string? _type;
      private string? _startTime;
      private string? _endTime;
      private string? _referenceId;
      private string? _cursor;
      private SortDirection? _sortDirection;
      private int? _limit;

      public Builder WithPortfolioId(string portfolioId)
      {
        _portfolioId = portfolioId;
        return this;
      }

      public Builder WithState(string state)
      {
        _state = state;
        return this;
      }

      public Builder WithType(string type)
      {
        _type = type;
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

      public Builder WithReferenceId(string referenceId)
      {
        _referenceId = referenceId;
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

      public ListAdvancedTransfersRequest Build()
      {
        var request = new ListAdvancedTransfersRequest(_portfolioId!)
        {
          State = _state,
          Type = _type,
          StartTime = _startTime,
          EndTime = _endTime,
          ReferenceId = _referenceId,
          Cursor = _cursor,
          SortDirection = _sortDirection,
          Limit = _limit,
        };
        return request;
      }
    }
  }
}
