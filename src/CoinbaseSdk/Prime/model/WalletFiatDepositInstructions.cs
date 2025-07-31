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
  public class WalletFiatDepositInstructions
  {
    public string? Id { get; set; }
    public string? Name { get; set; }
    public WalletDepositInstructionType Type { get; set; }
    public string? AccountNumber { get; set; }
    public string? RoutingNumber { get; set; }
    public string? ReferenceCode { get; set; }

    public WalletFiatDepositInstructions() { }

    public class WalletFiatDepositInstructionsBuilder
    {
      private string? _id;
      private string? _name;
      private WalletDepositInstructionType _type;
      private string? _accountNumber;
      private string? _routingNumber;
      private string? _referenceCode;

      public WalletFiatDepositInstructionsBuilder WithId(string id)
      {
        this._id = id;
        return this;
      }

      public WalletFiatDepositInstructionsBuilder WithName(string name)
      {
        this._name = name;
        return this;
      }

      public WalletFiatDepositInstructionsBuilder WithType(WalletDepositInstructionType type)
      {
        this._type = type;
        return this;
      }

      public WalletFiatDepositInstructionsBuilder WithAccountNumber(string accountNumber)
      {
        this._accountNumber = accountNumber;
        return this;
      }

      public WalletFiatDepositInstructionsBuilder WithRoutingNumber(string routingNumber)
      {
        this._routingNumber = routingNumber;
        return this;
      }

      public WalletFiatDepositInstructionsBuilder WithReferenceCode(string referenceCode)
      {
        this._referenceCode = referenceCode;
        return this;
      }

      public WalletFiatDepositInstructions Build()
      {
        return new WalletFiatDepositInstructions
        {
          Id = this._id,
          Name = this._name,
          Type = this._type,
          AccountNumber = this._accountNumber,
          RoutingNumber = this._routingNumber,
          ReferenceCode = this._referenceCode,
        };
      }
    }
  }
}