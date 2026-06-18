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

namespace CoinbaseSdk.Prime.AddressBook
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Core.Error;

  /// <summary>
  /// Create Address Book Entry.
  /// </summary>
  public class CreateAddressBookEntryRequest(string portfolioId)
  {
    [JsonIgnore]
    public string PortfolioId { get; set; } = portfolioId;

    public string? Address { get; set; }

    public string? CurrencySymbol { get; set; }

    public string? Name { get; set; }

    public string? AccountIdentifier { get; set; }

    public string[] ChainIds { get; set; } = [];

    public class CreateAddressBookEntryRequestBuilder
    {
      private string? _portfolioId;
      private string? _address;
      private string? _currencySymbol;
      private string? _name;
      private string? _accountIdentifier;
      private string[] _chainIds;

      public CreateAddressBookEntryRequestBuilder WithPortfolioId(string portfolioId)
      {
        _portfolioId = portfolioId;
        return this;
      }

      public CreateAddressBookEntryRequestBuilder WithAddress(string? address)
      {
        _address = address;
        return this;
      }

      public CreateAddressBookEntryRequestBuilder WithCurrencySymbol(string? currencySymbol)
      {
        _currencySymbol = currencySymbol;
        return this;
      }

      public CreateAddressBookEntryRequestBuilder WithName(string? name)
      {
        _name = name;
        return this;
      }

      public CreateAddressBookEntryRequestBuilder WithAccountIdentifier(string? accountIdentifier)
      {
        _accountIdentifier = accountIdentifier;
        return this;
      }

      public CreateAddressBookEntryRequestBuilder WithChainIds(string[] chainIds)
      {
        _chainIds = chainIds;
        return this;
      }

      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(_portfolioId))
        {
          throw new CoinbaseClientException("PortfolioId is required");
        }
      }

      public CreateAddressBookEntryRequest Build()
      {
        Validate();
        return new CreateAddressBookEntryRequest(_portfolioId!)
        {
          Address = _address,
          CurrencySymbol = _currencySymbol,
          Name = _name,
          AccountIdentifier = _accountIdentifier,
          ChainIds = _chainIds ?? [],
        };
      }
    }
  }
}
