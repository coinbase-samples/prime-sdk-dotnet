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
  /// Request to unstake a wallet.
  /// </summary>
  public class CreateUnstakeRequest(string portfolioId, string walletId)
  {
    [JsonIgnore]
    public string PortfolioId { get; set; } = portfolioId;

    [JsonIgnore]
    public string WalletId { get; set; } = walletId;

    public string? IdempotencyKey { get; set; }

    public WalletUnstakeInputs Inputs { get; set; }

    public WalletStakingMetadata Metadata { get; set; }

    public class CreateUnstakeRequestBuilder
    {
      private string? _portfolioId;
      private string? _walletId;
      private string? _idempotencyKey;
      private WalletUnstakeInputs _inputs;
      private WalletStakingMetadata _metadata;

      public CreateUnstakeRequestBuilder WithPortfolioId(string portfolioId)
      {
        _portfolioId = portfolioId;
        return this;
      }

      public CreateUnstakeRequestBuilder WithWalletId(string walletId)
      {
        _walletId = walletId;
        return this;
      }

      public CreateUnstakeRequestBuilder WithIdempotencyKey(string? idempotencyKey)
      {
        _idempotencyKey = idempotencyKey;
        return this;
      }

      public CreateUnstakeRequestBuilder WithInputs(WalletUnstakeInputs inputs)
      {
        _inputs = inputs;
        return this;
      }

      public CreateUnstakeRequestBuilder WithMetadata(WalletStakingMetadata metadata)
      {
        _metadata = metadata;
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

      public CreateUnstakeRequest Build()
      {
        Validate();
        return new CreateUnstakeRequest(_portfolioId!, _walletId!)
        {
          IdempotencyKey = _idempotencyKey,
          Inputs = _inputs,
          Metadata = _metadata,
        };
      }
    }
  }
}
