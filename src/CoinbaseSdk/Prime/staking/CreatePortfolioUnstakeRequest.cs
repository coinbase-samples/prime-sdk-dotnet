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
  using CoinbaseSdk.Prime.Model.Enums;

  /// <summary>
  /// Request to unstake currency across a portfolio.
  /// </summary>
  public class CreatePortfolioUnstakeRequest(string portfolioId)
  {
    [JsonIgnore]
    public string PortfolioId { get; set; } = portfolioId;

    [JsonPropertyName("idempotency_key")]
    public string? IdempotencyKey { get; set; }

    [JsonPropertyName("currency_symbol")]
    public string? CurrencySymbol { get; set; }

    [JsonPropertyName("amount")]
    public string? Amount { get; set; }

    [JsonPropertyName("metadata")]
    public PortfolioStakingMetadata? Metadata { get; set; }

    [JsonPropertyName("validator_provider")]
    public ValidatorProvider? ValidatorProvider { get; set; }

    public class CreatePortfolioUnstakeRequestBuilder
    {
      private string? _portfolioId;
      private string? _idempotencyKey;
      private string? _currencySymbol;
      private string? _amount;
      private PortfolioStakingMetadata? _metadata;
      private ValidatorProvider? _validatorProvider;

      public CreatePortfolioUnstakeRequestBuilder WithPortfolioId(string portfolioId)
      {
        _portfolioId = portfolioId;
        return this;
      }

      public CreatePortfolioUnstakeRequestBuilder WithIdempotencyKey(string? idempotencyKey)
      {
        _idempotencyKey = idempotencyKey;
        return this;
      }

      public CreatePortfolioUnstakeRequestBuilder WithCurrencySymbol(string? currencySymbol)
      {
        _currencySymbol = currencySymbol;
        return this;
      }

      public CreatePortfolioUnstakeRequestBuilder WithAmount(string? amount)
      {
        _amount = amount;
        return this;
      }

      public CreatePortfolioUnstakeRequestBuilder WithMetadata(PortfolioStakingMetadata? metadata)
      {
        _metadata = metadata;
        return this;
      }

      public CreatePortfolioUnstakeRequestBuilder WithValidatorProvider(ValidatorProvider? validatorProvider)
      {
        _validatorProvider = validatorProvider;
        return this;
      }

      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(_portfolioId))
        {
          throw new CoinbaseClientException("PortfolioId is required");
        }
      }

      public CreatePortfolioUnstakeRequest Build()
      {
        Validate();
        return new CreatePortfolioUnstakeRequest(_portfolioId!)
        {
          IdempotencyKey = _idempotencyKey,
          CurrencySymbol = _currencySymbol,
          Amount = _amount,
          Metadata = _metadata,
          ValidatorProvider = _validatorProvider,
        };
      }
    }
  }
}
