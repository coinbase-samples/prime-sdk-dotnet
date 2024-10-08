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

namespace CoinbaseSdk.Prime.Wallets
{
  using CoinbaseSdk.Prime.Common;
  public class ListWalletsResponse
  {
    public Wallet[] Wallets { get; set; } = [];
    public Pagination? Pagination { get; set; }

    public ListWalletsResponse() { }

    public class ListWalletsResponseBuilder
    {
      private Wallet[] _wallets = [];
      private Pagination? _pagination;

      public ListWalletsResponseBuilder() { }

      public ListWalletsResponseBuilder WithWallets(Wallet[] wallets)
      {
        this._wallets = wallets;
        return this;
      }

      public ListWalletsResponseBuilder WithPagination(Pagination pagination)
      {
        this._pagination = pagination;
        return this;
      }

      public ListWalletsResponse Build()
      {
        return new ListWalletsResponse
        {
          Wallets = this._wallets,
          Pagination = this._pagination
        };
      }
    }
  }
}