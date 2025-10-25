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

namespace CoinbaseSdk.Prime.Transactions
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Core.Error;

  public class CreateConversionRequest(string portfolioId, string walletId)
  {
    [JsonIgnore]
    public string PortfolioId { get; set; } = portfolioId;

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
        _portfolioId = portfolioId;
        return this;
      }

      public CreateConversionRequestBuilder WithWalletId(string walletId)
      {
        _walletId = walletId;
        return this;
      }

      public CreateConversionRequestBuilder WithAmount(string amount)
      {
        _amount = amount;
        return this;
      }

      public CreateConversionRequestBuilder WithDestination(string destination)
      {
        _destination = destination;
        return this;
      }

      public CreateConversionRequestBuilder WithIdempotencyKey(string idempotencyKey)
      {
        _idempotencyKey = idempotencyKey;
        return this;
      }

      public CreateConversionRequestBuilder WithSourceSymbol(string sourceSymbol)
      {
        _sourceSymbol = sourceSymbol;
        return this;
      }

      public CreateConversionRequestBuilder WithDestinationSymbol(string destinationSymbol)
      {
        _destinationSymbol = destinationSymbol;
        return this;
      }

      /// <summary>
      /// Validate the builder.
      /// </summary>
      /// <exception cref="CoinbaseClientException">Thrown when the
      /// <see cref="_portfolioId"/> or <see cref="_walletId"/> are null, empty
      /// or whitespace.</exception>
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

      /// <summary>
      /// Build the <see cref="CreateConversionRequest"/> object.
      /// </summary>
      /// <returns>The <see cref="CreateConversionRequest"/> object.</returns>
      /// <exception cref="CoinbaseClientException">Thrown when the required fields are not set.</exception>
      public CreateConversionRequest Build()
      {
        Validate();
        return new CreateConversionRequest(_portfolioId!, _walletId!)
        {
          Amount = _amount,
          Destination = _destination,
          IdempotencyKey = _idempotencyKey,
          SourceSymbol = _sourceSymbol,
          DestinationSymbol = _destinationSymbol,
        };
      }
    }
  }
}
