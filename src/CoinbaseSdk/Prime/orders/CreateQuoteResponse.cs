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

  public class CreateQuoteResponse
  {
    [JsonPropertyName("quote_id")]
    public string? QuoteId { get; set; }

    [JsonPropertyName("expiration_time")]
    public string? ExpirationTime { get; set; }

    [JsonPropertyName("best_price")]
    public string? BestPrice { get; set; }

    [JsonPropertyName("order_total")]
    public string? OrderTotal { get; set; }

    [JsonPropertyName("price_inclusive_of_fees")]
    public string? PriceInclusiveOfFees { get; set; }

    public CreateQuoteResponse() { }

    public class CreateQuoteResponseBuilder
    {
      private string? _quoteId;
      private string? _expirationTime;
      private string? _bestPrice;
      private string? _orderTotal;
      private string? _priceInclusiveOfFees;
      public CreateQuoteResponseBuilder() { }

      public CreateQuoteResponseBuilder WithQuoteId(string quoteId)
      {
        _quoteId = quoteId;
        return this;
      }

      public CreateQuoteResponseBuilder WithExpirationTime(string expirationTime)
      {
        _expirationTime = expirationTime;
        return this;
      }

      public CreateQuoteResponseBuilder WithBestPrice(string bestPrice)
      {
        _bestPrice = bestPrice;
        return this;
      }

      public CreateQuoteResponseBuilder WithOrderTotal(string orderTotal)
      {
        _orderTotal = orderTotal;
        return this;
      }

      public CreateQuoteResponseBuilder WithPriceInclusiveOfFees(string? priceInclusiveOfFees)
      {
        _priceInclusiveOfFees = priceInclusiveOfFees;
        return this;
      }

      public CreateQuoteResponse Build()
      {
        return new CreateQuoteResponse
        {
          QuoteId = _quoteId,
          ExpirationTime = _expirationTime,
          BestPrice = _bestPrice,
          OrderTotal = _orderTotal,
          PriceInclusiveOfFees = _priceInclusiveOfFees
        };
      }
    }
  }
}