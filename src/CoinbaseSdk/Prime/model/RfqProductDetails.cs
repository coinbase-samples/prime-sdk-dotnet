/*
 * Copyright 2025-present Coinbase Global, Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace CoinbaseSdk.Prime.Model
{
  using System.Text.Json.Serialization;

  public class RfqProductDetails
  {
    public bool? Tradable { get; set; }

    [JsonPropertyName("min_notional_size")]
    public string? MinNotionalSize { get; set; }

    [JsonPropertyName("max_notional_size")]
    public string? MaxNotionalSize { get; set; }

    [JsonPropertyName("min_base_size")]
    public string? MinBaseSize { get; set; }

    [JsonPropertyName("max_base_size")]
    public string? MaxBaseSize { get; set; }

    [JsonPropertyName("min_quote_size")]
    public string? MinQuoteSize { get; set; }

    [JsonPropertyName("max_quote_size")]
    public string? MaxQuoteSize { get; set; }

    public RfqProductDetails() { }

    public class RfqProductDetailsBuilder
    {
      private bool? _tradable;
      private string? _minNotionalSize;
      private string? _maxNotionalSize;
      private string? _minBaseSize;
      private string? _maxBaseSize;
      private string? _minQuoteSize;
      private string? _maxQuoteSize;

      public RfqProductDetailsBuilder WithTradable(bool tradable)
      {
        this._tradable = tradable;
        return this;
      }

      public RfqProductDetailsBuilder WithMinNotionalSize(string minNotionalSize)
      {
        this._minNotionalSize = minNotionalSize;
        return this;
      }

      public RfqProductDetailsBuilder WithMaxNotionalSize(string maxNotionalSize)
      {
        this._maxNotionalSize = maxNotionalSize;
        return this;
      }

      public RfqProductDetailsBuilder WithMinBaseSize(string minBaseSize)
      {
        this._minBaseSize = minBaseSize;
        return this;
      }

      public RfqProductDetailsBuilder WithMaxBaseSize(string maxBaseSize)
      {
        this._maxBaseSize = maxBaseSize;
        return this;
      }

      public RfqProductDetailsBuilder WithMinQuoteSize(string minQuoteSize)
      {
        this._minQuoteSize = minQuoteSize;
        return this;
      }

      public RfqProductDetailsBuilder WithMaxQuoteSize(string maxQuoteSize)
      {
        this._maxQuoteSize = maxQuoteSize;
        return this;
      }

      public RfqProductDetails Build()
      {
        return new RfqProductDetails
        {
          Tradable = this._tradable,
          MinNotionalSize = this._minNotionalSize,
          MaxNotionalSize = this._maxNotionalSize,
          MinBaseSize = this._minBaseSize,
          MaxBaseSize = this._maxBaseSize,
          MinQuoteSize = this._minQuoteSize,
          MaxQuoteSize = this._maxQuoteSize,
        };
      }
    }
  }
}