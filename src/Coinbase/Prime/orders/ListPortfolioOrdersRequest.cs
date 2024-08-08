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

namespace Coinbase.Prime.Orders
{
  using Coinbase.Core.Error;
  using Coinbase.Prime.Common;
  using System.Text.Json.Serialization;
  public class ListPortfolioOrdersRequest(string portfolioId)
  : BaseListRequest(portfolioId, null)
  {
    [JsonPropertyName("order_statuses")]
    public OrderStatus[]? OrderStatuses { get; set; }

    [JsonPropertyName("product_ids")]
    public string[]? ProductIds { get; set; }

    [JsonPropertyName("order_type")]
    public OrderType? OrderType { get; set; }

    [JsonPropertyName("order_side")]
    public OrderSide? OrderSide { get; set; }

    [JsonPropertyName("start_date")]
    public string? StartDate { get; set; }

    [JsonPropertyName("end_date")]
    public string? EndDate { get; set; }


    public class ListPortfolioOrdersRequestBuilder
    {
      private string? _portfolioId;
      private OrderStatus[]? _orderStatuses;
      private string[]? _productIds;
      private OrderType? _orderType;
      private OrderSide? _orderSide;
      private string? _startDate;
      private string? _endDate;
      private string? _cursor;
      private string? _sortDirection;
      private int? _limit;

      public ListPortfolioOrdersRequestBuilder WithPortfolioId(string portfolioId)
      {
        this._portfolioId = portfolioId;
        return this;
      }

      public ListPortfolioOrdersRequestBuilder WithOrderStatuses(OrderStatus[] orderStatuses)
      {
        this._orderStatuses = orderStatuses;
        return this;
      }

      public ListPortfolioOrdersRequestBuilder WithProductIds(string[] productIds)
      {
        this._productIds = productIds;
        return this;
      }

      public ListPortfolioOrdersRequestBuilder WithOrderType(OrderType orderType)
      {
        this._orderType = orderType;
        return this;
      }

      public ListPortfolioOrdersRequestBuilder WithOrderSide(OrderSide orderSide)
      {
        this._orderSide = orderSide;
        return this;
      }

      public ListPortfolioOrdersRequestBuilder WithStartDate(string startDate)
      {
        this._startDate = startDate;
        return this;
      }

      public ListPortfolioOrdersRequestBuilder WithEndDate(string endDate)
      {
        this._endDate = endDate;
        return this;
      }

      public ListPortfolioOrdersRequestBuilder WithCursor(string cursor)
      {
        this._cursor = cursor;
        return this;
      }

      public ListPortfolioOrdersRequestBuilder WithSortDirection(string sortDirection)
      {
        this._sortDirection = sortDirection;
        return this;
      }

      public ListPortfolioOrdersRequestBuilder WithLimit(int limit)
      {
        this._limit = limit;
        return this;
      }

      /// <summary>
      /// Validates the builder.
      /// </summary>
      /// <exception cref="CoinbaseClientException">Thrown when <see cref="_portfolioId" /> is null, empty, or whitespace.</exception>
      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(this._portfolioId))
        {
          throw new CoinbaseClientException("PortfolioId is required");
        }
      }

      /// <summary>
      /// Builds the <see cref="ListPortfolioOrdersRequest"/>.
      /// </summary>
      /// <returns>The <see cref="ListPortfolioOrdersRequest"/>.</returns>
      /// <exception cref="CoinbaseClientException">Thrown when required fields are not provided.</exception>
      public ListPortfolioOrdersRequest Build()
      {
        Validate();
        return new ListPortfolioOrdersRequest(this._portfolioId!)
        {
          OrderStatuses = this._orderStatuses,
          ProductIds = this._productIds,
          OrderType = this._orderType,
          OrderSide = this._orderSide,
          StartDate = this._startDate,
          EndDate = this._endDate,
          Cursor = this._cursor,
          SortDirection = this._sortDirection,
          Limit = this._limit
        };
      }
    }
  }
}
