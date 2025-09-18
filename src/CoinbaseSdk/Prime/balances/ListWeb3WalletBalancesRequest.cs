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
  using CoinbaseSdk.Prime.Model;

  public class ListWeb3WalletBalancesRequest(string walletId) : PaginatedRequest
  {
    [JsonIgnore]
    public string WalletId { get; set; } = walletId;


    public class ListWeb3WalletBalancesRequestBuilder : PaginatedRequestBuilder<ListWeb3WalletBalancesRequest, ListWeb3WalletBalancesRequestBuilder>
    {
      private string? _walletId;

      public ListWeb3WalletBalancesRequestBuilder WithWalletId(string walletId)
      {
        _walletId = walletId;
        return this;
      }


      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(_walletId))
        {
          throw new CoinbaseClientException("WalletId is required");
        }
      }

      public override ListWeb3WalletBalancesRequest Build()
      {
        this.Validate();
        var request = new ListWeb3WalletBalancesRequest(_walletId!);
        SetPaginationProperties(request);
        return request;
      }
    }
  }
}