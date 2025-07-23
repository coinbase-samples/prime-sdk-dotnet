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

namespace CoinbaseSdk.Prime.Futures
{
  using System.Text.Json.Serialization;

  public class FcmMarginCallDetails
  {
    [JsonPropertyName("entity_id")]
    public string? EntityId { get; set; }

    [JsonPropertyName("margin_call_amount")]
    public string? MarginCallAmount { get; set; }

    [JsonPropertyName("margin_call_currency")]
    public string? MarginCallCurrency { get; set; }

    [JsonPropertyName("margin_call_date")]
    public string? MarginCallDate { get; set; }

    [JsonPropertyName("margin_requirement")]
    public string? MarginRequirement { get; set; }

    [JsonPropertyName("excess_margin")]
    public string? ExcessMargin { get; set; }
  }
}