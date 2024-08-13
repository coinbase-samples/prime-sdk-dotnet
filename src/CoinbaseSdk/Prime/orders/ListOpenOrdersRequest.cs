/*
 * Copyright 2024-present Coinbase Global, Inc.
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

namespace CoinbaseSdk.Prime.Orders
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Prime.Common;

  public class ListOpenOrdersRequest(string portfolioId)
  : BaseListRequest(portfolioId, null)
  {
    [JsonPropertyName("product_ids")]
    public string[]? ProductIds { get; set; }

    [JsonPropertyName("order_type")]
    public OrderType? OrderType { get; set; }

    [JsonPropertyName("start_date"), JsonRequired]
    public DateTime? StartDate { get; set; }

    [JsonPropertyName("order_side")]
    public OrderSide? OrderSide { get; set; }

    [JsonPropertyName("end_date")]
    public DateTime? EndDate { get; set; }

    public class ListOpenOrdersRequestBuilder
    {
      private string? _portfolioId;
      private string[]? _productIds;
      private OrderType? _orderType;
      private DateTime? _startDate;
      private OrderSide? _orderSide;
      private DateTime? _endDate;
      private string? _cursor;
      private string? _sortDirection;
      private int? _limit;

      public ListOpenOrdersRequestBuilder WithPortfolioId(string portfolioId)
      {
        this._portfolioId = portfolioId;
        return this;
      }

      public ListOpenOrdersRequestBuilder WithProductIds(string[] productIds)
      {
        this._productIds = productIds;
        return this;
      }

      public ListOpenOrdersRequestBuilder WithOrderType(OrderType orderType)
      {
        this._orderType = orderType;
        return this;
      }

      public ListOpenOrdersRequestBuilder WithStartDate(DateTime startDate)
      {
        this._startDate = startDate;
        return this;
      }

      public ListOpenOrdersRequestBuilder WithOrderSide(OrderSide orderSide)
      {
        this._orderSide = orderSide;
        return this;
      }

      public ListOpenOrdersRequestBuilder WithEndDate(DateTime endDate)
      {
        this._endDate = endDate;
        return this;
      }

      public ListOpenOrdersRequestBuilder WithCursor(string cursor)
      {
        this._cursor = cursor;
        return this;
      }

      public ListOpenOrdersRequestBuilder WithSortDirection(string sortDirection)
      {
        this._sortDirection = sortDirection;
        return this;
      }

      public ListOpenOrdersRequestBuilder WithLimit(int limit)
      {
        this._limit = limit;
        return this;
      }

      public ListOpenOrdersRequestBuilder WithPagination(Pagination pagination)
      {
        this._cursor = pagination.NextCursor;
        this._sortDirection = pagination.SortDirection;
        return this;
      }

      public ListOpenOrdersRequest Build()
      {
        return new ListOpenOrdersRequest(this._portfolioId!)
        {
          ProductIds = this._productIds,
          OrderType = this._orderType,
          StartDate = this._startDate,
          OrderSide = this._orderSide,
          EndDate = this._endDate,
          Cursor = this._cursor,
          SortDirection = this._sortDirection,
          Limit = this._limit
        };
      }
    }
  }
}
