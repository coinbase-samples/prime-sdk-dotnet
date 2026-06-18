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

namespace CoinbaseSdk.Prime.Staking
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Core.Error;
  using CoinbaseSdk.Prime.Model;

  /// <summary>
  /// Claim Wallet Staking Rewards (Alpha).
  /// </summary>
  public class ClaimStakingRewardsRequest(string portfolioId, string walletId)
  {
    [JsonIgnore]
    public string PortfolioId { get; set; } = portfolioId;

    [JsonIgnore]
    public string WalletId { get; set; } = walletId;

    public string? IdempotencyKey { get; set; }

    public WalletClaimRewardsInputs Inputs { get; set; }

    public class ClaimStakingRewardsRequestBuilder
    {
      private string? _portfolioId;
      private string? _walletId;
      private string? _idempotencyKey;
      private WalletClaimRewardsInputs _inputs;

      public ClaimStakingRewardsRequestBuilder WithPortfolioId(string portfolioId)
      {
        _portfolioId = portfolioId;
        return this;
      }

      public ClaimStakingRewardsRequestBuilder WithWalletId(string walletId)
      {
        _walletId = walletId;
        return this;
      }

      public ClaimStakingRewardsRequestBuilder WithIdempotencyKey(string? idempotencyKey)
      {
        _idempotencyKey = idempotencyKey;
        return this;
      }

      public ClaimStakingRewardsRequestBuilder WithInputs(WalletClaimRewardsInputs inputs)
      {
        _inputs = inputs;
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

      public ClaimStakingRewardsRequest Build()
      {
        Validate();
        return new ClaimStakingRewardsRequest(_portfolioId!, _walletId!)
        {
          IdempotencyKey = _idempotencyKey,
          Inputs = _inputs,
        };
      }
    }
  }
}
