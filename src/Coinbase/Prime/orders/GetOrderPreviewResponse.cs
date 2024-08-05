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
  using Coinbase.Core.Error;

  public class GetOrderPreviewResponse
  {
    [JsonPropertyName("portfolio_id")]
    public string? PortfolioId { get; set; }

    [JsonPropertyName("product_id")]
    public string? ProductId { get; set; }

    public OrderSide? Side { get; set; }
    public OrderType? Type { get; set; }

    [JsonPropertyName("base_quantity")]
    public string? BaseQuantity { get; set; }

    [JsonPropertyName("quote_value")]
    public string? QuoteValue { get; set; }

    [JsonPropertyName("limit_price")]
    public string? LimitPrice { get; set; }

    [JsonPropertyName("start_time")]
    public string? StartTime { get; set; }

    [JsonPropertyName("expiry_time")]
    public string? ExpiryTime { get; set; }

    [JsonPropertyName("time_in_force")]
    public TimeInForce TimeInForce { get; set; }

    public string? Commission { get; set; }
    public string? Slippage { get; set; }

    [JsonPropertyName("best_bid")]
    public string? BestBid { get; set; }

    [JsonPropertyName("best_ask")]
    public string? BestAsk { get; set; }

    [JsonPropertyName("average_filled_price")]
    public string? AverageFilledPrice { get; set; }

    [JsonPropertyName("order_total")]
    public string? OrderTotal { get; set; }


    public GetOrderPreviewResponse() { }

    public class GetOrderPreviewResponseBuilder
    {
      private string? _portfolioId;
      private string? _productId;
      private OrderSide? _side;
      private OrderType? _type;
      private string? _baseQuantity;
      private string? _quoteValue;
      private string? _limitPrice;
      private string? _startTime;
      private string? _expiryTime;
      private TimeInForce _timeInForce;
      private string? _commission;
      private string? _slippage;
      private string? _bestBid;
      private string? _bestAsk;
      private string? _averageFilledPrice;
      private string? _orderTotal;

      public GetOrderPreviewResponseBuilder WithPortfolioId(string portfolioId)
      {
        this._portfolioId = portfolioId;
        return this;
      }

      public GetOrderPreviewResponseBuilder WithProductId(string productId)
      {
        this._productId = productId;
        return this;
      }

      public GetOrderPreviewResponseBuilder WithSide(OrderSide side)
      {
        this._side = side;
        return this;
      }

      public GetOrderPreviewResponseBuilder WithType(OrderType type)
      {
        this._type = type;
        return this;
      }

      public GetOrderPreviewResponseBuilder WithBaseQuantity(string baseQuantity)
      {
        this._baseQuantity = baseQuantity;
        return this;
      }

      public GetOrderPreviewResponseBuilder WithQuoteValue(string quoteValue)
      {
        this._quoteValue = quoteValue;
        return this;
      }

      public GetOrderPreviewResponseBuilder WithLimitPrice(string limitPrice)
      {
        this._limitPrice = limitPrice;
        return this;
      }

      public GetOrderPreviewResponseBuilder WithStartTime(string startTime)
      {
        this._startTime = startTime;
        return this;
      }

      public GetOrderPreviewResponseBuilder WithExpiryTime(string expiryTime)
      {
        this._expiryTime = expiryTime;
        return this;
      }

      public GetOrderPreviewResponseBuilder WithTimeInForce(TimeInForce timeInForce)
      {
        this._timeInForce = timeInForce;
        return this;
      }

      public GetOrderPreviewResponseBuilder WithCommission(string commission)
      {
        this._commission = commission;
        return this;
      }

      public GetOrderPreviewResponseBuilder WithSlippage(string slippage)
      {
        this._slippage = slippage;
        return this;
      }

      public GetOrderPreviewResponseBuilder WithBestBid(string bestBid)
      {
        this._bestBid = bestBid;
        return this;
      }

      public GetOrderPreviewResponseBuilder WithBestAsk(string bestAsk)
      {
        this._bestAsk = bestAsk;
        return this;
      }

      public GetOrderPreviewResponseBuilder WithAverageFilledPrice(string averageFilledPrice)
      {
        this._averageFilledPrice = averageFilledPrice;
        return this;
      }

      public GetOrderPreviewResponseBuilder WithOrderTotal(string orderTotal)
      {
        this._orderTotal = orderTotal;
        return this;
      }

      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(_portfolioId))
        {
          throw new CoinbaseClientException("PortfolioId is required");
        }
      }

      public GetOrderPreviewResponse Build()
      {
        Validate();
        return new GetOrderPreviewResponse
        {
          PortfolioId = this._portfolioId,
          ProductId = this._productId,
          Side = this._side,
          Type = this._type,
          BaseQuantity = this._baseQuantity,
          QuoteValue = this._quoteValue,
          LimitPrice = this._limitPrice,
          StartTime = this._startTime,
          ExpiryTime = this._expiryTime,
          TimeInForce = this._timeInForce,
          Commission = this._commission,
          Slippage = this._slippage,
          BestBid = this._bestBid,
          BestAsk = this._bestAsk,
          AverageFilledPrice = this._averageFilledPrice,
          OrderTotal = this._orderTotal
        };
      }
    }
  }
}
