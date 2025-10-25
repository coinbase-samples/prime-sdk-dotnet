/*
 * Copyright 2025-present Coinbase Global, Inc.
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

  public class EditOrderRequest(string portfolioId, string orderId)
  {
    [JsonIgnore]
    public string PortfolioId { get; set; } = portfolioId;

    [JsonIgnore]
    public string OrderId { get; set; } = orderId;

    [JsonPropertyName("product_id")]
    public string? ProductId { get; set; }

    [JsonPropertyName("orig_client_order_id")]
    public string? OrigClientOrderId { get; set; }

    [JsonPropertyName("client_order_id")]
    public string? ClientOrderId { get; set; }

    [JsonPropertyName("base_quantity")]
    public string? BaseQuantity { get; set; }

    [JsonPropertyName("quote_value")]
    public string? QuoteValue { get; set; }

    [JsonPropertyName("limit_price")]
    public string? LimitPrice { get; set; }

    [JsonPropertyName("expiry_time")]
    public string? ExpiryTime { get; set; }

    [JsonPropertyName("display_quote_size")]
    public string? DisplayQuoteSize { get; set; }

    [JsonPropertyName("display_base_size")]
    public string? DisplayBaseSize { get; set; }

    [JsonPropertyName("stop_price")]
    public string? StopPrice { get; set; }

    public class EditOrderRequestBuilder
    {
      private string? _portfolioId;
      private string? _orderId;
      private string? _productId;
      private string? _origClientOrderId;
      private string? _clientOrderId;
      private string? _baseQuantity;
      private string? _quoteValue;
      private string? _limitPrice;
      private string? _expiryTime;
      private string? _displayQuoteSize;
      private string? _displayBaseSize;
      private string? _stopPrice;

      public EditOrderRequestBuilder WithPortfolioId(string portfolioId)
      {
        _portfolioId = portfolioId;
        return this;
      }

      public EditOrderRequestBuilder WithOrderId(string orderId)
      {
        _orderId = orderId;
        return this;
      }

      public EditOrderRequestBuilder WithProductId(string? productId)
      {
        _productId = productId;
        return this;
      }

      public EditOrderRequestBuilder WithOrigClientOrderId(string? origClientOrderId)
      {
        _origClientOrderId = origClientOrderId;
        return this;
      }

      public EditOrderRequestBuilder WithClientOrderId(string? clientOrderId)
      {
        _clientOrderId = clientOrderId;
        return this;
      }

      public EditOrderRequestBuilder WithBaseQuantity(string? baseQuantity)
      {
        _baseQuantity = baseQuantity;
        return this;
      }

      public EditOrderRequestBuilder WithQuoteValue(string? quoteValue)
      {
        _quoteValue = quoteValue;
        return this;
      }

      public EditOrderRequestBuilder WithLimitPrice(string? limitPrice)
      {
        _limitPrice = limitPrice;
        return this;
      }

      public EditOrderRequestBuilder WithExpiryTime(string? expiryTime)
      {
        _expiryTime = expiryTime;
        return this;
      }

      public EditOrderRequestBuilder WithDisplayQuoteSize(string? displayQuoteSize)
      {
        _displayQuoteSize = displayQuoteSize;
        return this;
      }

      public EditOrderRequestBuilder WithDisplayBaseSize(string? displayBaseSize)
      {
        _displayBaseSize = displayBaseSize;
        return this;
      }

      public EditOrderRequestBuilder WithStopPrice(string? stopPrice)
      {
        _stopPrice = stopPrice;
        return this;
      }

      /// <summary>
      /// Validates the builder.
      /// </summary>
      /// <exception cref="CoinbaseClientException">Thrown when required fields are missing.</exception>
      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(_portfolioId))
        {
          throw new CoinbaseClientException("PortfolioId is required");
        }

        if (string.IsNullOrWhiteSpace(_orderId))
        {
          throw new CoinbaseClientException("OrderId is required");
        }

        if (string.IsNullOrWhiteSpace(_origClientOrderId))
        {
          throw new CoinbaseClientException("OrigClientOrderId is required");
        }

        if (string.IsNullOrWhiteSpace(_clientOrderId))
        {
          throw new CoinbaseClientException("ClientOrderId is required");
        }
      }

      /// <summary>
      /// Builds the <see cref="EditOrderRequest"/> object.
      /// </summary>
      /// <returns>The <see cref="EditOrderRequest"/> object.</returns>
      /// <exception cref="CoinbaseClientException">Thrown when the required fields are not set.</exception>
      public EditOrderRequest Build()
      {
        Validate();
        return new EditOrderRequest(_portfolioId!, _orderId!)
        {
          ProductId = _productId,
          OrigClientOrderId = _origClientOrderId,
          ClientOrderId = _clientOrderId,
          BaseQuantity = _baseQuantity,
          QuoteValue = _quoteValue,
          LimitPrice = _limitPrice,
          ExpiryTime = _expiryTime,
          DisplayQuoteSize = _displayQuoteSize,
          DisplayBaseSize = _displayBaseSize,
          StopPrice = _stopPrice,
        };
      }
    }
  }
}
