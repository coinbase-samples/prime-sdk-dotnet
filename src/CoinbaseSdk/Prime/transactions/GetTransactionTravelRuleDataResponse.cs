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

namespace CoinbaseSdk.Prime.Transactions
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Prime.Model;

  public class GetTransactionTravelRuleDataResponse
  {
    [JsonPropertyName("fulfilled")]
    public bool? Fulfilled { get; set; }

    [JsonPropertyName("is_self")]
    public bool? IsSelf { get; set; }

    [JsonPropertyName("originator")]
    public TravelRuleParty? Originator { get; set; }

    [JsonPropertyName("beneficiary")]
    public TravelRuleParty? Beneficiary { get; set; }

    [JsonPropertyName("amount")]
    public string? Amount { get; set; }

    [JsonPropertyName("amount_currency")]
    public string? AmountCurrency { get; set; }

    [JsonPropertyName("fiat_amount")]
    public string? FiatAmount { get; set; }

    [JsonPropertyName("fiat_amount_currency")]
    public string? FiatAmountCurrency { get; set; }

    [JsonPropertyName("blockchain_network")]
    public string? BlockchainNetwork { get; set; }

    public GetTransactionTravelRuleDataResponse() { }
  }
}
