/*
 * Copyright 2024-present Coinbase Global, Inc.
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

namespace Coinbase.Prime.Transactions
{
  using System.Text.Json.Serialization;
  using Coinbase.Core.Error;
  using Coinbase.Prime.Common;

  public class CreateConversionRequest(string portfolioId, string walletId)
  : BasePrimeRequest(portfolioId, null)
  {
    [JsonIgnore]
    public string WalletId { get; set; } = walletId;
    public string? Amount { get; set; }
    public string? Destination { get; set; }

    [JsonPropertyName("idempotency_key")]
    public string? IdempotencyKey { get; set; }

    [JsonPropertyName("source_symbol")]
    public string? SourceSymbol { get; set; }

    [JsonPropertyName("destination_symbol")]
    public string? DestinationSymbol { get; set; }

    public class CreateConversionRequestBuilder
    {
      private string? _portfolioId;
      private string? _walletId;
      private string? _amount;
      private string? _destination;
      private string? _idempotencyKey;
      private string? _sourceSymbol;
      private string? _destinationSymbol;

      public CreateConversionRequestBuilder WithPortfolioId(string portfolioId)
      {
        this._portfolioId = portfolioId;
        return this;
      }

      public CreateConversionRequestBuilder WithWalletId(string walletId)
      {
        this._walletId = walletId;
        return this;
      }

      public CreateConversionRequestBuilder WithAmount(string amount)
      {
        this._amount = amount;
        return this;
      }

      public CreateConversionRequestBuilder WithDestination(string destination)
      {
        this._destination = destination;
        return this;
      }

      public CreateConversionRequestBuilder WithIdempotencyKey(string idempotencyKey)
      {
        this._idempotencyKey = idempotencyKey;
        return this;
      }

      public CreateConversionRequestBuilder WithSourceSymbol(string sourceSymbol)
      {
        this._sourceSymbol = sourceSymbol;
        return this;
      }

      public CreateConversionRequestBuilder WithDestinationSymbol(string destinationSymbol)
      {
        this._destinationSymbol = destinationSymbol;
        return this;
      }

      private void Validate()
      {
        if (string.IsNullOrEmpty(_portfolioId))
        {
          throw new CoinbaseClientException("PortfolioId is required");
        }
        if (string.IsNullOrEmpty(_walletId))
        {
          throw new CoinbaseClientException("WalletId is required");
        }
      }

      public CreateConversionRequest Build()
      {
        this.Validate();
        return new CreateConversionRequest(this._portfolioId!, this._walletId!)
        {
          Amount = this._amount,
          Destination = this._destination,
          IdempotencyKey = this._idempotencyKey,
          SourceSymbol = this._sourceSymbol,
          DestinationSymbol = this._destinationSymbol
        };
      }
    }
  }
}
