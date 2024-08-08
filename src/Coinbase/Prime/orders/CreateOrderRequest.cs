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
  using Coinbase.Prime.Common;
  public class CreateOrderRequest(string portfolioId)
  : BasePrimeRequest(portfolioId, null)
  {
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

    [JsonPropertyName("time_in_force")]
    public TimeInForce? TimeInForce { get; set; }

    [JsonPropertyName("start_time")]
    public string? StartTime { get; set; }

    [JsonPropertyName("expiry_time")]
    public string? ExpiryTime { get; set; }

    [JsonPropertyName("stp_id")]
    public string? StpId { get; set; }

    [JsonPropertyName("display_quote_size")]
    public string? DisplayQuoteSize { get; set; }

    [JsonPropertyName("display_base_size")]
    public string? DisplayBaseSize { get; set; }

    [JsonPropertyName("is_raise_exact")]
    public bool? IsRaiseExact { get; set; }

    [JsonPropertyName("historical_pov")]
    public string? HistoricalPov { get; set; }

    public class CreateOrderRequestBuilder
    {
      private string? _portfolioId;
      private string? _productId;
      private OrderSide? _side;
      private string? _clientOrderId;
      private OrderType? _type;
      private string? _baseQuantity;
      private string? _quoteValue;
      private string? _limitPrice;
      private string? _stopPrice;
      private TimeInForce? _timeInForce;
      private string? _startTime;
      private string? _expiryTime;
      private string? _stpId;
      private string? _displayQuoteSize;
      private string? _displayBaseSize;
      private bool? _isRaiseExact;
      private string? _historicalPov;

      public CreateOrderRequestBuilder WithPortfolioId(string portfolioId)
      {
        this._portfolioId = portfolioId;
        return this;
      }

      public CreateOrderRequestBuilder WithProductId(string? productId)
      {
        this._productId = productId;
        return this;
      }

      public CreateOrderRequestBuilder WithSide(OrderSide? side)
      {
        this._side = side;
        return this;
      }

      public CreateOrderRequestBuilder WithClientOrderId(string? clientOrderId)
      {
        this._clientOrderId = clientOrderId;
        return this;
      }

      public CreateOrderRequestBuilder WithType(OrderType? type)
      {
        this._type = type;
        return this;
      }

      public CreateOrderRequestBuilder WithBaseQuantity(string? baseQuantity)
      {
        this._baseQuantity = baseQuantity;
        return this;
      }

      public CreateOrderRequestBuilder WithQuoteValue(string? quoteValue)
      {
        this._quoteValue = quoteValue;
        return this;
      }

      public CreateOrderRequestBuilder WithLimitPrice(string? limitPrice)
      {
        this._limitPrice = limitPrice;
        return this;
      }

      public CreateOrderRequestBuilder WithStopPrice(string? stopPrice)
      {
        this._stopPrice = stopPrice;
        return this;
      }

      public CreateOrderRequestBuilder WithTimeInForce(TimeInForce? timeInForce)
      {
        this._timeInForce = timeInForce;
        return this;
      }

      public CreateOrderRequestBuilder WithStartTime(string? startTime)
      {
        this._startTime = startTime;
        return this;
      }

      public CreateOrderRequestBuilder WithExpiryTime(string? expiryTime)
      {
        this._expiryTime = expiryTime;
        return this;
      }

      public CreateOrderRequestBuilder WithStpId(string? stpId)
      {
        this._stpId = stpId;
        return this;
      }

      public CreateOrderRequestBuilder WithDisplayQuoteSize(string? displayQuoteSize)
      {
        this._displayQuoteSize = displayQuoteSize;
        return this;
      }

      public CreateOrderRequestBuilder WithDisplayBaseSize(string? displayBaseSize)
      {
        this._displayBaseSize = displayBaseSize;
        return this;
      }

      public CreateOrderRequestBuilder WithIsRaiseExact(bool? isRaiseExact)
      {
        this._isRaiseExact = isRaiseExact;
        return this;
      }

      public CreateOrderRequestBuilder WithHistoricalPov(string? historicalPov)
      {
        this._historicalPov = historicalPov;
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
      /// Builds the <see cref="CreateOrderRequest"/> object.
      /// </summary>
      /// <returns>The <see cref="CreateOrderRequest"/> object.</returns>
      /// <exception cref="CoinbaseClientException">Thrown when the required fields are not set.</exception>
      public CreateOrderRequest Build()
      {
        this.Validate();
        return new CreateOrderRequest(_portfolioId!)
        {
          ProductId = this._productId,
          Side = this._side,
          ClientOrderId = this._clientOrderId,
          Type = this._type,
          BaseQuantity = this._baseQuantity,
          QuoteValue = this._quoteValue,
          LimitPrice = this._limitPrice,
          StopPrice = this._stopPrice,
          TimeInForce = this._timeInForce,
          StartTime = this._startTime,
          ExpiryTime = this._expiryTime,
          StpId = this._stpId,
          DisplayQuoteSize = this._displayQuoteSize,
          DisplayBaseSize = this._displayBaseSize,
          IsRaiseExact = this._isRaiseExact,
          HistoricalPov = this._historicalPov
        };
      }
    }
  }
}
