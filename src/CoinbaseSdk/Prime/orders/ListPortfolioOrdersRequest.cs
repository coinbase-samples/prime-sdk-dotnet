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

  public class ListPortfolioOrdersRequest(string portfolioId) : PaginatedRequest
  {
    [JsonIgnore]
    public string PortfolioId { get; set; } = portfolioId;

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

    public class Builder
    {
      private string? _portfolioId;
      private OrderStatus[]? _orderStatuses;
      private string[]? _productIds;
      private OrderType? _orderType;
      private OrderSide? _orderSide;
      private string? _startDate;
      private string? _endDate;
      private string? _cursor;
      private SortDirection? _sortDirection;
      private int? _limit;

      public Builder WithPortfolioId(string portfolioId)
      {
        _portfolioId = portfolioId;
        return this;
      }

      public Builder WithOrderStatuses(OrderStatus[] orderStatuses)
      {
        _orderStatuses = orderStatuses;
        return this;
      }

      public Builder WithProductIds(string[] productIds)
      {
        _productIds = productIds;
        return this;
      }

      public Builder WithOrderType(OrderType orderType)
      {
        _orderType = orderType;
        return this;
      }

      public Builder WithOrderSide(OrderSide orderSide)
      {
        _orderSide = orderSide;
        return this;
      }

      public Builder WithStartDate(string startDate)
      {
        _startDate = startDate;
        return this;
      }

      public Builder WithEndDate(string endDate)
      {
        _endDate = endDate;
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
      /// Builds the <see cref="ListPortfolioOrdersRequest"/>.
      /// </summary>
      /// <returns>The <see cref="ListPortfolioOrdersRequest"/>.</returns>
      /// <exception cref="CoinbaseClientException">Thrown when required fields are not provided.</exception>
      public ListPortfolioOrdersRequest Build()
      {
        Validate();
        var request = new ListPortfolioOrdersRequest(_portfolioId!)
        {
          OrderStatuses = _orderStatuses,
          ProductIds = _productIds,
          OrderType = _orderType,
          OrderSide = _orderSide,
          StartDate = _startDate,
          EndDate = _endDate,
          Cursor = _cursor,
          SortDirection = _sortDirection,
          Limit = _limit,
        };
        return request;
      }
    }
  }
}
