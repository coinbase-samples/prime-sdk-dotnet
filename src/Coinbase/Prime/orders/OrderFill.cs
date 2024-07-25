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

using Newtonsoft.Json;
using System;

namespace Coinbase.Prime.Orders
{
  public class OrderFill
  {
    public string? Id { get; set; }

    [JsonProperty("order_id")]
    public string? OrderId { get; set; }

    [JsonProperty("product_id")]
    public string? ProductId { get; set; }

    public OrderSide? Side { get; set; }

    [JsonProperty("filled_quantity")]
    public string? FilledQuantity { get; set; }

    [JsonProperty("filled_value")]
    public string? FilledValue { get; set; }

    public string? Price { get; set; }

    public string? Time { get; set; }

    public string? Commission { get; set; }

    public string? Venue { get; set; }

    public OrderFill() { }

    private OrderFill(Builder builder)
    {
      Id = builder.Id;
      OrderId = builder.OrderId;
      ProductId = builder.ProductId;
      Side = builder.Side;
      FilledQuantity = builder.FilledQuantity;
      FilledValue = builder.FilledValue;
      Price = builder.Price;
      Time = builder.Time;
      Commission = builder.Commission;
      Venue = builder.Venue;
    }

    public class Builder
    {
      public string? Id { get; private set; }
      public string? OrderId { get; private set; }
      public string? ProductId { get; private set; }
      public OrderSide? Side { get; private set; }
      public string? FilledQuantity { get; private set; }
      public string? FilledValue { get; private set; }
      public string? Price { get; private set; }
      public string? Time { get; private set; }
      public string? Commission { get; private set; }
      public string? Venue { get; private set; }

      public Builder() { }

      public Builder SetId(string id)
      {
        Id = id;
        return this;
      }

      public Builder SetOrderId(string orderId)
      {
        OrderId = orderId;
        return this;
      }

      public Builder SetProductId(string productId)
      {
        ProductId = productId;
        return this;
      }

      public Builder SetSide(OrderSide side)
      {
        Side = side;
        return this;
      }

      public Builder SetFilledQuantity(string filledQuantity)
      {
        FilledQuantity = filledQuantity;
        return this;
      }

      public Builder SetFilledValue(string filledValue)
      {
        FilledValue = filledValue;
        return this;
      }

      public Builder SetPrice(string price)
      {
        Price = price;
        return this;
      }

      public Builder SetTime(string time)
      {
        Time = time;
        return this;
      }

      public Builder SetCommission(string commission)
      {
        Commission = commission;
        return this;
      }

      public Builder SetVenue(string venue)
      {
        Venue = venue;
        return this;
      }

      public OrderFill Build()
      {
        return new OrderFill(this);
      }
    }
  }
}
