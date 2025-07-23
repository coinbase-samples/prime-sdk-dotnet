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

  public class PmAssetInfo
  {
    [JsonPropertyName("symbol")]
    public string? Symbol { get; set; }

    [JsonPropertyName("amount")]
    public string? Amount { get; set; }

    [JsonPropertyName("price")]
    public string? Price { get; set; }

    public PmAssetInfo() { }

    public class PmAssetInfoBuilder
    {
      private string? _symbol;
      private string? _amount;
      private string? _price;

      public PmAssetInfoBuilder WithSymbol(string? symbol)
      {
        this._symbol = symbol;
        return this;
      }

      public PmAssetInfoBuilder WithAmount(string? amount)
      {
        this._amount = amount;
        return this;
      }

      public PmAssetInfoBuilder WithPrice(string? price)
      {
        this._price = price;
        return this;
      }

      public PmAssetInfo Build()
      {
        return new PmAssetInfo
        {
          Symbol = this._symbol,
          Amount = this._amount,
          Price = this._price
        };
      }
    }
  }
}