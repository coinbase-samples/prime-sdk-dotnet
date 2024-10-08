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

namespace CoinbaseSdk.Prime.Balances
{
  using CoinbaseSdk.Prime.Common;
  public class ListWeb3WalletBalancesResponse
  {
    public Web3WalletBalance[] Balances { get; set; } = [];
    public Pagination? Pagination { get; set; }

    public ListWeb3WalletBalancesResponse() { }

    public class ListWeb3WalletBalancesResponseBuilder
    {
      private Web3WalletBalance[] _balances = Array.Empty<Web3WalletBalance>();
      private Pagination? _pagination;

      public ListWeb3WalletBalancesResponseBuilder WithBalances(Web3WalletBalance[] balances)
      {
        this._balances = balances;
        return this;
      }

      public ListWeb3WalletBalancesResponseBuilder WithPagination(Pagination? pagination)
      {
        this._pagination = pagination;
        return this;
      }

      public ListWeb3WalletBalancesResponse Build()
      {
        return new ListWeb3WalletBalancesResponse
        {
          Balances = this._balances,
          Pagination = this._pagination
        };
      }
    }
  }
}
