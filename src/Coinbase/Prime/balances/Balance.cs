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
  using Newtonsoft.Json;
  public class Balance
  {
    public string? Symbol { get; set; }
    public string? Amount { get; set; }
    public string? Holds { get; set; }

    [JsonProperty("bonded_amount")]
    public string? BondedAmount { get; set; }

    [JsonProperty("reserved_amount")]
    public string? ReservedAmount { get; set; }

    [JsonProperty("unbonding_amount")]
    public string? UnbondingAmount { get; set; }

    [JsonProperty("unvested_amount")]
    public string? UnvestedAmount { get; set; }

    [JsonProperty("pending_rewards_amount")]
    public string? PendingRewardsAmount { get; set; }

    [JsonProperty("past_rewards_amount")]
    public string? PastRewardsAmount { get; set; }

    [JsonProperty("bondable_amount")]
    public string? BondableAmount { get; set; }

    [JsonProperty("withdrawable_amount")]
    public string? WithdrawableAmount { get; set; }

    [JsonProperty("fiat_amount")]
    public string? FiatAmount { get; set; }

    public Balance() { }
  }
}
