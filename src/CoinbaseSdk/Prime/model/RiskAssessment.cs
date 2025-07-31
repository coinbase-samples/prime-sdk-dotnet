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

  /// <summary>
  /// Risk assessment details for transactions.
  /// </summary>
  public class RiskAssessment
  {
    /// <summary>
    /// Indicates if the transaction has been flagged for compliance concerns.
    /// </summary>
    [JsonPropertyName("compliance_risk_detected")]
    public bool? ComplianceRiskDetected { get; set; }

    /// <summary>
    /// Indicates if the transaction has been flagged for security concerns.
    /// </summary>
    [JsonPropertyName("security_risk_detected")]
    public bool? SecurityRiskDetected { get; set; }
  }
}