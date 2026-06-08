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
  /// Request to stake currency in a portfolio.
  /// </summary>
  public class CreatePortfolioStakeRequest(string portfolioId)
  {
    [JsonIgnore]
    public string PortfolioId { get; set; } = portfolioId;

    public string? IdempotencyKey { get; set; }

    public string? CurrencySymbol { get; set; }

    public string? Amount { get; set; }

    public PortfolioStakingMetadata Metadata { get; set; }

    public class CreatePortfolioStakeRequestBuilder
    {
      private string? _portfolioId;
      private string? _idempotencyKey;
      private string? _currencySymbol;
      private string? _amount;
      private PortfolioStakingMetadata _metadata;

      public CreatePortfolioStakeRequestBuilder WithPortfolioId(string portfolioId)
      {
        _portfolioId = portfolioId;
        return this;
      }

      public CreatePortfolioStakeRequestBuilder WithIdempotencyKey(string? idempotencyKey)
      {
        _idempotencyKey = idempotencyKey;
        return this;
      }

      public CreatePortfolioStakeRequestBuilder WithCurrencySymbol(string? currencySymbol)
      {
        _currencySymbol = currencySymbol;
        return this;
      }

      public CreatePortfolioStakeRequestBuilder WithAmount(string? amount)
      {
        _amount = amount;
        return this;
      }

      public CreatePortfolioStakeRequestBuilder WithMetadata(PortfolioStakingMetadata metadata)
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
      }

      public CreatePortfolioStakeRequest Build()
      {
        Validate();
        return new CreatePortfolioStakeRequest(_portfolioId!)
        {
          IdempotencyKey = _idempotencyKey,
          CurrencySymbol = _currencySymbol,
          Amount = _amount,
          Metadata = _metadata,
        };
      }
    }
  }
}
