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
  using CoinbaseSdk.Prime.Model.Enums;

  /// <summary>
  /// Request for creating a new trading order.
  /// </summary>
  public class CreateOrderRequest(string portfolioId)
  {
    /// <summary>
    /// The ID of the portfolio that will own the order.
    /// </summary>
    [JsonIgnore]
    public string PortfolioId { get; set; } = portfolioId;

    /// <summary>
    /// The ID of the product being traded by the order.
    /// </summary>
    [JsonPropertyName("product_id")]
    public string? ProductId { get; set; }

    /// <summary>
    /// The side of the order (BUY or SELL).
    /// </summary>
    [JsonPropertyName("side")]
    public OrderSide? Side { get; set; }

    /// <summary>
    /// A client-generated order ID used for reference purposes (note: order will be rejected if this ID is not unique among all currently active orders).
    /// </summary>
    [JsonPropertyName("client_order_id")]
    public string? ClientOrderId { get; set; }

    /// <summary>
    /// Strategy (execution algorithm) for the order.
    /// </summary>
    [JsonPropertyName("type")]
    public OrderType? Type { get; set; }

    /// <summary>
    /// Order size in base asset units (either base_quantity or quote_value is required).
    /// </summary>
    [JsonPropertyName("base_quantity")]
    public string? BaseQuantity { get; set; }

    /// <summary>
    /// Order size in quote asset units, i.e. the amount the user wants to spend (when buying) or receive (when selling); the quantity in base units will be determined based on the market liquidity and indicated quote_value. Either base_quantity or quote_value is required.
    /// </summary>
    [JsonPropertyName("quote_value")]
    public string? QuoteValue { get; set; }

    /// <summary>
    /// The limit price (required for TWAP, VWAP, LIMIT and STOP_LIMIT orders).
    /// </summary>
    [JsonPropertyName("limit_price")]
    public string? LimitPrice { get; set; }

    /// <summary>
    /// Specifies the stop price at which the order activates. The order is activated if the last trade price on Coinbase Exchange crosses the stop price specified on the order.
    /// </summary>
    [JsonPropertyName("stop_price")]
    public string? StopPrice { get; set; }

    /// <summary>
    /// Indicates the order time validity.
    /// </summary>
    [JsonPropertyName("time_in_force")]
    public TimeInForceType? TimeInForce { get; set; }

    /// <summary>
    /// The start time of the order in UTC (only applies to TWAP, VWAP orders.).
    /// </summary>
    [JsonPropertyName("start_time")]
    public string? StartTime { get; set; }

    /// <summary>
    /// The expiry time of the order in UTC (applies to TWAP, VWAP, LIMIT, and STOP_LIMIT orders with time_in_force set to GTD).
    /// </summary>
    [JsonPropertyName("expiry_time")]
    public string? ExpiryTime { get; set; }

    /// <summary>
    /// Self-trade prevention ID for the order.
    /// </summary>
    [JsonPropertyName("stp_id")]
    public string? StpId { get; set; }

    /// <summary>
    /// The maximum order size that will show up on venue order books (in quote currency).
    /// </summary>
    [JsonPropertyName("display_quote_size")]
    public string? DisplayQuoteSize { get; set; }

    /// <summary>
    /// The maximum order size that will show up on venue order books (in base currency).
    /// </summary>
    [JsonPropertyName("display_base_size")]
    public string? DisplayBaseSize { get; set; }

    /// <summary>
    /// Indicates if this was a raise exact order (size inclusive of fees for sell orders in quote).
    /// </summary>
    [JsonPropertyName("is_raise_exact")]
    public bool? IsRaiseExact { get; set; }

    /// <summary>
    /// The estimated participation rate for a TWAP/VWAP order. This field can be specified instead of expiry time, and will be used to compute the expiry time of the order based on historical participation rate.
    /// </summary>
    [JsonPropertyName("historical_pov")]
    public string? HistoricalPov { get; set; }

    /// <summary>
    /// The currency in which the settlement will be made.
    /// </summary>
    [JsonPropertyName("settl_currency")]
    public string? SettlCurrency { get; set; }

    /// <summary>
    /// Post-only flag - when true, the order will only be posted to the order book and not immediately matched. Only applicable to LIMIT orders with GTC or GTD time in force.
    /// </summary>
    [JsonPropertyName("post_only")]
    public bool? PostOnly { get; set; }

    /// <summary>
    /// Builder class for creating CreateOrderRequest instances.
    /// </summary>
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
      private TimeInForceType? _timeInForce;
      private string? _startTime;
      private string? _expiryTime;
      private string? _stpId;
      private string? _displayQuoteSize;
      private string? _displayBaseSize;
      private bool? _isRaiseExact;
      private string? _historicalPov;
      private string? _settlCurrency;
      private bool? _postOnly;

      public CreateOrderRequestBuilder WithPortfolioId(string portfolioId)
      {
        _portfolioId = portfolioId;
        return this;
      }

      public CreateOrderRequestBuilder WithProductId(string productId)
      {
        _productId = productId;
        return this;
      }

      public CreateOrderRequestBuilder WithSide(OrderSide side)
      {
        _side = side;
        return this;
      }

      public CreateOrderRequestBuilder WithClientOrderId(string clientOrderId)
      {
        _clientOrderId = clientOrderId;
        return this;
      }

      public CreateOrderRequestBuilder WithType(OrderType type)
      {
        _type = type;
        return this;
      }

      public CreateOrderRequestBuilder WithBaseQuantity(string baseQuantity)
      {
        _baseQuantity = baseQuantity;
        return this;
      }

      public CreateOrderRequestBuilder WithQuoteValue(string quoteValue)
      {
        _quoteValue = quoteValue;
        return this;
      }

      public CreateOrderRequestBuilder WithLimitPrice(string limitPrice)
      {
        _limitPrice = limitPrice;
        return this;
      }

      public CreateOrderRequestBuilder WithStopPrice(string stopPrice)
      {
        _stopPrice = stopPrice;
        return this;
      }

      public CreateOrderRequestBuilder WithTimeInForce(TimeInForceType timeInForce)
      {
        _timeInForce = timeInForce;
        return this;
      }

      public CreateOrderRequestBuilder WithStartTime(string startTime)
      {
        _startTime = startTime;
        return this;
      }

      public CreateOrderRequestBuilder WithExpiryTime(string expiryTime)
      {
        _expiryTime = expiryTime;
        return this;
      }

      public CreateOrderRequestBuilder WithStpId(string stpId)
      {
        _stpId = stpId;
        return this;
      }

      public CreateOrderRequestBuilder WithDisplayQuoteSize(string displayQuoteSize)
      {
        _displayQuoteSize = displayQuoteSize;
        return this;
      }

      public CreateOrderRequestBuilder WithDisplayBaseSize(string displayBaseSize)
      {
        _displayBaseSize = displayBaseSize;
        return this;
      }

      public CreateOrderRequestBuilder WithIsRaiseExact(bool isRaiseExact)
      {
        _isRaiseExact = isRaiseExact;
        return this;
      }

      public CreateOrderRequestBuilder WithHistoricalPov(string historicalPov)
      {
        _historicalPov = historicalPov;
        return this;
      }

      public CreateOrderRequestBuilder WithSettlCurrency(string settlCurrency)
      {
        _settlCurrency = settlCurrency;
        return this;
      }

      public CreateOrderRequestBuilder WithPostOnly(bool postOnly)
      {
        _postOnly = postOnly;
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
        Validate();
        return new CreateOrderRequest(_portfolioId!)
        {
          ProductId = _productId,
          Side = _side,
          ClientOrderId = _clientOrderId,
          Type = _type,
          BaseQuantity = _baseQuantity,
          QuoteValue = _quoteValue,
          LimitPrice = _limitPrice,
          StopPrice = _stopPrice,
          TimeInForce = _timeInForce,
          StartTime = _startTime,
          ExpiryTime = _expiryTime,
          StpId = _stpId,
          DisplayQuoteSize = _displayQuoteSize,
          DisplayBaseSize = _displayBaseSize,
          IsRaiseExact = _isRaiseExact,
          HistoricalPov = _historicalPov,
          SettlCurrency = _settlCurrency,
          PostOnly = _postOnly,
        };
      }
    }
  }
}
