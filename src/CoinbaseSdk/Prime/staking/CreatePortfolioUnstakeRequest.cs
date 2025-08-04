/*
 * Copyright 2025-present Coinbase Global, Inc.
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

namespace CoinbaseSdk.Prime.Staking
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Prime.Model;

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

    public class CreatePortfolioUnstakeRequestBuilder
    {
      private string? _idempotencyKey;
      private string? _currencySymbol;
      private string? _amount;
      private PortfolioStakingMetadata? _metadata;

      public CreatePortfolioUnstakeRequestBuilder WithIdempotencyKey(string? idempotencyKey)
      {
        this._idempotencyKey = idempotencyKey;
        return this;
      }

      public CreatePortfolioUnstakeRequestBuilder WithCurrencySymbol(string? currencySymbol)
      {
        this._currencySymbol = currencySymbol;
        return this;
      }

      public CreatePortfolioUnstakeRequestBuilder WithAmount(string? amount)
      {
        this._amount = amount;
        return this;
      }

      public CreatePortfolioUnstakeRequestBuilder WithMetadata(PortfolioStakingMetadata? metadata)
      {
        this._metadata = metadata;
        return this;
      }

      public CreatePortfolioUnstakeRequest Build(string portfolioId)
      {
        return new CreatePortfolioUnstakeRequest(portfolioId)
        {
          IdempotencyKey = this._idempotencyKey,
          CurrencySymbol = this._currencySymbol,
          Amount = this._amount,
          Metadata = this._metadata
        };
      }
    }
  }
}