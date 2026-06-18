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

namespace CoinbaseSdk.Prime.Orders
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Core.Error;
  using CoinbaseSdk.Prime.Common;
  using CoinbaseSdk.Prime.Model.Enums;

  /// <summary>
  /// List Open Orders.
  /// </summary>
  public class ListOpenOrdersRequest(string portfolioId) : PaginatedRequest
  {
    [JsonIgnore]
    public string PortfolioId { get; set; } = portfolioId;

    public string[] ProductIds { get; set; } = [];

    public OrderType? OrderType { get; set; }

    public string? StartDate { get; set; }

    public OrderSide? OrderSide { get; set; }

    public string? EndDate { get; set; }

    public class ListOpenOrdersRequestBuilder
    {
      private string? _portfolioId;
      private string[]? _productIds;
      private OrderType? _orderType;
      private string? _startDate;
      private OrderSide? _orderSide;
      private string? _endDate;
      private string? _cursor;
      private SortDirection? _sortDirection;
      private int? _limit;

      public ListOpenOrdersRequestBuilder WithPortfolioId(string portfolioId)
      {
        _portfolioId = portfolioId;
        return this;
      }

      public ListOpenOrdersRequestBuilder WithProductIds(string[] productIds)
      {
        _productIds = productIds;
        return this;
      }

      public ListOpenOrdersRequestBuilder WithOrderType(OrderType? orderType)
      {
        _orderType = orderType;
        return this;
      }

      public ListOpenOrdersRequestBuilder WithStartDate(string? startDate)
      {
        _startDate = startDate;
        return this;
      }

      public ListOpenOrdersRequestBuilder WithOrderSide(OrderSide? orderSide)
      {
        _orderSide = orderSide;
        return this;
      }

      public ListOpenOrdersRequestBuilder WithEndDate(string? endDate)
      {
        _endDate = endDate;
        return this;
      }

      public ListOpenOrdersRequestBuilder WithCursor(string cursor)
      {
        _cursor = cursor;
        return this;
      }

      public ListOpenOrdersRequestBuilder WithSortDirection(SortDirection sortDirection)
      {
        _sortDirection = sortDirection;
        return this;
      }

      public ListOpenOrdersRequestBuilder WithLimit(int limit)
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

      public ListOpenOrdersRequest Build()
      {
        Validate();
        return new ListOpenOrdersRequest(_portfolioId!)
        {
          ProductIds = _productIds ?? [],
          OrderType = _orderType,
          StartDate = _startDate,
          OrderSide = _orderSide,
          EndDate = _endDate,
          Cursor = _cursor,
          SortDirection = _sortDirection,
          Limit = _limit,
        };
      }
    }
  }
}
