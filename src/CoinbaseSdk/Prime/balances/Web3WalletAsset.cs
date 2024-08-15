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

namespace CoinbaseSdk.Prime.Balances
{
  using System.Text.Json.Serialization;
  public class Web3WalletAsset
  {
    public string? Network { get; set; }

    [JsonPropertyName("contract_address")]
    public string? ContractAddress { get; set; }

    public string? Symbol { get; set; }

    [JsonPropertyName("token_id")]
    public string? TokenId { get; set; }

    public Web3WalletAsset() { }

    public Web3WalletAsset(string network, string contractAddress, string symbol, string tokenId)
    {
      Network = network;
      ContractAddress = contractAddress;
      Symbol = symbol;
      TokenId = tokenId;
    }

    public class Web3WalletAssetBuilder
    {
      private string? _network;
      private string? _contractAddress;
      private string? _symbol;
      private string? _tokenId;

      public Web3WalletAssetBuilder WithNetwork(string? network)
      {
        this._network = network;
        return this;
      }

      public Web3WalletAssetBuilder WithContractAddress(string? contractAddress)
      {
        this._contractAddress = contractAddress;
        return this;
      }

      public Web3WalletAssetBuilder WithSymbol(string? symbol)
      {
        this._symbol = symbol;
        return this;
      }

      public Web3WalletAssetBuilder WithTokenId(string? tokenId)
      {
        this._tokenId = tokenId;
        return this;
      }

      public Web3WalletAsset Build()
      {
        return new Web3WalletAsset
        {
          Network = this._network,
          ContractAddress = this._contractAddress,
          Symbol = this._symbol,
          TokenId = this._tokenId
        };
      }
    }
  }
}
