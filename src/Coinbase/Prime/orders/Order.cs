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

namespace Coinbase.Prime.Orders
{
  public class Order
  {
    public string? Id { get; set; }

    [JsonProperty("user_id")]
    public string? UserId { get; set; }

    [JsonProperty("portfolio_id")]
    public string? PortfolioId { get; set; }

    [JsonProperty("product_id")]
    public string? ProductId { get; set; }

    public OrderSide? Side { get; set; }

    [JsonProperty("client_order_id")]
    public string? ClientOrderId { get; set; }

    public OrderType? Type { get; set; }

    [JsonProperty("base_quantity")]
    public string? BaseQuantity { get; set; }

    [JsonProperty("quote_value")]
    public string? QuoteValue { get; set; }

    [JsonProperty("limit_price")]
    public string? LimitPrice { get; set; }

    [JsonProperty("stop_price")]
    public string? StopPrice { get; set; }

    [JsonProperty("start_time")]
    public string? StartTime { get; set; }

    [JsonProperty("expiry_time")]
    public string? ExpiryTime { get; set; }

    public OrderStatus? Status { get; set; }

    [JsonProperty("time_in_force")]
    public TimeInForce? TimeInForce { get; set; }

    [JsonProperty("created_at")]
    public string? CreatedAt { get; set; }

    [JsonProperty("filled_quantity")]
    public string? FilledQuantity { get; set; }

    [JsonProperty("filled_value")]
    public string? FilledValue { get; set; }

    [JsonProperty("average_filled_price")]
    public string? AverageFilledPrice { get; set; }

    public string? Commission { get; set; }

    [JsonProperty("exchange_fee")]
    public string? ExchangeFee { get; set; }

    [JsonProperty("historical_pov")]
    public string? HistoricalPov { get; set; }

    public Order() { }

    private Order(Builder builder)
    {
      Id = builder.Id;
      UserId = builder.UserId;
      PortfolioId = builder.PortfolioId;
      ProductId = builder.ProductId;
      Side = builder.Side;
      ClientOrderId = builder.ClientOrderId;
      Type = builder.Type;
      BaseQuantity = builder.BaseQuantity;
      QuoteValue = builder.QuoteValue;
      LimitPrice = builder.LimitPrice;
      StopPrice = builder.StopPrice;
      StartTime = builder.StartTime;
      ExpiryTime = builder.ExpiryTime;
      Status = builder.Status;
      TimeInForce = builder.TimeInForce;
      CreatedAt = builder.CreatedAt;
      FilledQuantity = builder.FilledQuantity;
      FilledValue = builder.FilledValue;
      AverageFilledPrice = builder.AverageFilledPrice;
      Commission = builder.Commission;
      ExchangeFee = builder.ExchangeFee;
      HistoricalPov = builder.HistoricalPov;
    }

    public class Builder
    {
      public string? Id { get; private set; }
      public string? UserId { get; private set; }
      public string? PortfolioId { get; private set; }
      public string? ProductId { get; private set; }
      public OrderSide? Side { get; private set; }
      public string? ClientOrderId { get; private set; }
      public OrderType? Type { get; private set; }
      public string? BaseQuantity { get; private set; }
      public string? QuoteValue { get; private set; }
      public string? LimitPrice { get; private set; }
      public string? StopPrice { get; private set; }
      public string? StartTime { get; private set; }
      public string? ExpiryTime { get; private set; }
      public OrderStatus? Status { get; private set; }
      public TimeInForce? TimeInForce { get; private set; }
      public string? CreatedAt { get; private set; }
      public string? FilledQuantity { get; private set; }
      public string? FilledValue { get; private set; }
      public string? AverageFilledPrice { get; private set; }
      public string? Commission { get; private set; }
      public string? ExchangeFee { get; private set; }
      public string? HistoricalPov { get; private set; }

      public Builder() { }

      public Builder SetId(string? id)
      {
        Id = id;
        return this;
      }

      public Builder SetUserId(string? userId)
      {
        UserId = userId;
        return this;
      }

      public Builder SetPortfolioId(string? portfolioId)
      {
        PortfolioId = portfolioId;
        return this;
      }

      public Builder SetProductId(string? productId)
      {
        ProductId = productId;
        return this;
      }

      public Builder SetSide(OrderSide? side)
      {
        Side = side;
        return this;
      }

      public Builder SetClientOrderId(string? clientOrderId)
      {
        ClientOrderId = clientOrderId;
        return this;
      }

      public Builder SetType(OrderType? type)
      {
        Type = type;
        return this;
      }

      public Builder SetBaseQuantity(string? baseQuantity)
      {
        BaseQuantity = baseQuantity;
        return this;
      }

      public Builder SetQuoteValue(string? quoteValue)
      {
        QuoteValue = quoteValue;
        return this;
      }

      public Builder SetLimitPrice(string? limitPrice)
      {
        LimitPrice = limitPrice;
        return this;
      }

      public Builder SetStopPrice(string? stopPrice)
      {
        StopPrice = stopPrice;
        return this;
      }

      public Builder SetStartTime(string? startTime)
      {
        StartTime = startTime;
        return this;
      }

      public Builder SetExpiryTime(string? expiryTime)
      {
        ExpiryTime = expiryTime;
        return this;
      }

      public Builder SetStatus(OrderStatus? status)
      {
        Status = status;
        return this;
      }

      public Builder SetTimeInForce(TimeInForce? timeInForce)
      {
        TimeInForce = timeInForce;
        return this;
      }

      public Builder SetCreatedAt(string? createdAt)
      {
        CreatedAt = createdAt;
        return this;
      }

      public Builder SetFilledQuantity(string? filledQuantity)
      {
        FilledQuantity = filledQuantity;
        return this;
      }

      public Builder SetFilledValue(string? filledValue)
      {
        FilledValue = filledValue;
        return this;
      }

      public Builder SetAverageFilledPrice(string? averageFilledPrice)
      {
        AverageFilledPrice = averageFilledPrice;
        return this;
      }

      public Builder SetCommission(string? commission)
      {
        Commission = commission;
        return this;
      }

      public Builder SetExchangeFee(string? exchangeFee)
      {
        ExchangeFee = exchangeFee;
        return this;
      }

      public Builder SetHistoricalPov(string? historicalPov)
      {
        HistoricalPov = historicalPov;
        return this;
      }

      public Order Build()
      {
        return new Order(this);
      }
    }
  }
}
