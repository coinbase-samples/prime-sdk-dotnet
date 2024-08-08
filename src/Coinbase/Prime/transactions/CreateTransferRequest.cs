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

  public class CreateTransferRequest(string portfolioId, string walletId)
  : BasePrimeRequest(portfolioId, null)
  {
    public string WalletId { get; set; } = walletId;
    public string? Amount { get; set; }
    public string? Destination { get; set; }

    [JsonPropertyName("idempotency_key")]
    public string? IdempotencyKey { get; set; }

    [JsonPropertyName("currency_symbol")]
    public string? CurrencySymbol { get; set; }

    public class CreateTransferRequestBuilder
    {
      private string? _portfolioId;
      private string? _walletId;
      private string? _amount;
      private string? _destination;
      private string? _idempotencyKey;
      private string? _currencySymbol;

      public CreateTransferRequestBuilder WithPortfolioId(string portfolioId)
      {
        this._portfolioId = portfolioId;
        return this;
      }

      public CreateTransferRequestBuilder WithWalletId(string walletId)
      {
        this._walletId = walletId;
        return this;
      }

      public CreateTransferRequestBuilder WithAmount(string amount)
      {
        this._amount = amount;
        return this;
      }

      public CreateTransferRequestBuilder WithDestination(string destination)
      {
        this._destination = destination;
        return this;
      }

      public CreateTransferRequestBuilder WithIdempotencyKey(string idempotencyKey)
      {
        this._idempotencyKey = idempotencyKey;
        return this;
      }

      public CreateTransferRequestBuilder WithCurrencySymbol(string currencySymbol)
      {
        this._currencySymbol = currencySymbol;
        return this;
      }

      /// <summary>
      /// Validate the builder.
      /// </summary>
      /// <exception cref="CoinbaseClientException">Thrown when the
      /// <see cref="_portfolioId"/> is null, empty or whitespace.</exception>
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

      /// <summary>
      /// Build the <see cref="CreateTransferRequest"/> object.
      /// </summary>
      /// <returns>The <see cref="CreateTransferRequest"/> object.</returns>
      /// <exception cref="CoinbaseClientException">Thrown when the required fields are not set.</exception>
      public CreateTransferRequest Build()
      {
        this.Validate();
        return new CreateTransferRequest(this._portfolioId!, this._walletId!)
        {
          Amount = this._amount,
          Destination = this._destination,
          IdempotencyKey = this._idempotencyKey,
          CurrencySymbol = this._currencySymbol
        };
      }
    }
  }
}
