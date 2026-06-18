/*
 * Copyright 2026-present Coinbase Global, Inc.
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

namespace CoinbaseSdk.Prime.Staking
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Core.Error;

  /// <summary>
  /// Preview Unstake.
  /// </summary>
  public class PreviewUnstakeRequest(string portfolioId, string walletId)
  {
    [JsonIgnore]
    public string PortfolioId { get; set; } = portfolioId;

    [JsonIgnore]
    public string WalletId { get; set; } = walletId;

    public string? Amount { get; set; }

    public class PreviewUnstakeRequestBuilder
    {
      private string? _portfolioId;
      private string? _walletId;
      private string? _amount;

      public PreviewUnstakeRequestBuilder WithPortfolioId(string portfolioId)
      {
        _portfolioId = portfolioId;
        return this;
      }

      public PreviewUnstakeRequestBuilder WithWalletId(string walletId)
      {
        _walletId = walletId;
        return this;
      }

      public PreviewUnstakeRequestBuilder WithAmount(string? amount)
      {
        _amount = amount;
        return this;
      }

      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(_portfolioId))
        {
          throw new CoinbaseClientException("PortfolioId is required");
        }
        if (string.IsNullOrWhiteSpace(_walletId))
        {
          throw new CoinbaseClientException("WalletId is required");
        }
      }

      public PreviewUnstakeRequest Build()
      {
        Validate();
        return new PreviewUnstakeRequest(_portfolioId!, _walletId!)
        {
          Amount = _amount,
        };
      }
    }
  }
}
