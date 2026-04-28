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

  /// <summary>
  /// Update Funding Settings (Beta).
  /// </summary>
  public class UpdateFundingSettingsResponse
  {
    [JsonPropertyName("activity_id")]
    public string? ActivityId { get; set; }

    [JsonPropertyName("activity_type")]
    public string? ActivityType { get; set; }

    [JsonPropertyName("num_approvals_remaining")]
    public int? NumApprovalsRemaining { get; set; }

    public UpdateFundingSettingsResponse() { }
  }
}
