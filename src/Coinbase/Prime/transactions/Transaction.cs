/*
 * Copyright 2024-present Coinbase Global, Inc.
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

namespace Coinbase.Prime.Transactions
{
  using System.Text.Json.Serialization;
  public class Transaction
  {
    public string? Id { get; set; }

    [JsonPropertyName("wallet_id")]
    public string? WalletId { get; set; }

    [JsonPropertyName("portfolio_id")]
    public string? PortfolioId { get; set; }

    public TransactionType Type { get; set; }

    public TransactionStatus Status { get; set; }

    public string? Symbol { get; set; }

    [JsonPropertyName("created_at")]
    public string? CreatedAt { get; set; }

    [JsonPropertyName("completed_at")]
    public string? CompletedAt { get; set; }

    public string? Amount { get; set; }

    [JsonPropertyName("transfer_from")]
    public TransferLocation? TransferFrom { get; set; }

    [JsonPropertyName("transfer_to")]
    public TransferLocation? TransferTo { get; set; }

    [JsonPropertyName("network_fees")]
    public string? NetworkFees { get; set; }

    public string? Fees { get; set; }

    [JsonPropertyName("fee_symbol")]
    public string? FeeSymbol { get; set; }

    [JsonPropertyName("blockchain_ids")]
    public string[] BlockchainIds { get; set; } = [];

    [JsonPropertyName("transaction_id")]
    public string? TransactionId { get; set; }

    [JsonPropertyName("destination_symbol")]
    public string? DestinationSymbol { get; set; }

    [JsonPropertyName("estimated_network_fees")]
    public EstimatedNetworkFees? EstimatedNetworkFees { get; set; }

    public string? Network { get; set; }

    [JsonPropertyName("estimated_asset_changes")]
    public AssetChange[] EstimatedAssetChanges { get; set; } = [];

    public TransactionMetadata? Metadata { get; set; }

    public Transaction() { }
  }
}
