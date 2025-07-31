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

namespace CoinbaseSdk.Prime.Model
{
  using System.Text.Json.Serialization;

  public class OnchainTransactionDetails
  {
    /// <summary>
    /// The signed transaction data.
    /// </summary>
    [JsonPropertyName("signed_transaction")]
    public string? SignedTransaction { get; set; }

    [JsonPropertyName("risk_assessment")]
    public RiskAssessment? RiskAssessment { get; set; }

    /// <summary>
    /// The blockchain network chain ID. Will be empty for Solana transactions.
    /// </summary>
    [JsonPropertyName("chain_id")]
    public string? ChainId { get; set; }

    /// <summary>
    /// The transaction nonce. Only present for EVM-based blockchain transactions.
    /// </summary>
    [JsonPropertyName("nonce")]
    public string? Nonce { get; set; }

    /// <summary>
    /// The ID of the transaction that this transaction replaced.
    /// </summary>
    [JsonPropertyName("replaced_transaction_id")]
    public string? ReplacedTransactionId { get; set; }

    /// <summary>
    /// The destination address for the transaction.
    /// </summary>
    [JsonPropertyName("destination_address")]
    public string? DestinationAddress { get; set; }

    /// <summary>
    /// If set to true, the transaction will not be broadcast to the network.
    /// </summary>
    [JsonPropertyName("skip_broadcast")]
    public bool? SkipBroadcast { get; set; }

    /// <summary>
    /// Reason for transaction failure if applicable.
    /// </summary>
    [JsonPropertyName("failure_reason")]
    public string? FailureReason { get; set; }

    [JsonPropertyName("signing_status")]
    public SigningStatus? SigningStatus { get; set; }
  }
}