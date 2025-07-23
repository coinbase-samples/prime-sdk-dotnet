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

namespace CoinbaseSdk.Prime.Model
{
  using System.Collections.Generic;
  using System.Text.Json.Serialization;

  public class Asset
  {
    public string? Name { get; set; }
    public string? Symbol { get; set; }

    [JsonPropertyName("decimal_precision")]
    public string? DecimalPrecision { get; set; }

    [JsonPropertyName("trading_supported")]
    public bool? TradingSupported { get; set; }

    [JsonPropertyName("explorer_url")]
    public string? ExplorerUrl { get; set; }

    [JsonPropertyName("networks")]
    public List<NetworkInfo>? Networks { get; set; }

    public Asset() { }

    public class AssetBuilder
    {
      private string? _name;
      private string? _symbol;
      private string? _decimalPrecision;
      private bool? _tradingSupported;
      private string? _explorerUrl;
      private List<NetworkInfo>? _networks;

      public AssetBuilder WithName(string? name)
      {
        this._name = name;
        return this;
      }

      public AssetBuilder WithSymbol(string? symbol)
      {
        this._symbol = symbol;
        return this;
      }

      public AssetBuilder WithDecimalPrecision(string? decimalPrecision)
      {
        this._decimalPrecision = decimalPrecision;
        return this;
      }

      public AssetBuilder WithTradingSupported(bool? tradingSupported)
      {
        this._tradingSupported = tradingSupported;
        return this;
      }

      public AssetBuilder WithExplorerUrl(string? explorerUrl)
      {
        this._explorerUrl = explorerUrl;
        return this;
      }

      public AssetBuilder WithNetworks(List<NetworkInfo>? networks)
      {
        this._networks = networks;
        return this;
      }

      public Asset Build()
      {
        return new Asset
        {
          Name = this._name,
          Symbol = this._symbol,
          DecimalPrecision = this._decimalPrecision,
          TradingSupported = this._tradingSupported,
          ExplorerUrl = this._explorerUrl,
          Networks = this._networks
        };
      }
    }
  }
}