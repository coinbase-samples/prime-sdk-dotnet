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
  public class CreateAddressBookEntryRequest
  {
    public string? Address { get; set; }

    [JsonPropertyName("currency_symbol")]
    public string? CurrencySymbol { get; set; }

    public string? Name { get; set; }

    [JsonPropertyName("account_identifier")]
    public string? AccountIdentifier { get; set; }

    public CreateAddressBookEntryRequest() { }

    public CreateAddressBookEntryRequest(
        string address,
        string currencySymbol,
        string name,
        string accountIdentifier)
    {
      Address = address;
      CurrencySymbol = currencySymbol;
      Name = name;
      AccountIdentifier = accountIdentifier;
    }
  }
}
