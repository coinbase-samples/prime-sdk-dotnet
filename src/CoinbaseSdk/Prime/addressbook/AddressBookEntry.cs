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
  using CoinbaseSdk.Prime.Model;

  public class AddressBookEntry
  {
    public string? Id { get; set; }

    [JsonPropertyName("currency_symbol")]
    public string? CurrencySymbol { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    [JsonPropertyName("account_identifier")]
    public string? AccountIdentifier { get; set; }

    [JsonPropertyName("account_identifier_name")]
    public string? AccountIdentifierName { get; set; }

    public string? State { get; set; }

    [JsonPropertyName("explorer_link")]
    public string? ExplorerLink { get; set; }

    [JsonPropertyName("last_used_at")]
    public string? LastUsedAt { get; set; }

    [JsonPropertyName("added_at")]
    public string? AddedAt { get; set; }

    [JsonPropertyName("added_by")]
    public User? AddedBy { get; set; }

    public AddressBookType Type { get; set; }

    [JsonPropertyName("counterparty_id")]
    public string? CounterpartyId { get; set; }

    public AddressBookEntry()
    {
    }

    public class AddressBookEntryBuilder
    {
      private string? _id;
      private string? _currencySymbol;
      private string? _name;
      private string? _address;
      private string? _accountIdentifier;
      private string? _accountIdentifierName;
      private string? _state;
      private string? _explorerLink;
      private string? _lastUsedAt;
      private string? _addedAt;
      private User? _addedBy;
      private AddressBookType _type;
      private string? _counterpartyId;

      public AddressBookEntryBuilder WithId(string? id)
      {
        this._id = id;
        return this;
      }

      public AddressBookEntryBuilder WithCurrencySymbol(string? currencySymbol)
      {
        this._currencySymbol = currencySymbol;
        return this;
      }

      public AddressBookEntryBuilder WithName(string? name)
      {
        this._name = name;
        return this;
      }

      public AddressBookEntryBuilder WithAddress(string? address)
      {
        this._address = address;
        return this;
      }

      public AddressBookEntryBuilder WithAccountIdentifier(string? accountIdentifier)
      {
        this._accountIdentifier = accountIdentifier;
        return this;
      }

      public AddressBookEntryBuilder WithAccountIdentifierName(string? accountIdentifierName)
      {
        this._accountIdentifierName = accountIdentifierName;
        return this;
      }

      public AddressBookEntryBuilder WithState(string? state)
      {
        this._state = state;
        return this;
      }

      public AddressBookEntryBuilder WithExplorerLink(string? explorerLink)
      {
        this._explorerLink = explorerLink;
        return this;
      }

      public AddressBookEntryBuilder WithLastUsedAt(string? lastUsedAt)
      {
        this._lastUsedAt = lastUsedAt;
        return this;
      }

      public AddressBookEntryBuilder WithAddedAt(string? addedAt)
      {
        this._addedAt = addedAt;
        return this;
      }

      public AddressBookEntryBuilder WithAddedBy(User? addedBy)
      {
        this._addedBy = addedBy;
        return this;
      }

      public AddressBookEntryBuilder WithType(AddressBookType type)
      {
        this._type = type;
        return this;
      }

      public AddressBookEntryBuilder WithCounterpartyId(string? counterpartyId)
      {
        this._counterpartyId = counterpartyId;
        return this;
      }

      public AddressBookEntry Build()
      {
        return new AddressBookEntry
        {
          Id = this._id,
          CurrencySymbol = this._currencySymbol,
          Name = this._name,
          Address = this._address,
          AccountIdentifier = this._accountIdentifier,
          AccountIdentifierName = this._accountIdentifierName,
          State = this._state,
          ExplorerLink = this._explorerLink,
          LastUsedAt = this._lastUsedAt,
          AddedAt = this._addedAt,
          AddedBy = this._addedBy,
          Type = this._type,
          CounterpartyId = this._counterpartyId
        };
      }
    }
  }
}
