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
  using System.Text.Json.Serialization;

  public class FcmRiskLimit
  {
    [JsonPropertyName("product_id")]
    public string? ProductId { get; set; }

    [JsonPropertyName("risk_limit")]
    public string? RiskLimitValue { get; set; }

    [JsonPropertyName("current_exposure")]
    public string? CurrentExposure { get; set; }

    [JsonPropertyName("remaining_capacity")]
    public string? RemainingCapacity { get; set; }

    [JsonPropertyName("limit_type")]
    public string? LimitType { get; set; }
  }
}