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

namespace Coinbase.Prime.Balances
{
  using System.Text.Json.Serialization;
  public class Balance
  {
    public string? Symbol { get; set; }
    public string? Amount { get; set; }
    public string? Holds { get; set; }

    [JsonPropertyName("bonded_amount")]
    public string? BondedAmount { get; set; }

    [JsonPropertyName("reserved_amount")]
    public string? ReservedAmount { get; set; }

    [JsonPropertyName("unbonding_amount")]
    public string? UnbondingAmount { get; set; }

    [JsonPropertyName("unvested_amount")]
    public string? UnvestedAmount { get; set; }

    [JsonPropertyName("pending_rewards_amount")]
    public string? PendingRewardsAmount { get; set; }

    [JsonPropertyName("past_rewards_amount")]
    public string? PastRewardsAmount { get; set; }

    [JsonPropertyName("bondable_amount")]
    public string? BondableAmount { get; set; }

    [JsonPropertyName("withdrawable_amount")]
    public string? WithdrawableAmount { get; set; }

    [JsonPropertyName("fiat_amount")]
    public string? FiatAmount { get; set; }

    public Balance() { }
  }
}
