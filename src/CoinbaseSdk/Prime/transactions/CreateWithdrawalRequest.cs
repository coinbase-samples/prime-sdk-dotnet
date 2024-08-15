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
  using CoinbaseSdk.Prime.Common;
  using CoinbaseSdk.Prime.Model;

  public class CreateWithdrawalRequest(string portfolioId, string walletId)
  : BasePrimeRequest(portfolioId, null)
  {
    [JsonIgnore]
    public string WalletId { get; set; } = walletId;

    public string? Amount { get; set; }

    [JsonPropertyName("destination_type")]
    public DestinationType? DestinationType { get; set; }

    [JsonPropertyName("idempotency_key")]
    public string? IdempotencyKey { get; set; }

    [JsonPropertyName("currency_symbol")]
    public string? CurrencySymbol { get; set; }

    [JsonPropertyName("payment_method")]
    public PaymentMethod? PaymentMethod { get; set; }

    [JsonPropertyName("blockchain_address")]
    public BlockchainAddress? BlockchainAddress { get; set; }

    public class CreateWithdrawalRequestBuilder
    {
      private string? _portfolioId;
      private string? _walletId;
      private string? _amount;
      private DestinationType? _destinationType;
      private string? _idempotencyKey;
      private string? _currencySymbol;
      private PaymentMethod? _paymentMethod;
      private BlockchainAddress? _blockchainAddress;

      public CreateWithdrawalRequestBuilder WithPortfolioId(string portfolioId)
      {
        this._portfolioId = portfolioId;
        return this;
      }

      public CreateWithdrawalRequestBuilder WithWalletId(string walletId)
      {
        this._walletId = walletId;
        return this;
      }

      public CreateWithdrawalRequestBuilder WithAmount(string amount)
      {
        this._amount = amount;
        return this;
      }

      public CreateWithdrawalRequestBuilder WithDestinationType(DestinationType destinationType)
      {
        this._destinationType = destinationType;
        return this;
      }

      public CreateWithdrawalRequestBuilder WithIdempotencyKey(string idempotencyKey)
      {
        this._idempotencyKey = idempotencyKey;
        return this;
      }

      public CreateWithdrawalRequestBuilder WithCurrencySymbol(string currencySymbol)
      {
        this._currencySymbol = currencySymbol;
        return this;
      }

      public CreateWithdrawalRequestBuilder WithPaymentMethod(PaymentMethod paymentMethod)
      {
        this._paymentMethod = paymentMethod;
        return this;
      }

      public CreateWithdrawalRequestBuilder WithBlockchainAddress(BlockchainAddress blockchainAddress)
      {
        this._blockchainAddress = blockchainAddress;
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
      /// Build the <see cref="CreateWithdrawalRequest"/> object.
      /// </summary>
      /// <returns>The <see cref="CreateWithdrawalRequest"/> object.</returns>
      /// <exception cref="CoinbaseClientException">Thrown when the required fields are not set.</exception>
      public CreateWithdrawalRequest Build()
      {
        this.Validate();
        return new CreateWithdrawalRequest(this._portfolioId!, this._walletId!)
        {
          Amount = this._amount,
          DestinationType = this._destinationType,
          IdempotencyKey = this._idempotencyKey,
          CurrencySymbol = this._currencySymbol,
          PaymentMethod = this._paymentMethod,
          BlockchainAddress = this._blockchainAddress
        };
      }
    }
  }
}
