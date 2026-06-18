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

namespace CoinbaseSdk.Prime.AdvancedTransfer
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Core.Error;
  using CoinbaseSdk.Prime.Common;
  using CoinbaseSdk.Prime.Model.Enums;

  /// <summary>
  /// List Advanced Transfers.
  /// </summary>
  public class ListAdvancedTransfersRequest(string portfolioId) : PaginatedRequest
  {
    [JsonIgnore]
    public string PortfolioId { get; set; } = portfolioId;

    public string? State { get; set; }

    public string? Type { get; set; }

    public string? StartTime { get; set; }

    public string? EndTime { get; set; }

    public string? ReferenceId { get; set; }

    public class ListAdvancedTransfersRequestBuilder
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

      public ListAdvancedTransfersRequestBuilder WithPortfolioId(string portfolioId)
      {
        _portfolioId = portfolioId;
        return this;
      }

      public ListAdvancedTransfersRequestBuilder WithState(string? state)
      {
        _state = state;
        return this;
      }

      public ListAdvancedTransfersRequestBuilder WithType(string? type)
      {
        _type = type;
        return this;
      }

      public ListAdvancedTransfersRequestBuilder WithStartTime(string? startTime)
      {
        _startTime = startTime;
        return this;
      }

      public ListAdvancedTransfersRequestBuilder WithEndTime(string? endTime)
      {
        _endTime = endTime;
        return this;
      }

      public ListAdvancedTransfersRequestBuilder WithReferenceId(string? referenceId)
      {
        _referenceId = referenceId;
        return this;
      }

      public ListAdvancedTransfersRequestBuilder WithCursor(string cursor)
      {
        _cursor = cursor;
        return this;
      }

      public ListAdvancedTransfersRequestBuilder WithSortDirection(SortDirection sortDirection)
      {
        _sortDirection = sortDirection;
        return this;
      }

      public ListAdvancedTransfersRequestBuilder WithLimit(int limit)
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

      public ListAdvancedTransfersRequest Build()
      {
        Validate();
        return new ListAdvancedTransfersRequest(_portfolioId!)
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
      }
    }
  }
}
