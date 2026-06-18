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

namespace CoinbaseSdk.Prime.Orders
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Core.Error;
  using CoinbaseSdk.Prime.Common;
  using CoinbaseSdk.Prime.Model.Enums;

  /// <summary>
  /// List Portfolio Fills.
  /// </summary>
  public class ListPortfolioFillsRequest(string portfolioId) : PaginatedRequest
  {
    [JsonIgnore]
    public string PortfolioId { get; set; } = portfolioId;

    public string? StartDate { get; set; }

    public string? EndDate { get; set; }

    public class ListPortfolioFillsRequestBuilder
    {
      private string? _portfolioId;
      private string? _startDate;
      private string? _endDate;
      private string? _cursor;
      private SortDirection? _sortDirection;
      private int? _limit;

      public ListPortfolioFillsRequestBuilder WithPortfolioId(string portfolioId)
      {
        _portfolioId = portfolioId;
        return this;
      }

      public ListPortfolioFillsRequestBuilder WithStartDate(string? startDate)
      {
        _startDate = startDate;
        return this;
      }

      public ListPortfolioFillsRequestBuilder WithEndDate(string? endDate)
      {
        _endDate = endDate;
        return this;
      }

      public ListPortfolioFillsRequestBuilder WithCursor(string cursor)
      {
        _cursor = cursor;
        return this;
      }

      public ListPortfolioFillsRequestBuilder WithSortDirection(SortDirection sortDirection)
      {
        _sortDirection = sortDirection;
        return this;
      }

      public ListPortfolioFillsRequestBuilder WithLimit(int limit)
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

      public ListPortfolioFillsRequest Build()
      {
        Validate();
        return new ListPortfolioFillsRequest(_portfolioId!)
        {
          StartDate = _startDate,
          EndDate = _endDate,
          Cursor = _cursor,
          SortDirection = _sortDirection,
          Limit = _limit,
        };
      }
    }
  }
}
