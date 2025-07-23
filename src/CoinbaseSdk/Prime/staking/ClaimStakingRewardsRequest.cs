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

  /// <summary>
  /// Request object for claiming staking rewards from a wallet.
  /// </summary>
  public class ClaimStakingRewardsRequest(string portfolioId, string walletId)
  {
    /// <summary>
    /// The portfolio ID.
    /// </summary>
    [JsonIgnore]
    public string PortfolioId { get; set; } = portfolioId;

    /// <summary>
    /// The wallet ID.
    /// </summary>
    [JsonIgnore]
    public string WalletId { get; set; } = walletId;

    /// <summary>
    /// The client generated idempotency key for requested execution.
    /// Any subsequent requests with the same key will return the original response.
    /// </summary>
    [JsonPropertyName("idempotency_key")]
    public string? IdempotencyKey { get; set; }

    /// <summary>
    /// Builder for <see cref="ClaimStakingRewardsRequest"/>.
    /// </summary>
    public class ClaimStakingRewardsRequestBuilder
    {
      private string? _portfolioId;
      private string? _walletId;
      private string? _idempotencyKey;

      /// <summary>
      /// Sets the portfolio ID.
      /// </summary>
      /// <param name="portfolioId">The portfolio ID.</param>
      /// <returns>The builder instance.</returns>
      public ClaimStakingRewardsRequestBuilder WithPortfolioId(string portfolioId)
      {
        this._portfolioId = portfolioId;
        return this;
      }

      /// <summary>
      /// Sets the wallet ID.
      /// </summary>
      /// <param name="walletId">The wallet ID.</param>
      /// <returns>The builder instance.</returns>
      public ClaimStakingRewardsRequestBuilder WithWalletId(string walletId)
      {
        this._walletId = walletId;
        return this;
      }

      /// <summary>
      /// Sets the idempotency key.
      /// </summary>
      /// <param name="idempotencyKey">The idempotency key.</param>
      /// <returns>The builder instance.</returns>
      public ClaimStakingRewardsRequestBuilder WithIdempotencyKey(string idempotencyKey)
      {
        this._idempotencyKey = idempotencyKey;
        return this;
      }

      /// <summary>
      /// Validates the builder.
      /// </summary>
      /// <exception cref="CoinbaseClientException">Thrown when the
      /// <see cref="_portfolioId"/>, <see cref="_walletId"/>, or <see cref="_idempotencyKey"/> are null, empty
      /// or whitespace.</exception>
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

        if (string.IsNullOrWhiteSpace(this._idempotencyKey))
        {
          throw new CoinbaseClientException("IdempotencyKey is required");
        }
      }

      /// <summary>
      /// Builds the <see cref="ClaimStakingRewardsRequest"/>.
      /// </summary>
      /// <returns>The <see cref="ClaimStakingRewardsRequest"/>.</returns>
      /// <exception cref="CoinbaseClientException">Thrown when the required fields are not set.</exception>
      public ClaimStakingRewardsRequest Build()
      {
        this.Validate();
        return new ClaimStakingRewardsRequest(this._portfolioId!, this._walletId!)
        {
          IdempotencyKey = this._idempotencyKey
        };
      }
    }
  }
}