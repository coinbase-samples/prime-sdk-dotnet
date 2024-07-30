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
  public class CreateWithdrawalResponse
  {
    [JsonPropertyName("activity_id")]
    public string? ActivityId { get; set; }

    [JsonPropertyName("approval_url")]
    public string? ApprovalUrl { get; set; }

    public string? Symbol { get; set; }
    public string? Amount { get; set; }
    public string? Fee { get; set; }

    [JsonPropertyName("destination_type")]
    public DestinationType DestinationType { get; set; }

    [JsonPropertyName("source_type")]
    public string? SourceType { get; set; }

    [JsonPropertyName("blockchain_destination")]
    public BlockchainAddress? BlockchainDestination { get; set; }

    [JsonPropertyName("blockchain_source")]
    public BlockchainAddress? BlockchainSource { get; set; }

    [JsonPropertyName("transaction_id")]
    public string? TransactionId { get; set; }

    public CreateWithdrawalResponse() { }
  }
}
