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
  public class ListPortfolioBalancesResponse
  {
    public Balance[] Balances { get; set; } = [];
    public BalanceType Type { get; set; }

    [JsonPropertyName("trading_balances")]
    public TotalBalance? TradingBalances { get; set; }

    [JsonPropertyName("vault_balances")]
    public TotalBalance? VaultBalances { get; set; }

    public ListPortfolioBalancesResponse() { }

    public class ListPortfolioBalancesResponseBuilder
    {
      private Balance[] _balances = Array.Empty<Balance>();
      private BalanceType _type;
      private TotalBalance? _tradingBalances;
      private TotalBalance? _vaultBalances;

      public ListPortfolioBalancesResponseBuilder WithBalances(Balance[] balances)
      {
        this._balances = balances;
        return this;
      }

      public ListPortfolioBalancesResponseBuilder WithType(BalanceType type)
      {
        this._type = type;
        return this;
      }

      public ListPortfolioBalancesResponseBuilder WithTradingBalances(TotalBalance? tradingBalances)
      {
        this._tradingBalances = tradingBalances;
        return this;
      }

      public ListPortfolioBalancesResponseBuilder WithVaultBalances(TotalBalance? vaultBalances)
      {
        this._vaultBalances = vaultBalances;
        return this;
      }

      public ListPortfolioBalancesResponse Build()
      {
        return new ListPortfolioBalancesResponse
        {
          Balances = this._balances,
          Type = this._type,
          TradingBalances = this._tradingBalances,
          VaultBalances = this._vaultBalances
        };
      }
    }
  }
}
