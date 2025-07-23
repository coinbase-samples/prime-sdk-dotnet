/*
 * Copyright 2025-present Coinbase Global, Inc.
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

namespace CoinbaseSdk.Prime.Model
{
  public class WalletCryptoDepositInstructions
  {
    public string? Id { get; set; }
    public string? Name { get; set; }
    public WalletDepositInstructionType Type { get; set; }
    public string? Address { get; set; }
    public string? AccountIdentifier { get; set; }
    public string? AccountIdentifierName { get; set; }
    public Network? Network { get; set; }

    public WalletCryptoDepositInstructions() { }

    public class WalletCryptoDepositInstructionsBuilder
    {
      private string? _id;
      private string? _name;
      private WalletDepositInstructionType _type;
      private string? _address;
      private string? _accountIdentifier;
      private string? _accountIdentifierName;
      private Network? _network;

      public WalletCryptoDepositInstructionsBuilder WithId(string id)
      {
        this._id = id;
        return this;
      }

      public WalletCryptoDepositInstructionsBuilder WithName(string name)
      {
        this._name = name;
        return this;
      }

      public WalletCryptoDepositInstructionsBuilder WithType(WalletDepositInstructionType type)
      {
        this._type = type;
        return this;
      }

      public WalletCryptoDepositInstructionsBuilder WithAddress(string address)
      {
        this._address = address;
        return this;
      }

      public WalletCryptoDepositInstructionsBuilder WithAccountIdentifier(string accountIdentifier)
      {
        this._accountIdentifier = accountIdentifier;
        return this;
      }

      public WalletCryptoDepositInstructionsBuilder WithAccountIdentifierName(string accountIdentifierName)
      {
        this._accountIdentifierName = accountIdentifierName;
        return this;
      }

      public WalletCryptoDepositInstructionsBuilder WithNetwork(Network network)
      {
        this._network = network;
        return this;
      }

      public WalletCryptoDepositInstructions Build()
      {
        return new WalletCryptoDepositInstructions
        {
          Id = this._id,
          Name = this._name,
          Type = this._type,
          Address = this._address,
          AccountIdentifier = this._accountIdentifier,
          AccountIdentifierName = this._accountIdentifierName,
          Network = this._network,
        };
      }
    }
  }
}