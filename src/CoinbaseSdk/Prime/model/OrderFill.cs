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

namespace CoinbaseSdk.Prime.Model
{
  using System.Text.Json.Serialization;

  public class OrderFill
  {
    public string? Id { get; set; }

    [JsonPropertyName("order_id")]
    public string? OrderId { get; set; }

    [JsonPropertyName("product_id")]
    public string? ProductId { get; set; }

    [JsonPropertyName("client_product_id")]
    public string? ClientProductId { get; set; }

    public OrderSide? Side { get; set; }

    [JsonPropertyName("filled_quantity")]
    public string? FilledQuantity { get; set; }

    [JsonPropertyName("filled_value")]
    public string? FilledValue { get; set; }

    public string? Price { get; set; }

    public string? Time { get; set; }

    public string? Commission { get; set; }

    public string? Venue { get; set; }

    [JsonPropertyName("venue_fees")]
    public string? VenueFees { get; set; }

    [JsonPropertyName("ces_commission")]
    public string? CesCommission { get; set; }

    public OrderFill() { }

    public class OrderFillBuilder
    {
      private string? _id;
      private string? _orderId;
      private string? _productId;
      private string? _clientProductId;
      private OrderSide? _side;
      private string? _filledQuantity;
      private string? _filledValue;
      private string? _price;
      private string? _time;
      private string? _commission;
      private string? _venue;
      private string? _venueFees;
      private string? _cesCommission;

      public OrderFillBuilder WithId(string? id)
      {
        this._id = id;
        return this;
      }

      public OrderFillBuilder WithOrderId(string? orderId)
      {
        this._orderId = orderId;
        return this;
      }

      public OrderFillBuilder WithProductId(string? productId)
      {
        this._productId = productId;
        return this;
      }

      public OrderFillBuilder WithClientProductId(string? clientProductId)
      {
        this._clientProductId = clientProductId;
        return this;
      }

      public OrderFillBuilder WithSide(OrderSide? side)
      {
        this._side = side;
        return this;
      }

      public OrderFillBuilder WithFilledQuantity(string? filledQuantity)
      {
        this._filledQuantity = filledQuantity;
        return this;
      }

      public OrderFillBuilder WithFilledValue(string? filledValue)
      {
        this._filledValue = filledValue;
        return this;
      }

      public OrderFillBuilder WithPrice(string? price)
      {
        this._price = price;
        return this;
      }

      public OrderFillBuilder WithTime(string? time)
      {
        this._time = time;
        return this;
      }

      public OrderFillBuilder WithCommission(string? commission)
      {
        this._commission = commission;
        return this;
      }

      public OrderFillBuilder WithVenue(string? venue)
      {
        this._venue = venue;
        return this;
      }

      public OrderFillBuilder WithVenueFees(string? venueFees)
      {
        this._venueFees = venueFees;
        return this;
      }

      public OrderFillBuilder WithCesCommission(string? cesCommission)
      {
        this._cesCommission = cesCommission;
        return this;
      }

      public OrderFill Build()
      {
        return new OrderFill
        {
          Id = this._id,
          OrderId = this._orderId,
          ProductId = this._productId,
          ClientProductId = this._clientProductId,
          Side = this._side,
          FilledQuantity = this._filledQuantity,
          FilledValue = this._filledValue,
          Price = this._price,
          Time = this._time,
          Commission = this._commission,
          Venue = this._venue,
          VenueFees = this._venueFees,
          CesCommission = this._cesCommission,
        };
      }
    }
  }
}
