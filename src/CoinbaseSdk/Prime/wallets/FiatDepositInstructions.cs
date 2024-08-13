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
  public class FiatDepositInstructions
  {
    public string? Id { get; set; }
    public string? Name { get; set; }
    public DepositType Type { get; set; }

    [JsonPropertyName("account_number")]
    public string? AccountNumber { get; set; }

    [JsonPropertyName("routing_number")]
    public string? RoutingNumber { get; set; }

    [JsonPropertyName("reference_code")]
    public string? ReferenceCode { get; set; }

    public FiatDepositInstructions() { }

    public class FiatDepositInstructionsBuilder
    {
      private string? _id;
      private string? _name;
      private DepositType _type;
      private string? _accountNumber;
      private string? _routingNumber;
      private string? _referenceCode;

      public FiatDepositInstructionsBuilder() { }

      public FiatDepositInstructionsBuilder WithId(string id)
      {
        this._id = id;
        return this;
      }

      public FiatDepositInstructionsBuilder WithName(string name)
      {
        this._name = name;
        return this;
      }

      public FiatDepositInstructionsBuilder WithType(DepositType type)
      {
        this._type = type;
        return this;
      }

      public FiatDepositInstructionsBuilder WithAccountNumber(string accountNumber)
      {
        this._accountNumber = accountNumber;
        return this;
      }

      public FiatDepositInstructionsBuilder WithRoutingNumber(string routingNumber)
      {
        this._routingNumber = routingNumber;
        return this;
      }

      public FiatDepositInstructionsBuilder WithReferenceCode(string referenceCode)
      {
        this._referenceCode = referenceCode;
        return this;
      }

      public FiatDepositInstructions Build()
      {
        return new FiatDepositInstructions
        {
          Id = this._id,
          Name = this._name,
          Type = this._type,
          AccountNumber = this._accountNumber,
          RoutingNumber = this._routingNumber,
          ReferenceCode = this._referenceCode
        };
      }
    }
  }
}
