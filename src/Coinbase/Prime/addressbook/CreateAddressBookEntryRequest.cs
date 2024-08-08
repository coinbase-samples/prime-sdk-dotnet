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

namespace Coinbase.Prime.AddressBook
{
  using System.Text.Json.Serialization;
  using Coinbase.Core.Error;
  using Coinbase.Prime.Common;

  public class CreateAddressBookEntryRequest(string portfolioId)
  : BasePrimeRequest(portfolioId, null)
  {
    public string? Address { get; set; }

    [JsonPropertyName("currency_symbol")]
    public string? CurrencySymbol { get; set; }

    public string? Name { get; set; }

    [JsonPropertyName("account_identifier")]
    public string? AccountIdentifier { get; set; }

    public class CreateAddressBookEntryRequestBuilder
    {
      private string? _portfolioId;
      private string? _address;
      private string? _currencySymbol;
      private string? _name;
      private string? _accountIdentifier;

      public CreateAddressBookEntryRequestBuilder WithPortfolioId(string portfolioId)
      {
        this._portfolioId = portfolioId;
        return this;
      }

      public CreateAddressBookEntryRequestBuilder WithAddress(string? address)
      {
        this._address = address;
        return this;
      }

      public CreateAddressBookEntryRequestBuilder WithCurrencySymbol(string? currencySymbol)
      {
        this._currencySymbol = currencySymbol;
        return this;
      }

      public CreateAddressBookEntryRequestBuilder WithName(string? name)
      {
        this._name = name;
        return this;
      }

      public CreateAddressBookEntryRequestBuilder WithAccountIdentifier(string? accountIdentifier)
      {
        this._accountIdentifier = accountIdentifier;
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
      }

      /// <summary>
      /// Build the <see cref="CreateAddressBookEntryRequest"/> object.
      /// </summary>
      /// <returns>The <see cref="CreateAddressBookEntryRequest"/> object.</returns>
      /// <exception cref="CoinbaseClientException">Thrown when the required fields are not set.</exception>
      public CreateAddressBookEntryRequest Build()
      {
        this.Validate();
        return new CreateAddressBookEntryRequest(this._portfolioId!)
        {
          Address = this._address,
          CurrencySymbol = this._currencySymbol,
          Name = this._name,
          AccountIdentifier = this._accountIdentifier
        };
      }
    }
  }
}
