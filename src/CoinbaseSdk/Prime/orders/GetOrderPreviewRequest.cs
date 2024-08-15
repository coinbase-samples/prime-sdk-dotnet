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
  using CoinbaseSdk.Prime.Model;

  public class GetOrderPreviewRequest(string portfolioId)
  : BasePrimeRequest(portfolioId, null)
  {
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

    [JsonPropertyName("stop_price")]
    public string? StopPrice { get; set; }

    [JsonPropertyName("time_in_force")]
    public TimeInForce? TimeInForce { get; set; }

    [JsonPropertyName("start_time")]
    public string? StartTime { get; set; }

    [JsonPropertyName("expiry_time")]
    public string? ExpiryTime { get; set; }

    [JsonPropertyName("is_raise_exact")]
    public bool? IsRaiseExact { get; set; }

    [JsonPropertyName("historical_pov")]
    public string? HistoricalPov { get; set; }

    public class GetOrderPreviewRequestBuilder
    {
      private string? _portfolioId;
      private string? _productId;
      private OrderSide? _side;
      private OrderType? _type;
      private string? _baseQuantity;
      private string? _quoteValue;
      private string? _limitPrice;
      private string? _stopPrice;
      private TimeInForce? _timeInForce;
      private string? _startTime;
      private string? _expiryTime;
      private bool? _isRaiseExact;
      private string? _historicalPov;

      public GetOrderPreviewRequestBuilder WithPortfolioId(string portfolioId)
      {
        this._portfolioId = portfolioId;
        return this;
      }

      public GetOrderPreviewRequestBuilder WithProductId(string productId)
      {
        this._productId = productId;
        return this;
      }

      public GetOrderPreviewRequestBuilder WithSide(OrderSide side)
      {
        this._side = side;
        return this;
      }

      public GetOrderPreviewRequestBuilder WithType(OrderType type)
      {
        this._type = type;
        return this;
      }

      public GetOrderPreviewRequestBuilder WithBaseQuantity(string baseQuantity)
      {
        this._baseQuantity = baseQuantity;
        return this;
      }

      public GetOrderPreviewRequestBuilder WithQuoteValue(string quoteValue)
      {
        this._quoteValue = quoteValue;
        return this;
      }

      public GetOrderPreviewRequestBuilder WithLimitPrice(string limitPrice)
      {
        this._limitPrice = limitPrice;
        return this;
      }

      public GetOrderPreviewRequestBuilder WithStopPrice(string stopPrice)
      {
        this._stopPrice = stopPrice;
        return this;
      }

      public GetOrderPreviewRequestBuilder WithTimeInForce(TimeInForce timeInForce)
      {
        this._timeInForce = timeInForce;
        return this;
      }

      public GetOrderPreviewRequestBuilder WithStartTime(string startTime)
      {
        this._startTime = startTime;
        return this;
      }

      public GetOrderPreviewRequestBuilder WithExpiryTime(string expiryTime)
      {
        this._expiryTime = expiryTime;
        return this;
      }

      public GetOrderPreviewRequestBuilder WithIsRaiseExact(bool isRaiseExact)
      {
        this._isRaiseExact = isRaiseExact;
        return this;
      }

      public GetOrderPreviewRequestBuilder WithHistoricalPov(string historicalPov)
      {
        this._historicalPov = historicalPov;
        return this;
      }

      /// <summary>
      /// Validates the request.
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
      /// Builds the <see cref="GetOrderPreviewRequest"/>.
      /// </summary>
      /// <returns>The <see cref="GetOrderPreviewRequest"/> object.</returns>
      /// <exception cref="CoinbaseClientException">Thrown when the required fields are not set.</exception>
      public GetOrderPreviewRequest Build()
      {
        Validate();
        return new GetOrderPreviewRequest(this._portfolioId!)
        {
          ProductId = this._productId,
          Side = this._side,
          Type = this._type,
          BaseQuantity = this._baseQuantity,
          QuoteValue = this._quoteValue,
          LimitPrice = this._limitPrice,
          StopPrice = this._stopPrice,
          TimeInForce = this._timeInForce,
          StartTime = this._startTime,
          ExpiryTime = this._expiryTime,
          IsRaiseExact = this._isRaiseExact,
          HistoricalPov = this._historicalPov
        };
      }
    }
  }
}
