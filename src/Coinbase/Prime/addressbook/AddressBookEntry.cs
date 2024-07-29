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

using Newtonsoft.Json;

namespace Coinbase.Prime.AddressBook
{
  public class AddressBookEntry
  {
    public string? Id { get; set; }

    [JsonProperty("currency_symbol")]
    public string? CurrencySymbol { get; set; }

    public string? Name { get; set; }
    public string? Address { get; set; }

    [JsonProperty("account_identifier")]
    public string? AccountIdentifier { get; set; }

    [JsonProperty("account_identifier_name")]
    public string? AccountIdentifierName { get; set; }

    public string? State { get; set; }

    [JsonProperty("explorer_link")]
    public string? ExplorerLink { get; set; }

    [JsonProperty("last_used_at")]
    public string? LastUsedAt { get; set; }

    [JsonProperty("added_at")]
    public string? AddedAt { get; set; }

    [JsonProperty("added_by")]
    public User? AddedBy { get; set; }

    public AddressBookType Type { get; set; }

    [JsonProperty("counterparty_id")]
    public string? CounterpartyId { get; set; }

    public AddressBookEntry()
    {
    }

    public AddressBookEntry(
        string id,
        string currencySymbol,
        string name,
        string address,
        string accountIdentifier,
        string accountIdentifierName,
        string state,
        string explorerLink,
        string lastUsedAt,
        string addedAt,
        User addedBy,
        AddressBookType type,
        string counterpartyId)
    {
      Id = id;
      CurrencySymbol = currencySymbol;
      Name = name;
      Address = address;
      AccountIdentifier = accountIdentifier;
      AccountIdentifierName = accountIdentifierName;
      State = state;
      ExplorerLink = explorerLink;
      LastUsedAt = lastUsedAt;
      AddedAt = addedAt;
      AddedBy = addedBy;
      Type = type;
      CounterpartyId = counterpartyId;
    }
  }
}
