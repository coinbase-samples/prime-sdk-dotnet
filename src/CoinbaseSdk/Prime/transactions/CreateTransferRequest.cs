/*
 * Copyright 2024-present Coinbase Global, Inc.
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

namespace CoinbaseSdk.Prime.Transactions
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Core.Error;

  /// <summary>
  /// Create Transfer.
  /// </summary>
  public class CreateTransferRequest(string portfolioId, string walletId)
  {
    [JsonIgnore]
    public string PortfolioId { get; set; } = portfolioId;

    [JsonIgnore]
    public string WalletId { get; set; } = walletId;

    public string? Amount { get; set; }

    public string? Destination { get; set; }

    public string? IdempotencyKey { get; set; }

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
        _portfolioId = portfolioId;
        return this;
      }

      public CreateTransferRequestBuilder WithWalletId(string walletId)
      {
        _walletId = walletId;
        return this;
      }

      public CreateTransferRequestBuilder WithAmount(string? amount)
      {
        _amount = amount;
        return this;
      }

      public CreateTransferRequestBuilder WithDestination(string? destination)
      {
        _destination = destination;
        return this;
      }

      public CreateTransferRequestBuilder WithIdempotencyKey(string? idempotencyKey)
      {
        _idempotencyKey = idempotencyKey;
        return this;
      }

      public CreateTransferRequestBuilder WithCurrencySymbol(string? currencySymbol)
      {
        _currencySymbol = currencySymbol;
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

      public CreateTransferRequest Build()
      {
        Validate();
        return new CreateTransferRequest(_portfolioId!, _walletId!)
        {
          Amount = _amount,
          Destination = _destination,
          IdempotencyKey = _idempotencyKey,
          CurrencySymbol = _currencySymbol,
        };
      }
    }
  }
}
