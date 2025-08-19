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

namespace CoinbaseSdk.Prime.Balances
{
  using CoinbaseSdk.Prime.Model;

  public class ListWeb3WalletBalancesResponse
  {
    public Web3Balance[] Balances { get; set; } = [];
    public PaginatedResponse? Pagination { get; set; }

    public ListWeb3WalletBalancesResponse()
    {
    }

    public class ListWeb3WalletBalancesResponseBuilder
    {
      private Web3Balance[] _balances = [];
      private PaginatedResponse? _pagination;

      public ListWeb3WalletBalancesResponseBuilder WithBalances(Web3Balance[] balances)
      {
        _balances = balances;
        return this;
      }

      public ListWeb3WalletBalancesResponseBuilder WithPagination(PaginatedResponse pagination)
      {
        _pagination = pagination;
        return this;
      }

      public ListWeb3WalletBalancesResponse Build()
      {
        return new ListWeb3WalletBalancesResponse()
        {
          Balances = _balances,
          Pagination = _pagination
        };
      }
    }
  }
}