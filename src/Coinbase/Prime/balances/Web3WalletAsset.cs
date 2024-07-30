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

namespace Coinbase.Prime.Balances
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
  }
}
