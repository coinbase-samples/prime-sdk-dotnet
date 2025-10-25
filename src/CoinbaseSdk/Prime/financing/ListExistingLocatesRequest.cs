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

namespace CoinbaseSdk.Prime.Financing
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Prime.Common;
  using CoinbaseSdk.Prime.Model.Enums;

  public class ListExistingLocatesRequest(string portfolioId) : PaginatedRequest
  {
    [JsonIgnore]
    public string PortfolioId { get; set; } = portfolioId;

    [JsonPropertyName("locate_ids")]
    public string[] LocateIds { get; set; } = [];

    [JsonPropertyName("conversion_date")]
    public string? ConversionDate { get; set; }

    [JsonPropertyName("locate_date")]
    public string? LocateDate { get; set; }

    public class Builder
    {
      private string[] _locateIds = [];
      private string? _conversionDate;
      private string? _locateDate;
      private string? _cursor;
      private SortDirection? _sortDirection;
      private int? _limit;

      public Builder WithLocateIds(string[] locateIds)
      {
        _locateIds = locateIds;
        return this;
      }

      public Builder WithConversionDate(string conversionDate)
      {
        _conversionDate = conversionDate;
        return this;
      }

      public Builder WithLocateDate(string locateDate)
      {
        _locateDate = locateDate;
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

      public ListExistingLocatesRequest Build(string portfolioId)
      {
        return new ListExistingLocatesRequest(portfolioId)
        {
          LocateIds = _locateIds,
          ConversionDate = _conversionDate,
          LocateDate = _locateDate,
          Cursor = _cursor,
          SortDirection = _sortDirection,
          Limit = _limit
        };
      }
    }
  }
}
