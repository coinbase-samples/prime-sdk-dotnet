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

using System.Text.Json.Serialization;
using Coinbase.Core.Error;
using Coinbase.Prime.Common;

namespace Coinbase.Prime.Wallets
{
  public class GetWalletByIdRequest(string portfolioId, string walletId)
  : BasePrimeRequest(portfolioId, walletId)
  {
    [JsonIgnore]
    public string WalletId { get; set; } = walletId;

    public class GetWalletByIdRequestBuilder
    {
      private string? _portfolioId;
      private string? _walletId;

      public GetWalletByIdRequestBuilder WithPortfolioId(string portfolioId)
      {
        this._portfolioId = portfolioId;
        return this;
      }

      public GetWalletByIdRequestBuilder WithWalletId(string walletId)
      {
        this._walletId = walletId;
        return this;
      }

      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(this._portfolioId))
        {
          throw new CoinbaseClientException("PortfolioId is required");
        }
        if (string.IsNullOrWhiteSpace(this._walletId))
        {
          throw new CoinbaseClientException("WalletId is required");
        }
      }

      public GetWalletByIdRequest Build()
      {
        this.Validate();
        return new GetWalletByIdRequest(this._portfolioId!, this._walletId!);
      }
    }
  }
}