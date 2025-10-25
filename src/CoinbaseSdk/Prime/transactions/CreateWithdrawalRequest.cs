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
  using CoinbaseSdk.Prime.Model;
  using CoinbaseSdk.Prime.Model.Enums;

  public class CreateWithdrawalRequest(string portfolioId, string walletId)
  {
    [JsonIgnore]
    public string PortfolioId { get; set; } = portfolioId;

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
    public PaymentMethodDestination? PaymentMethod { get; set; }

    [JsonPropertyName("blockchain_address")]
    public BlockchainAddress? BlockchainAddress { get; set; }

    [JsonPropertyName("counterparty")]
    public CounterpartyDestination? Counterparty { get; set; }

    public class Builder
    {
      private string? _portfolioId;
      private string? _walletId;
      private string? _amount;
      private DestinationType? _destinationType;
      private string? _idempotencyKey;
      private string? _currencySymbol;
      private PaymentMethodDestination? _paymentMethod;
      private BlockchainAddress? _blockchainAddress;
      private CounterpartyDestination? _counterparty;

      public Builder WithPortfolioId(string portfolioId)
      {
        _portfolioId = portfolioId;
        return this;
      }

      public Builder WithWalletId(string walletId)
      {
        _walletId = walletId;
        return this;
      }

      public Builder WithAmount(string amount)
      {
        _amount = amount;
        return this;
      }

      public Builder WithDestinationType(DestinationType destinationType)
      {
        _destinationType = destinationType;
        return this;
      }

      public Builder WithIdempotencyKey(string idempotencyKey)
      {
        _idempotencyKey = idempotencyKey;
        return this;
      }

      public Builder WithCurrencySymbol(string currencySymbol)
      {
        _currencySymbol = currencySymbol;
        return this;
      }

      public Builder WithPaymentMethod(PaymentMethodDestination paymentMethod)
      {
        _paymentMethod = paymentMethod;
        return this;
      }

      public Builder WithBlockchainAddress(BlockchainAddress blockchainAddress)
      {
        _blockchainAddress = blockchainAddress;
        return this;
      }

      public Builder WithCounterparty(CounterpartyDestination counterparty)
      {
        _counterparty = counterparty;
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
        if (string.IsNullOrWhiteSpace(_portfolioId))
        {
          throw new CoinbaseClientException("PortfolioId is required");
        }
        if (string.IsNullOrWhiteSpace(_walletId))
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
        Validate();
        return new CreateWithdrawalRequest(_portfolioId!, _walletId!)
        {
          Amount = _amount,
          DestinationType = _destinationType,
          IdempotencyKey = _idempotencyKey,
          CurrencySymbol = _currencySymbol,
          PaymentMethod = _paymentMethod,
          BlockchainAddress = _blockchainAddress,
          Counterparty = _counterparty
        };
      }
    }
  }
}
