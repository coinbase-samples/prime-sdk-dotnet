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

namespace CoinbaseSdk.Prime.Wallets
{
  using System.Text.Json.Serialization;
  public class CryptoDepositInstructions
  {
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public DepositType Type { get; set; }

    [JsonPropertyName("account_identifier")]
    public string? AccountIdentifier { get; set; }

    public CryptoDepositInstructions() { }

    public class CryptoDepositInstructionsBuilder
    {
      private string? _id;
      private string? _name;
      private string? _address;
      private DepositType _type;
      private string? _accountIdentifier;

      public CryptoDepositInstructionsBuilder() { }

      public CryptoDepositInstructionsBuilder WithId(string id)
      {
        this._id = id;
        return this;
      }

      public CryptoDepositInstructionsBuilder WithName(string name)
      {
        this._name = name;
        return this;
      }

      public CryptoDepositInstructionsBuilder WithAddress(string address)
      {
        this._address = address;
        return this;
      }

      public CryptoDepositInstructionsBuilder WithType(DepositType type)
      {
        this._type = type;
        return this;
      }

      public CryptoDepositInstructionsBuilder WithAccountIdentifier(string accountIdentifier)
      {
        this._accountIdentifier = accountIdentifier;
        return this;
      }

      public CryptoDepositInstructions Build()
      {
        return new CryptoDepositInstructions
        {
          Id = this._id,
          Name = this._name,
          Address = this._address,
          Type = this._type,
          AccountIdentifier = this._accountIdentifier
        };
      }
    }
  }
}
