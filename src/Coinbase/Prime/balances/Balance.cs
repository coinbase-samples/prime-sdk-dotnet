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

    public class BalanceBuilder
    {
      private string? _symbol;
      private string? _amount;
      private string? _holds;
      private string? _bondedAmount;
      private string? _reservedAmount;
      private string? _unbondingAmount;
      private string? _unvestedAmount;
      private string? _pendingRewardsAmount;
      private string? _pastRewardsAmount;
      private string? _bondableAmount;
      private string? _withdrawableAmount;
      private string? _fiatAmount;

      public BalanceBuilder WithSymbol(string? symbol)
      {
        this._symbol = symbol;
        return this;
      }

      public BalanceBuilder WithAmount(string? amount)
      {
        this._amount = amount;
        return this;
      }

      public BalanceBuilder WithHolds(string? holds)
      {
        this._holds = holds;
        return this;
      }

      public BalanceBuilder WithBondedAmount(string? bondedAmount)
      {
        this._bondedAmount = bondedAmount;
        return this;
      }

      public BalanceBuilder WithReservedAmount(string? reservedAmount)
      {
        this._reservedAmount = reservedAmount;
        return this;
      }

      public BalanceBuilder WithUnbondingAmount(string? unbondingAmount)
      {
        this._unbondingAmount = unbondingAmount;
        return this;
      }

      public BalanceBuilder WithUnvestedAmount(string? unvestedAmount)
      {
        this._unvestedAmount = unvestedAmount;
        return this;
      }

      public BalanceBuilder WithPendingRewardsAmount(string? pendingRewardsAmount)
      {
        this._pendingRewardsAmount = pendingRewardsAmount;
        return this;
      }

      public BalanceBuilder WithPastRewardsAmount(string? pastRewardsAmount)
      {
        this._pastRewardsAmount = pastRewardsAmount;
        return this;
      }

      public BalanceBuilder WithBondableAmount(string? bondableAmount)
      {
        this._bondableAmount = bondableAmount;
        return this;
      }

      public BalanceBuilder WithWithdrawableAmount(string? withdrawableAmount)
      {
        this._withdrawableAmount = withdrawableAmount;
        return this;
      }

      public BalanceBuilder WithFiatAmount(string? fiatAmount)
      {
        this._fiatAmount = fiatAmount;
        return this;
      }

      public Balance Build()
      {
        return new Balance
        {
          Symbol = this._symbol,
          Amount = this._amount,
          Holds = this._holds,
          BondedAmount = this._bondedAmount,
          ReservedAmount = this._reservedAmount,
          UnbondingAmount = this._unbondingAmount,
          UnvestedAmount = this._unvestedAmount,
          PendingRewardsAmount = this._pendingRewardsAmount,
          PastRewardsAmount = this._pastRewardsAmount,
          BondableAmount = this._bondableAmount,
          WithdrawableAmount = this._withdrawableAmount,
          FiatAmount = this._fiatAmount
        };
      }
    }
  }
}
