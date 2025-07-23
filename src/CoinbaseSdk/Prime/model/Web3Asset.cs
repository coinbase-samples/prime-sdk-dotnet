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

  public class Web3Asset
  {
    /// <summary>
    /// Network this asset is on (ie "ethereum-mainnet").
    /// </summary>
    [JsonPropertyName("network")]
    public string? Network { get; set; }

    /// <summary>
    /// Contract Address of this asset (empty for native assets).
    /// </summary>
    [JsonPropertyName("contract_address")]
    public string? ContractAddress { get; set; }

    /// <summary>
    /// Symbol of this asset.
    /// </summary>
    [JsonPropertyName("symbol")]
    public string? Symbol { get; set; }

    /// <summary>
    /// Token ID of this asset (empty for non NFT assets).
    /// </summary>
    [JsonPropertyName("token_id")]
    public string? TokenId { get; set; }

    /// <summary>
    /// Name of this asset, either the name of the crypto token or the NFT collection name.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }
  }
}