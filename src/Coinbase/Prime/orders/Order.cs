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
  using System.Text.Json.Serialization;
  public class Order
  {
    public string? Id { get; set; }

    [JsonPropertyName("user_id")]
    public string? UserId { get; set; }

    [JsonPropertyName("portfolio_id")]
    public string? PortfolioId { get; set; }

    [JsonPropertyName("product_id")]
    public string? ProductId { get; set; }

    public OrderSide? Side { get; set; }

    [JsonPropertyName("client_order_id")]
    public string? ClientOrderId { get; set; }

    public OrderType? Type { get; set; }

    [JsonPropertyName("base_quantity")]
    public string? BaseQuantity { get; set; }

    [JsonPropertyName("quote_value")]
    public string? QuoteValue { get; set; }

    [JsonPropertyName("limit_price")]
    public string? LimitPrice { get; set; }

    [JsonPropertyName("stop_price")]
    public string? StopPrice { get; set; }

    [JsonPropertyName("start_time")]
    public string? StartTime { get; set; }

    [JsonPropertyName("expiry_time")]
    public string? ExpiryTime { get; set; }

    public OrderStatus? Status { get; set; }

    [JsonPropertyName("time_in_force")]
    public TimeInForce? TimeInForce { get; set; }

    [JsonPropertyName("created_at")]
    public string? CreatedAt { get; set; }

    [JsonPropertyName("filled_quantity")]
    public string? FilledQuantity { get; set; }

    [JsonPropertyName("filled_value")]
    public string? FilledValue { get; set; }

    [JsonPropertyName("average_filled_price")]
    public string? AverageFilledPrice { get; set; }

    public string? Commission { get; set; }

    [JsonPropertyName("exchange_fee")]
    public string? ExchangeFee { get; set; }

    [JsonPropertyName("historical_pov")]
    public string? HistoricalPov { get; set; }

    public Order() { }

    public class OrderBuilder
    {
      private string? _id;
      private string? _userId;
      private string? _portfolioId;
      private string? _productId;
      private OrderSide? _side;
      private string? _clientOrderId;
      private OrderType? _type;
      private string? _baseQuantity;
      private string? _quoteValue;
      private string? _limitPrice;
      private string? _stopPrice;
      private string? _startTime;
      private string? _expiryTime;
      private OrderStatus? _status;
      private TimeInForce? _timeInForce;
      private string? _createdAt;
      private string? _filledQuantity;
      private string? _filledValue;
      private string? _averageFilledPrice;
      private string? _commission;
      private string? _exchangeFee;
      private string? _historicalPov;

      public OrderBuilder WithId(string id)
      {
        this._id = id;
        return this;
      }

      public OrderBuilder WithUserId(string userId)
      {
        this._userId = userId;
        return this;
      }

      public OrderBuilder WithPortfolioId(string portfolioId)
      {
        this._portfolioId = portfolioId;
        return this;
      }

      public OrderBuilder WithProductId(string productId)
      {
        this._productId = productId;
        return this;
      }

      public OrderBuilder WithSide(OrderSide side)
      {
        this._side = side;
        return this;
      }

      public OrderBuilder WithClientOrderId(string clientOrderId)
      {
        this._clientOrderId = clientOrderId;
        return this;
      }

      public OrderBuilder WithType(OrderType type)
      {
        this._type = type;
        return this;
      }

      public OrderBuilder WithBaseQuantity(string baseQuantity)
      {
        this._baseQuantity = baseQuantity;
        return this;
      }

      public OrderBuilder WithQuoteValue(string quoteValue)
      {
        this._quoteValue = quoteValue;
        return this;
      }

      public OrderBuilder WithLimitPrice(string limitPrice)
      {
        this._limitPrice = limitPrice;
        return this;
      }

      public OrderBuilder WithStopPrice(string stopPrice)
      {
        this._stopPrice = stopPrice;
        return this;
      }

      public OrderBuilder WithStartTime(string startTime)
      {
        this._startTime = startTime;
        return this;
      }

      public OrderBuilder WithExpiryTime(string expiryTime)
      {
        this._expiryTime = expiryTime;
        return this;
      }

      public OrderBuilder WithStatus(OrderStatus status)
      {
        this._status = status;
        return this;
      }

      public OrderBuilder WithTimeInForce(TimeInForce timeInForce)
      {
        this._timeInForce = timeInForce;
        return this;
      }

      public OrderBuilder WithCreatedAt(string createdAt)
      {
        this._createdAt = createdAt;
        return this;
      }

      public OrderBuilder WithFilledQuantity(string filledQuantity)
      {
        this._filledQuantity = filledQuantity;
        return this;
      }

      public OrderBuilder WithFilledValue(string filledValue)
      {
        this._filledValue = filledValue;
        return this;
      }

      public OrderBuilder WithAverageFilledPrice(string averageFilledPrice)
      {
        this._averageFilledPrice = averageFilledPrice;
        return this;
      }

      public OrderBuilder WithCommission(string commission)
      {
        this._commission = commission;
        return this;
      }

      public OrderBuilder WithExchangeFee(string exchangeFee)
      {
        this._exchangeFee = exchangeFee;
        return this;
      }

      public OrderBuilder WithHistoricalPov(string historicalPov)
      {
        this._historicalPov = historicalPov;
        return this;
      }

      public Order Build()
      {
        return new Order
        {
          Id = this._id,
          UserId = this._userId,
          PortfolioId = this._portfolioId,
          ProductId = this._productId,
          Side = this._side,
          ClientOrderId = this._clientOrderId,
          Type = this._type,
          BaseQuantity = this._baseQuantity,
          QuoteValue = this._quoteValue,
          LimitPrice = this._limitPrice,
          StopPrice = this._stopPrice,
          StartTime = this._startTime,
          ExpiryTime = this._expiryTime,
          Status = this._status,
          TimeInForce = this._timeInForce,
          CreatedAt = this._createdAt,
          FilledQuantity = this._filledQuantity,
          FilledValue = this._filledValue,
          AverageFilledPrice = this._averageFilledPrice,
          Commission = this._commission,
          ExchangeFee = this._exchangeFee,
          HistoricalPov = this._historicalPov
        };
      }
    }
  }
}