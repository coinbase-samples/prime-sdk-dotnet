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

using System;
using Newtonsoft.Json;

namespace Coinbase.Prime.Orders
{
  public class ListOpenOrdersRequest
  {
    [JsonProperty("product_ids")]
    public string[]? ProductIds { get; set; }

    [JsonProperty("order_type")]
    public OrderType? OrderType { get; set; }

    [JsonProperty("start_date"), JsonRequired]
    public DateTime? StartDate { get; set; }

    [JsonProperty("order_side")]
    public OrderSide? OrderSide { get; set; }

    [JsonProperty("end_date")]
    public DateTime? EndDate { get; set; }

    public string? Cursor { get; set; }

    [JsonProperty("sort_direction")]
    public string? sortDirection { get; set; }

    public int? Limit { get; set; }


    public ListOpenOrdersRequest() { }

    private ListOpenOrdersRequest(Builder builder)
    {
      ProductIds = builder.ProductIds;
      OrderType = builder.OrderType;
      StartDate = builder.StartDate;
      OrderSide = builder.OrderSide;
      EndDate = builder.EndDate;
      Cursor = builder.Cursor;
      sortDirection = builder.sortDirection;
      Limit = builder.Limit;
    }

    public class Builder
    {
      public string[]? ProductIds { get; private set; }
      public OrderType? OrderType { get; private set; }
      public DateTime? StartDate { get; private set; }
      public OrderSide? OrderSide { get; private set; }
      public DateTime? EndDate { get; private set; }
      public string? Cursor { get; private set; }
      public string? sortDirection { get; private set; }
      public int? Limit { get; private set; }

      public Builder() { }

      public Builder SetProductIds(string[]? productIds)
      {
        ProductIds = productIds;
        return this;
      }

      public Builder SetOrderType(OrderType? orderType)
      {
        OrderType = orderType;
        return this;
      }

      public Builder SetStartDate(DateTime? startDate)
      {
        StartDate = startDate;
        return this;
      }

      public Builder SetOrderSide(OrderSide? orderSide)
      {
        OrderSide = orderSide;
        return this;
      }

      public Builder SetEndDate(DateTime? endDate)
      {
        EndDate = endDate;
        return this;
      }

      public Builder SetCursor(string? cursor)
      {
        Cursor = cursor;
        return this;
      }

      public Builder SetSortDirection(string? sortDirection)
      {
        sortDirection = sortDirection;
        return this;
      }

      public Builder SetLimit(int? limit)
      {
        Limit = limit;
        return this;
      }

      public ListOpenOrdersRequest Build()
      {
        return new ListOpenOrdersRequest(this);
      }
    }
  }
}
