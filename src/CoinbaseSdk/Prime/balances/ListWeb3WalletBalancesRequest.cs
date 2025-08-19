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
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Core.Error;

  public class ListWeb3WalletBalancesRequest(string walletId)
  {
    [JsonIgnore]
    public string WalletId { get; set; } = walletId;

    public string? Cursor { get; set; }
    public int? Limit { get; set; }

    public class ListWeb3WalletBalancesRequestBuilder
    {
      private string? _walletId;
      private string? _cursor;
      private int? _limit;

      public ListWeb3WalletBalancesRequestBuilder WithWalletId(string walletId)
      {
        _walletId = walletId;
        return this;
      }

      public ListWeb3WalletBalancesRequestBuilder WithCursor(string cursor)
      {
        _cursor = cursor;
        return this;
      }

      public ListWeb3WalletBalancesRequestBuilder WithLimit(int limit)
      {
        _limit = limit;
        return this;
      }

      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(_walletId))
        {
          throw new CoinbaseClientException("WalletId is required");
        }
      }

      public ListWeb3WalletBalancesRequest Build()
      {
        this.Validate();
        return new ListWeb3WalletBalancesRequest(_walletId!)
        {
          Cursor = _cursor,
          Limit = _limit
        };
      }
    }
  }
}