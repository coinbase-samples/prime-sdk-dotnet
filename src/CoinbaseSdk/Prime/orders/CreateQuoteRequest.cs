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
  using CoinbaseSdk.Prime.Model.Enums;

  public class CreateQuoteRequest(string portfolioId)
  {
    [JsonIgnore]
    public string PortfolioId { get; set; } = portfolioId;

    [JsonPropertyName("product_id")]
    public string? ProductId { get; set; }

    public OrderSide? Side { get; set; }

    [JsonPropertyName("client_quote_id")]
    public string? ClientQuoteId { get; set; }

    [JsonPropertyName("base_quantity")]
    public string? BaseQuantity { get; set; }

    [JsonPropertyName("quote_value")]
    public string? QuoteValue { get; set; }

    [JsonPropertyName("limit_price")]
    public string? LimitPrice { get; set; }

    [JsonPropertyName("settl_currency")]
    public string? SettlCurrency { get; set; }

    public class CreateQuoteRequestBuilder(string portfolioId)
    {
      private readonly string _portfolioId = portfolioId;
      private string? _productId;
      private OrderSide? _side;
      private string? _clientQuoteId;
      private string? _baseQuantity;
      private string? _quoteValue;
      private string? _limitPrice;
      private string? _settlCurrency;

      public CreateQuoteRequestBuilder WithProductId(string productId)
      {
        _productId = productId;
        return this;
      }

      public CreateQuoteRequestBuilder WithSide(OrderSide side)
      {
        _side = side;
        return this;
      }

      public CreateQuoteRequestBuilder WithClientQuoteId(string clientQuoteId)
      {
        _clientQuoteId = clientQuoteId;
        return this;
      }

      public CreateQuoteRequestBuilder WithBaseQuantity(string baseQuantity)
      {
        _baseQuantity = baseQuantity;
        return this;
      }

      public CreateQuoteRequestBuilder WithQuoteValue(string quoteValue)
      {
        _quoteValue = quoteValue;
        return this;
      }

      public CreateQuoteRequestBuilder WithLimitPrice(string limitPrice)
      {
        _limitPrice = limitPrice;
        return this;
      }

      public CreateQuoteRequestBuilder WithSettlCurrency(string settlCurrency)
      {
        _settlCurrency = settlCurrency;
        return this;
      }

      public CreateQuoteRequest Build()
      {
        return new CreateQuoteRequest(_portfolioId)
        {
          ProductId = _productId,
          Side = _side,
          ClientQuoteId = _clientQuoteId,
          BaseQuantity = _baseQuantity,
          QuoteValue = _quoteValue,
          LimitPrice = _limitPrice,
          SettlCurrency = _settlCurrency,
        };
      }
    }
  }
}
