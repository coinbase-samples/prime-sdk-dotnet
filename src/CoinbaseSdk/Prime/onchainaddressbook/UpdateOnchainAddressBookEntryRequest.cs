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

namespace CoinbaseSdk.Prime.OnchainAddressBook
{
  using System.Text.Json.Serialization;
  using CoinbaseSdk.Core.Error;
  using CoinbaseSdk.Prime.Model;

  /// <summary>
  /// Update Onchain Address Book Entry.
  /// </summary>
  public class UpdateOnchainAddressBookEntryRequest(string portfolioId)
  {
    [JsonIgnore]
    public string PortfolioId { get; set; } = portfolioId;

    public AddressGroup AddressGroup { get; set; }

    public class UpdateOnchainAddressBookEntryRequestBuilder
    {
      private string? _portfolioId;
      private AddressGroup _addressGroup;

      public UpdateOnchainAddressBookEntryRequestBuilder WithPortfolioId(string portfolioId)
      {
        _portfolioId = portfolioId;
        return this;
      }

      public UpdateOnchainAddressBookEntryRequestBuilder WithAddressGroup(AddressGroup addressGroup)
      {
        _addressGroup = addressGroup;
        return this;
      }

      private void Validate()
      {
        if (string.IsNullOrWhiteSpace(_portfolioId))
        {
          throw new CoinbaseClientException("PortfolioId is required");
        }
      }

      public UpdateOnchainAddressBookEntryRequest Build()
      {
        Validate();
        return new UpdateOnchainAddressBookEntryRequest(_portfolioId!)
        {
          AddressGroup = _addressGroup,
        };
      }
    }
  }
}
