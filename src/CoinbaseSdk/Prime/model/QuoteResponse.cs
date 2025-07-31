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

  public class QuoteResponse
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

    public QuoteResponse() { }

    public class QuoteResponseBuilder
    {
      private string? _quoteId;
      private string? _expirationTime;
      private string? _bestPrice;
      private string? _orderTotal;
      private string? _priceInclusiveOfFees;

      public QuoteResponseBuilder WithQuoteId(string quoteId)
      {
        this._quoteId = quoteId;
        return this;
      }

      public QuoteResponseBuilder WithExpirationTime(string expirationTime)
      {
        this._expirationTime = expirationTime;
        return this;
      }

      public QuoteResponseBuilder WithBestPrice(string bestPrice)
      {
        this._bestPrice = bestPrice;
        return this;
      }

      public QuoteResponseBuilder WithOrderTotal(string orderTotal)
      {
        this._orderTotal = orderTotal;
        return this;
      }

      public QuoteResponseBuilder WithPriceInclusiveOfFees(string priceInclusiveOfFees)
      {
        this._priceInclusiveOfFees = priceInclusiveOfFees;
        return this;
      }

      public QuoteResponse Build()
      {
        return new QuoteResponse
        {
          QuoteId = this._quoteId,
          ExpirationTime = this._expirationTime,
          BestPrice = this._bestPrice,
          OrderTotal = this._orderTotal,
          PriceInclusiveOfFees = this._priceInclusiveOfFees,
        };
      }
    }
  }
}