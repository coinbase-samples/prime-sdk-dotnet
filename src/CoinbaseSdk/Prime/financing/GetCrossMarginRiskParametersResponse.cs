/*
 * Copyright 2026-present Coinbase Global, Inc.
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

namespace CoinbaseSdk.Prime.Financing
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Prime.Model;

  /// <summary>
  /// Get Cross Margin Risk Parameters.
  /// </summary>
  public class GetCrossMarginRiskParametersResponse
  {
    [JsonPropertyName("risk_parameters")]
    public CrossMarginRiskParameters[] RiskParameters { get; set; } = [];

    [JsonPropertyName("offset_credit_matrix_long_short")]
    public TierPairRateEntry[] OffsetCreditMatrixLongShort { get; set; } = [];

    [JsonPropertyName("offset_credit_matrix_long_long")]
    public TierPairRateEntry[] OffsetCreditMatrixLongLong { get; set; } = [];

    [JsonPropertyName("offset_credit_matrix_short_short")]
    public TierPairRateEntry[] OffsetCreditMatrixShortShort { get; set; } = [];

    [JsonPropertyName("margin_period_of_risk")]
    public double? MarginPeriodOfRisk { get; set; }

    public GetCrossMarginRiskParametersResponse() { }
  }
}
