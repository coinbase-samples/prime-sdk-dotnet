/*
 * Copyright 2024-present Coinbase Global, Inc.
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

namespace CoinbaseSdk.Prime.Balances
{
  public class TotalBalance
  {
    public string? Total { get; set; }
    public string? Holds { get; set; }

    public TotalBalance() { }

    public TotalBalance(string total, string holds)
    {
      Total = total;
      Holds = holds;
    }

    public class TotalBalanceBuilder
    {
      private string? _total;
      private string? _holds;

      public TotalBalanceBuilder WithTotal(string? total)
      {
        this._total = total;
        return this;
      }

      public TotalBalanceBuilder WithHolds(string? holds)
      {
        this._holds = holds;
        return this;
      }

      public TotalBalance Build()
      {
        return new TotalBalance
        {
          Total = this._total,
          Holds = this._holds
        };
      }
    }
  }
}
